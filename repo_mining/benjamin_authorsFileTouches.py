import json
import requests
import datetime

# Class to hold data on authors' touches
class Touch:
    def __init__(self, name, fileNumber, age):
        self.author = name
        self.weeksAgo = age
        self.file = fileNumber

# GitHub Authentication function
def github_auth(url, lsttoken, ct):
    jsonData = None
    try:
        ct = ct % len(lsttoken)
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
def GetTouches(touches, files, lsttokens, repo, srcFileExtensions):
    ipage = 1  # url page counter
    ct = 0  # token counter
    today = (datetime.date.today() - datetime.timedelta(days=datetime.date.today().weekday()))

    try:
    # loop though all the commit pages until the last returned empty page
        while True:
            spage = str(ipage)
            commitsUrl = 'https://api.github.com/repos/' + repo + '/commits?page=' + spage + '&per_page=1000'
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
                    # Only acts upon specified source files
                    if not filename.split('.')[len(filename.split('.'))-1] in srcFileExtensions:
                        continue
                    # Orders files into numbers
                    if not filename in files:
                        files.append(filename)
                    editDateRaw = datetime.datetime.strptime(shaObject['commit']['author']['date'], '%Y-%m-%dT%H:%M:%SZ').date()
                    editDate = (editDateRaw - datetime.timedelta(days=editDateRaw.weekday()))        
                    touches.append(Touch(shaObject['commit']['author']['name'], files.index(filename), (today - editDate).days / 7))
            ipage += 1
    except:
        print("Error receiving data")
        exit(0)

if __name__ == "__main__":

    # GitHub repo
    # repo = 'TheBenKnee/CS-472-Senior-Design-Project'
    repo = 'scottyab/rootbeer'

    lstTokens = ['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']

    touches = []
    files = []
    srcFileExtensions = ['py', 'cpp', 'cxx', 'cc', '.java', 'kt']

    GetTouches(touches, files, lstTokens, repo, srcFileExtensions)