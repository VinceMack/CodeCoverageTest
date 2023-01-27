
import datetime
import json
import requests
import csv

import os

if not os.path.exists("data"):
 os.makedirs("data")

authorsDict = dict()
filesDict = dict()

# GitHub Authentication function
def github_auth(url, lsttoken, ct):
    jsonData = None
    try:
        ct = ct % len(lstTokens)
        headers = {'Authorization': 'Bearer {}'.format(lsttoken[ct])}
        request = requests.get(url, headers=headers)
        jsonData = json.loads(request.content)
        ct += 1
    except Exception as e:
        pass
        print(e)
    return jsonData, ct

# @dictFiles, empty dictionary of files
# @lstTokens, GitHub authentication tokens
# @repo, GitHub repo
def countfiles(dictfiles, lsttokens, repo):
    ipage = 1  # url page counter
    ct = 0  # token counter

    try:
        # loop though all the commit pages until the last returned empty page
        while True:
            spage = str(ipage)
            commitsUrl = 'https://api.github.com/repos/' + repo + '/commits?page=' + spage + '&per_page=100'
            jsonCommits, ct = github_auth(commitsUrl, lsttokens, ct)

            # break out of the while loop if there are no more commits in the pages
            if len(jsonCommits) == 0:
                break
            # iterate through the list of commits in  spage

            for shaObject in jsonCommits:
                sha = shaObject['sha']
                # For each commit, use the GitHub commit API to extract the files touched by the commit
                shaUrl = 'https://api.github.com/repos/' + repo + '/commits/' + sha
                shaDetails, ct = github_auth(shaUrl, lsttokens, ct)

                filesjson = shaDetails['files']
                for filenameObj in filesjson:
                    filename = filenameObj['filename']
                    if '.java' in filename or '.cpp' in filename or '.c' in filename[len(filename)-3:len(filename)]:
                        filesDict[filename] = True
                        authorsList = authorsDict.get(filename, list())
                        author = shaObject.get('commit').get('author').get('name')
                        dateObj = datetime.datetime.strptime(shaObject.get('commit').get('author').get('date'), "%Y-%m-%dT%H:%M:%SZ")
                        if len(authorsList) > 0:
                            modified = False
                            for i in range(len(authorsList)):
                                if author in authorsList[i][0]:
                                    if dateObj < authorsList[i][1]:
                                        authorsList[i][1] = dateObj
                                        modified = True
                                        break
                            if not modified:
                                authorsList.append([author, dateObj])
                        else:
                            authorsList.append([author, dateObj])
                            authorsDict[filename] = authorsList
                        authorsDict[filename] = authorsList
                        dictfiles[filename] = dictfiles.get(filename, 0) + 1
                        print(filename)
            ipage += 1
    except:
        print("Error receiving data")
        exit(0)
# GitHub repo
repo = 'scottyab/rootbeer'
# repo = 'Skyscanner/backpack' # This repo is commit heavy. It takes long to finish executing
# repo = 'k9mail/k-9' # This repo is commit heavy. It takes long to finish executing
# repo = 'mendhak/gpslogger'


# put your tokens here
# Remember to empty the list when going to commit to GitHub.
# Otherwise they will all be reverted and you will have to re-create them
# I would advise to create more than one token for repos with heavy commits
lstTokens = ["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"]

dictfiles = dict()
countfiles(dictfiles, lstTokens, repo)
print('Total number of files: ' + str(len(dictfiles)))

file = repo.split('/')[1]
# change this to the path of your file
fileOutput = 'data/file_' + file + '.csv'
rows = ["Filename", "Touches"]
fileCSV = open(fileOutput, 'w')
writer = csv.writer(fileCSV)
writer.writerow(rows)

bigcount = None
bigfilename = None
for filename, count in dictfiles.items():
    rows = [filename, count]
    writer.writerow(rows)
    if bigcount is None or count > bigcount:
        bigcount = count
        bigfilename = filename
fileCSV.close()

for key in authorsDict:
    obj = authorsDict.get(key)
    for n in obj:
        print(n)
    print('========================')

exit(0)
