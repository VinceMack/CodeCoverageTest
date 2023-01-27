import csv
import numpy as np
import matplotlib.pyplot as plt
from datetime import date
import pandas
import datetime



data = pandas.read_csv(r'data/file_rootbeer.csv')
data['Date'] = pandas.to_datetime(data['Date'], format = '%Y-%m-%d')
today = datetime.date.today()
today = pandas.to_datetime(today, format = '%Y-%m-%d')



data['Days'] = abs(today - data['Date'])
data['Weeks'] = (data['Days'].dt.days/7)

colors = pandas.Series(['#FF0000', '#0000FF' ,'#fc4503', '#03fc6f', '#a903fc', '#0341fc', 
                   '#03fcba', '#5afc03' ,'#fc03df', '#66ff66', '#4B6D8F', '#AED581',
                   '#00838F', '#F44336' ,'#F44336', '#2E7D32', '#181B1C', '#4D4183'])

colorData = data.drop_duplicates(subset = ['Author'])
colorData = colorData.drop(columns = ["File ID","Date"])
colorData = colorData.reset_index(drop = True)
colorData['Colors'] = colors


data['colors'] = np.nan
i = 0
for temp in data.Author:
    data.colors[i] = colorData[colorData.Author == temp].Colors.item()
    i+= 1

plt.scatter(data['File ID'],data['Weeks'],c = data['colors'])
plt.xlabel('File ID')
plt.ylabel('Weeks')

plt.show()