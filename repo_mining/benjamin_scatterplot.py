import benjamin_authorsFileTouches as ba

import numpy as np
import matplotlib.pyplot as plt

# @touches, filled list of touches
# @files, filled list of filenames
def GraphTouches(touches, files):
    authors = {}
    colors = ['red', 'blue', 'orange', 'yellow', 'green', 'purple', 'pink', 'black', 'grey', 'brown', 
                'gold', 'navy', 'lightsteelblue', 'magenta', 'rosybrown', 'lime', 'cyan', 'lightgreen',
                    'palevioletred', 'cornflowerblue', 'olive']
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
        plt.scatter(authors[author][0], authors[author][1], c=colors[color], s=50, label=author)
        color += 1

    plt.xticks(np.arange(0, len(files), 2.0))
    plt.yticks(np.arange(0, maxWeeks + 50, 50.0))

    plt.legend(loc='center left', bbox_to_anchor=(1, 0.5))
    plt.title('File Edits')
    plt.xlabel('File')
    plt.ylabel('Weeks Since Today')
    plt.tight_layout()

    plt.show()

if __name__ == "__main__":

    # GitHub repo
    # repo = 'TheBenKnee/CS-472-Senior-Design-Project'
    repo = 'scottyab/rootbeer'

    lstTokens = []

    touches = []
    files = []
    srcFileExtensions = ['py', 'cpp', 'cxx', 'cc', 'java', 'kt']

    ba.GetTouches(touches, files, lstTokens, repo, srcFileExtensions)
    GraphTouches(touches, files)