import json
import requests
import datetime

import numpy as np
import matplotlib.pyplot as plt

class Touch:
    def __init__(self, name, fileNumber, age):
        self.author = name
        self.weeksAgo = age
        self.file = fileNumber

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

# @touches, empty list of touches
# @files, empty list of filenames
# @lstTokens, GitHub authentication tokens
# @repo, GitHub repo
def getTouches(touches, files, lsttokens, repo):
    ipage = 1  # url page counter
    ct = 0  # token counter
    today = (datetime.date.today() - datetime.timedelta(days=datetime.date.today().weekday()))

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
                try:
                    sha = shaObject['sha']
                except:
                    continue
                # For each commit, use the GitHub commit API to extract the files touched by the commit
                shaUrl = 'https://api.github.com/repos/' + repo + '/commits/' + sha
                shaDetails, ct = github_auth(shaUrl, lsttokens, ct)
                filesjson = shaDetails['files']
                for filenameObj in filesjson:
                    filename = filenameObj['filename']
                    if not filename in files:
                        files.append(filename)
                    editDateRaw = datetime.datetime.strptime(shaObject['commit']['author']['date'], '%Y-%m-%dT%H:%M:%SZ').date()
                    editDate = (editDateRaw - datetime.timedelta(days=editDateRaw.weekday()))        
                    touches.append(Touch(shaObject['commit']['author']['name'], files.index(filename), (today - editDate).days / 7))
            ipage += 1
    except:
        print("Error receiving data")
        exit(0)

# @touches, filled list of touches
# @files, filled list of filenames
def graphTouches(touches, files):
    authors = {}
    colors = ['red', 'blue', 'orange', 'yellow', 'green', 'purple', 'pink', 'black', 'grey', 'brown', 
                'gold', 'navy', 'lightsteelblue', 'magenta', 'rosybrown', 'lime', 'cyan']
    for touch in touches:
        if not touch.author in authors:
            authors[touch.author] = [[], []]
        authors[touch.author][0].append(touch.file)
        authors[touch.author][1].append(touch.weeksAgo)
    
    maxWeeks = 0
    color = 0
    for author in authors:
        if color > len(colors) - 1:
            color = len(colors) - 1
        if max(authors[author][1]) > maxWeeks:
            maxWeeks = max(authors[author][1])
        plt.scatter(authors[author][0], authors[author][1], c=colors[color], s=100)
        color += 1

    plt.xticks(np.arange(0, len(files), 1.0))
    plt.yticks(np.arange(0, maxWeeks + 1, 1.0))

    plt.title('File Edits')
    plt.xlabel('File')
    plt.ylabel('Weeks Ago')

    plt.show()

# GitHub repo
# repo = 'TheBenKnee/CS-472-Senior-Design-Project'
repo = 'scottyab/rootbeer'
# repo = 'Skyscanner/backpack' # This repo is commit heavy. It takes long to finish executing
# repo = 'k9mail/k-9' # This repo is commit heavy. It takes long to finish executing
# repo = 'mendhak/gpslogger'


# put your tokens here
# Remember to empty the list when going to commit to GitHub.
# Otherwise they will all be reverted and you will have to re-create them
# I would advise to create more than one token for repos with heavy commits
lstTokens = []

touches = []
files = []

getTouches(touches, files, lstTokens, repo)
graphTouches(touches, files)