
import datetime
import json
import matplotlib.pyplot as plot
import numpy as np
import requests
import csv

import os

if not os.path.exists("data"):
 os.makedirs("data")

authorsDict = dict()
filesDict = dict()

def sort (arr, arr2, arr3, arr4, arr5):
    flag = False
    for i in range(len(arr) - 1):
        for j in range(0, len(arr) - i - 2):
            if arr[j] > arr[j + 1]:
                flag = True
                arr[j], arr[j + 1] = arr[j + 1], arr[j]
                arr2[j], arr2[j + 1] = arr2[j + 1], arr2[j]
                arr3[j], arr3[j + 1] = arr3[j + 1], arr3[j]
                arr4[j], arr4[j + 1] = arr4[j + 1], arr4[j]
                arr5[j], arr5[j + 1] = arr5[j + 1], arr5[j]
        if not flag:
            return
    return [arr, arr2, arr3, arr4, arr5]

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
                        dateAccessed = dateObj.month
                        yearAccessed = dateObj.isocalendar().year
                        if len(authorsList) > 0:
                            modified = False
                            for i in range(len(authorsList)):
                                if author in authorsList[i][0]:
                                    if dateAccessed < authorsList[i][1]:
                                        authorsList[i][1] = dateAccessed
                                        modified = True
                                        break
                            if not modified:
                                authorsList.append([author, dateAccessed, yearAccessed])
                        else:
                            authorsList.append([author, dateAccessed, yearAccessed])
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

x = []
new_x = []
y = []
color = []
id = []
filesList = []

for key in authorsDict:
    # filesList.append(key)     
    for n in authorsDict.get(key):
        x.append(n[0])
        y.append(str(np.int64(n[2])) + "-" + str(np.int64(n[1])))
        # color.append(np.int64(n[1]) + np.int64(n[2]))
        id.append(np.int64(n[1] + np.int64(n[2] * 100)))
        color.append((len(n[0]) * 255 % ord(n[0][len(n)-1])))
        fileAbbrev = key[key.rfind('/') + 1:key.rfind('.')]
        filesList.append(fileAbbrev if len(fileAbbrev) < 8 else fileAbbrev[0:8] + '...')

sortedArrs = sort(id, x, y, color, filesList)
id = sortedArrs[0]
x = sortedArrs[1]
y = sortedArrs[2]
color = sortedArrs[3]
filesList = sortedArrs[4]

# print('The file ' + bigfilename + ' has been touched ' + str(bigcount) + ' times.')
plot.scatter(filesList,y,c=color,s=50)
plot.show()
