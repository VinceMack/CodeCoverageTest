import matplotlib.pyplot as plot
import matplotlib.cm as cm
import datetime
import csv

def time2unix(t):
    try:
        return datetime.datetime.strptime(t, "%Y-%m-%dT%H:%M:%SZ").timestamp()
    except:
        print(t + " does not fit!")

# gives times changed for a filename and a list of all commits made to it
timesChanged = dict()
commitList = dict()
authorColors = dict()

# commit class that holds author and 
class commit:
    timestamp = None
    author = None
    def __init__(self,t,a):
        self.timestamp = t
        self.author = a

# will hold all the file names in order of times changed
files = []


# arbitrary large number to start... dont judge me
earliestCommit = 999999999
with open('data/file_rootbeer.csv', 'r') as csvfile:
    reader = csv.reader(csvfile)
    for row in reader:
        if len(row) == 3:
            filename = row[0]
            if timesChanged.get(filename, 0) == 0:
                files.append(filename)
                commitList[filename] = []
            authorColors[row[2]] = "" # record authors
            # convert timestamp to weeks since first commit
            week = time2unix(row[1])/604800
            if week < earliestCommit:
                earliestCommit = week
            commitList[filename].append(commit(week, row[2]))
            timesChanged[filename] = timesChanged.get(filename, 0) + 1
# sort based on number of commits in descending order
files = sorted(files, key=lambda f: timesChanged[f], reverse=True)

# start weeks at earliest commit
for f in files:
    for i in range(len(commitList[f])):
        commitList[f][i].timestamp = commitList[f][i].timestamp-earliestCommit

# assign a color to every author
cmap = cm.get_cmap('nipy_spectral')
cindex = 0
for a in authorColors:
    authorColors[a] = cmap(cindex)
    cindex = cindex+ int(256/len(authorColors))
#print(authorColors)

# create three arrays to form the axes and colors
timestamps = []
filenumbers = []
authors = []
for f in range(len(files)):
    for i in range(len(commitList[files[f]])):
        filenumbers.append(f)
        timestamps.append(commitList[files[f]][i].timestamp)
        authors.append(authorColors[commitList[files[f]][i].author])

plot.scatter(filenumbers, timestamps, c=authors)
plot.ylabel('Week')
plot.xlabel('File')
plot.show()