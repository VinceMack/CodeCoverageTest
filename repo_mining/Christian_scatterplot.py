import numpy as np
import matplotlib.pyplot as plt
import Christian_authorFileTouches as aft

colors = ['blue', 'orange', 'green', 'red', 'purple',
        'brown', 'pink', 'gray', 'olive', 'cyan',
        'maroon', 'tomato', 'tan', 'gold', 'greenyellow',
        'lime', 'turquoise', 'lightblue', 'navy', 'blueviolet']

# GitHub repo and token
repo = 'scottyab/rootbeer'
lstTokens = []

# collection of touches and authors
touchList = []
authorList = {}

# gets list of touches
aft.getFileTouches(touchList, lstTokens, repo)

# assign authors to list of authors
for touch in touchList:
    if not touch.author in authorList:
        authorList[touch.author] = [[], []]

# assign the file worked on and age according to weeks
for commit in touchList:
        authorList[commit.author][0].append(commit.fileNumber)
        authorList[commit.author][1].append(commit.age)

# create graph
plt.title("Author Activities")
plt.xlabel('File')
plt.ylabel('Weeks')
plt.tight_layout()

color = 0
for author in authorList:
    plt.scatter(authorList[author][0], authorList[author][1], c = colors[color])
    color += 1

plt.show()