import json
import requests
import matplotlib.pyplot as plt
import matplotlib.cm as cm
import numpy as np
from datetime import datetime

class Author:
    def __init__(self, name, date, file):
        self.name = name
        self.date = datetime(int(date[0:4]), int(date[5:7]), int(date[8:10]), int(date[11:13]), int(date[14:16]), int(date[17:19]))
        self.file = file

def myKey(e):
    return e.name

# GitHub Authentication function
# This function is from the provided CollectFiles.py
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

# Check if a source file
def checkFile(fileName):
    if "src/" not in fileName:
        return False
    if (fileName.endswith('.cpp') or
    fileName.endswith('.c') or
    fileName.endswith('.java')):
        return True
    else:
        return False

# This function is originally from the CollectFiles.py and
# has been modified to get names of commit authors and dates
def countfiles(lsttokens, repo, authorArr):
    ipage = 1  # url page counter
    ct = 0  # token counter
    startDate = datetime(2024, 1, 1)
    numAuthors = []

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
                    if checkFile(filename):
                        author = Author(shaObject['commit']['author']['name'], shaObject['commit']['author']['date'], filename)
                        authorArr.append(author)
                        if author.name not in numAuthors:
                            numAuthors.append(author.name)
                        if author.date < startDate:
                            startDate = author.date
            ipage += 1
        
        return startDate, numAuthors
    except:
        print("Error receiving data")
        exit(0)
    
repo = 'scottyab/rootbeer'
lstTokens = [""]
authorArr = []
startDate, numAuthors = countfiles(lstTokens, repo, authorArr)

authorArr.sort(key=myKey)
previousName = authorArr[0].name
files = []
times = []
fig, ax = plt.subplots()
c = cm.rainbow(np.linspace(0, 1, len(numAuthors)))
i = 0
for author in authorArr:
    if previousName != author.name:
        x = np.array(files)
        y = np.array(times)
        ax.scatter(x, y, color=c[i], label=previousName)
        files = []
        times = []
        i = i + 1
    files.append(author.file)
    deltaTime = author.date - startDate
    times.append(deltaTime.days / 7)
    previousName = author.name

x = np.array(files)
y = np.array(times)
ax.scatter(x, y, color=c[i], label=previousName)

xlabels = range(0, 100)
ax.set_xticklabels(xlabels)
ax.set_xlabel("File")
ax.set_ylabel("Weeks")
#ax.legend()
plt.show()