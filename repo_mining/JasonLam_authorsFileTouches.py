import json
import requests
import csv

import os

if not os.path.exists("data"):
 os.makedirs("data")

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
def countfiles(dictfiles, touchCount, lsttokens, repo):
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
                authorjson = shaDetails['commit']
                authornames = authorjson['author']['name']
                lastaccessdate = authorjson['author']['date']
                for filenameObj in filesjson:
                    filename = filenameObj['filename']
                    for x in fileArray:
                        if filename.endswith((".py", ".java", ".cpp", ".h")):
                            if filename == x:
                                date = lastaccessdate.split('T')
                                commitMeta = [authornames, str(fileIDs.get(x)), date[0]]
                                touchCount.append(commitMeta)
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
touchCount = []

dictfiles = dict()
fileIDs = dict()
fileID = 0
fileArray = []
filePath = 'data/file_rootbeer.csv'

with open(filePath, 'r') as csvfile:
    data = csv.DictReader(csvfile)
    for row in data:
        fileArray.append(row['Filename'])

for file in fileArray:
    fileIDs[file] = dictfiles.get(file, fileID)
    fileID += 1

countfiles(dictfiles,touchCount, lstTokens, repo)

file = repo.split('/')[1]
fileOutput = 'data/file_' + file + 'Touches.csv'

fileCSV = open(fileOutput, 'w', newline = '\n')
writer = csv.writer(fileCSV)
rows = ["Author", "File ID", "Date"]
writer.writerow(rows)

for x in touchCount:
    writer.writerow(x)

fileCSV.close()
print('The file ' + fileOutput + ' has been created.')

