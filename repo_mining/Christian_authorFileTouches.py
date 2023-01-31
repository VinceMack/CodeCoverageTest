import json
import requests
import datetime

# data of files touched
class FileTouch:
    def __init__(self, author, age, fileNumber):
        self.author = author
        self.age = age
        self.fileNumber = fileNumber

# collection of touches
touchList = []

# GitHub repo and token
repo = 'scottyab/rootbeer'
lstTokens = []

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

# @touchList, list of touches
# @lstTokens, GitHub authentication tokens
# @repo, GitHub repo
def getFileTouches(touchList, lsttokens, repo):
    ipage = 1  # url page counter
    ct = 0  # token counter

    # list of files that exist
    fileList = []
    currentDay = datetime.date.today()

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

                # iterate through commits
                for filenameObj in filesjson:
                    filename = filenameObj['filename']
                    
                    # evaluate if file contains a '.java' extension 
                    if '.java' in filename:

                        # get commit data
                        author = shaObject['commit']['author']['name']
                        dateStr = shaObject['commit']['author']['date']
                        dateObj = datetime.datetime.strptime(dateStr, '%Y-%m-%dT%H:%M:%SZ').date()                    
                        weeks = (currentDay - dateObj).days / 7

                        # get file number
                        if not filename in fileList:
                            fileList.append(filename)
                        fileNumber = fileList.index(filename)

                        # store and print touch data
                        touchList.append(FileTouch(author, weeks, fileNumber))
                        print(author, "edited file", fileNumber, "-", weeks, "weeks ago.")

            ipage += 1

    except:
        print("Error receiving data")
        exit(0)

if __name__ == "__main__":

    # Collect authors and dates of commits
    getFileTouches(touchList, lstTokens, repo)