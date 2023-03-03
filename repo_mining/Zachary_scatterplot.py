import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cm as cm
import matplotlib.colors as colors
import pandas as pd
from datetime import datetime

data = pandas.read_csv(r'data\file_rootbeer.csv')
# for jupyter notebook
#data = pd.read_csv('file_rootbeer.csv')

currentDay = datetime.today().strftime('%Y-%m-%d')

data['Timestamp'] = pd.to_datetime(data['Timestamp'], format = '%Y-%m-%d')
data['TimeFormat'] = data['Timestamp'].astype(str).str[:10]

weekList = []
# Calculate the difference of weeks since last commit
for ind in data.index:
    curr = datetime.strptime(currentDay, '%Y-%m-%d')
    commDate = data['TimeFormat'][ind]
    commDate = datetime.strptime(commDate, '%Y-%m-%d')
    diff = curr - commDate
    weekList.append(diff.days//7)

data['Weeks'] = weekList

fileNumbers = []
fileDict = {val: idx for idx, val in enumerate(data['Filename'].unique())}
for i in data.index:
    grabName = data['Filename'][i]
    fileNumbers.append(fileDict.get(grabName))
data['FileNumber'] = fileNumbers

# Create a dictionary to match the authors with a color that we will store in
# a color array below
authorColor = []
authDict = {val: idx for idx, val in enumerate(data['Author'].unique())}
for i in data.index:
    grabName = data['Author'][i]
    authorColor.append(authDict.get(grabName))

# Random color hex values made with a generator
colorMapping = np.array(['#c6b944', '#283d4c', '#44ab20', '#d94f93', '#97a5c5',
                     '#4175bc', '#ae8761', '#701e42', '#38cc48', '#93af8c',
                     '#0bb2fe', '#23df99', '#270836', '#5417d5', '#fce6de'])

plt.scatter(data['FileNumber'], data['Weeks'], c = colorMapping[authorColor])
plt.xlabel("File")
plt.ylabel("Weeks")
plt.show()