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
    
# commit class, stores filename, timestamp, and author of a single commit
class commit:
    filename = ""
    timestamp = ""
    author = ""
    def __init__(self, filename, timestamp, author):
        self.filename = filename
        self.timestamp = timestamp
        self.author = author
        
commitList = []

srcFileExtensions = ['.cpp', '.java', '.h', '.kt' '.gradle', '.xml']

# @lstTokens, GitHub authentication tokens
# @repo, GitHub repo
def countfiles(lsttokens, repo):
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
                #for i in shaDetails:
                #    print(i)
                author = "unknown author"
                timestamp = shaDetails['commit']['author']['date']
                print(timestamp)
                try:
                    author = shaDetails['author']['login']
                except:
                    print("Error finding author")
                print(author)
                for filenameObj in filesjson:
                    filename = filenameObj['filename']
                    
                    # only count files with specific extensions as src files
                    validExtension = False
                    for ext in srcFileExtensions:
                        if filename.find(ext) != -1:
                            validExtension = True;
                            break
                    
                    if validExtension:
                        commitList.append(commit(filename,timestamp,author))
                        print(filename)
                    
                    
                    
                    
                    
                    
                    #dictfiles[filename] = dictfiles.get(filename, 0) + 1
                print('\n')
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
lstTokens = ["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",
                "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",
                "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"]

#dictfiles = dict()
countfiles(lstTokens, repo)
#print('Total number of files: ' + str(len(dictfiles)))

file = repo.split('/')[1]
# change this to the path of your file
fileOutput = 'data/file_' + file + '.csv'
rows = ["Filename", "Timestamp", "Author"]
fileCSV = open(fileOutput, 'w')
writer = csv.writer(fileCSV)
writer.writerow(rows)

for com in commitList:
    writer.writerow([com.filename, com.timestamp, com.author])

print("Done! Data is in " + fileOutput)
"""
bigcount = None
bigfilename = None
for filename, count in dictfiles.items():
    rows = [filename, count]
    writer.writerow(rows)
    if bigcount is None or count > bigcount:
        bigcount = count
        bigfilename = filename
fileCSV.close()
print('The file ' + bigfilename + ' has been touched ' + str(bigcount) + ' times.')
"""
