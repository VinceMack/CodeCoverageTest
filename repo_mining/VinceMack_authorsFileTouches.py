import matplotlib.pyplot as plt
import numpy as np
import requests
import datetime
import json
import os

if not os.path.exists("data"):
 os.makedirs("data")

# CommitData - name, weeksSinceFirstCommit, sourceFileNumber
class CommitData:
    def __init__(self, n, w, f):
        self.weeksSinceFirstCommit = w
        self.sourceFileNumber = f
        self.name = n

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

# getCommitData - commitsData, files, lsttokens, repo
def getCommitData(commitsData, files, lsttokens, repo):
    ipage = 1  # url page counter
    ct = 0  # token counter
    firstCommit = datetime.date(2015, 6, 15) # date of the first commit of the repo we are observing

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
            #iterations = 0
            for shaObject in jsonCommits:
                sha = shaObject['sha']
                # For each commit, use the GitHub commit API to extract the files touched by the commit
                shaUrl = 'https://api.github.com/repos/' + repo + '/commits/' + sha
                shaDetails, ct = github_auth(shaUrl, lsttokens, ct)
                filesjson = shaDetails['files']
                for filenameObj in filesjson:
                    filename = filenameObj['filename']
                    
                    # assure each iteration is a source file
                    sourceFile = ['java']
                    if not filename.split('.')[len(filename.split('.'))-1] in sourceFile:
                        #iterations = iterations + 1
                        #print("not a source file", iterations)
                        continue
                    
                    # set numeric representation of files
                    if not filename in files:
                        files.append(filename)

                    # get numeric representation of files
                    fileNum = files.index(filename)
                    print("File number", fileNum, end=" ")

                    # get name from the current commit data
                    name = shaObject['commit']['author']['name']
                    print("was edited by", name, end=" ")

                    # use DateTime to find the number of weeks between date of the current commit and date of the first commit
                    temp = datetime.datetime.strptime(shaObject['commit']['author']['date'], '%Y-%m-%dT%H:%M:%SZ').date()
                    commitDate = (temp - datetime.timedelta(days=temp.weekday())) 
                    weeksAfterCommitDate = (commitDate - firstCommit).days / 7
                    print(weeksAfterCommitDate, "weeks after the first commit.")
                    
                    # add the current commit to the commitsData array
                    commitsData.append(CommitData(name, weeksAfterCommitDate, fileNum))
            ipage += 1
    except:
        print("Error receiving data")
        exit(0)

# graphCommitData - commitsData, files
def graphCommitData(commitsData, files):
    
    # find the maximum number of weeks between the newest and initial commits
    maxWeeksSinceFirstCommit = 0
    for curr in commitsData:
        if (curr.weeksSinceFirstCommit >= maxWeeksSinceFirstCommit):
            maxWeeksSinceFirstCommit = curr.weeksSinceFirstCommit
    
    # sets the keys of the commits using the contributor names
    commits = {}
    for curr in commitsData:
        if not curr.name in commits:
            commits[curr.name] = [[], []]
    
    # populate the commit data 
    for curr in commitsData:
        commits[curr.name][0].append(curr.weeksSinceFirstCommit)
        commits[curr.name][1].append(curr.sourceFileNumber)
        
    
    # plot the commit data 
    colors = ['red', 'green', 'blue', 'yellow', 'pink', 'black', 'orange', 'purple', 'coral', 'brown', 'sandybrown', 'cyan', 'magenta', 'lime', 'gold', 'navy', 'plum']
    color = 0
    for name in commits:
        plt.scatter(commits[name][1], commits[name][0], c=colors[color], s=50, label=name)
        color += 1

    # Graph Labels
    plt.title('Repository Commit Activity')

    plt.xlabel('Files')
    plt.xticks(np.arange(0, len(files), 1.0))
    
    plt.ylabel('Weeks Since First Commit')
    plt.yticks(np.arange(0, maxWeeksSinceFirstCommit*1.25, 50.0))
    
    # Render Graph
    plt.legend(loc='upper center', bbox_to_anchor=(0.5, -0.1),fancybox=True, shadow=True, ncol=3) # legend for Executive Summary analysis
    plt.tight_layout()
    plt.show()
      
# GitHub repo
repo = 'scottyab/rootbeer'

# GitHub API Token
lstTokens = [""]

# - - - - -

commitsData = []
files = []
getCommitData(commitsData, files, lstTokens, repo)
graphCommitData(commitsData, files)

