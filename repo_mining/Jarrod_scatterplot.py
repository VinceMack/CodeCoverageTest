import csv
import numpy as np
import matplotlib.pyplot as plt
from datetime import date

authorid = dict()
authorNum = 0

filename = 'data/file_authorsrootbeer.csv'
files = []

authors = []
totalauthors = []
filenums = []
dates = []
numdates = []
weeks = []
authororder = []

colormap = np.array(['#FF0000', '#CD5C5C', '#FF1493', '#FFC0CB', '#FF7F50',
                     '#FF8C00', '#FFD700', '#FFFF00', '#EE82EE', '#9370DB',
                     '#4B0082', '#6A5ACD', '#7FFF00', '#32CD32', '#00FF7F',
                     '#00FFFF', '#7FFFD4', '#00BFFF', '#0000FF'])

with open(filename, 'r') as csvfile:
    datareader = csv.reader(csvfile)
    next(datareader)
    for row in datareader:
        files.append(row)
      
#Parses the .csv into their corresponding array      
counter = 0
for x in files:
    authors.append(x[0])
    filenums.append(int(x[1]))
    dates.append(x[2])
    
#Converts the dates from strings to ints
for x in dates:
    splitDates = x.split('-')
    for y in range(0, len(splitDates)):
        splitDates[y] = int(splitDates[y])
    numdates.append(splitDates)    
      
#Calculates the number of weeks between 1/26/2023 to the commit date
for x in numdates: 
    updateDate = date(x[0],x[1],x[2])
    currentDate = date(2023,1,26)
    days = abs(updateDate-currentDate).days
    weeks.append(days//7)
    
#Gives each author an id to get a color
for currauthor in authors:
    if currauthor not in authorid:
        authorid[currauthor] = authorid.get(currauthor, authorNum)
        authorNum += 1

#Creates an array with corresponding author id to assign data points with author
for x in authors:
    authororder.append(authorid.get(x))      
    
plt.scatter(filenums, weeks, c = colormap[authororder])
plt.xlabel("File")
plt.ylabel("Weeks")
plt.show()