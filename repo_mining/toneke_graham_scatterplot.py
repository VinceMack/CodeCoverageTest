## SCATTER PLOT
## TONEKEGRAHAM
import csv
import numpy as np
import matplotlib.pyplot as plt
from datetime import date
import pandas
import datetime



data = pandas.read_csv('data.csv')
data['Date'] = pandas.to_datetime(data['Date'], format = '%Y-%m-%d')
today = datetime.date.today()
today = pandas.to_datetime(today, format = '%Y-%m-%d')



data['Days'] = abs(today - data['Date'])
data['Weeks'] = (data['Days'].dt.days/7)

colors = pandas.Series(['#FF0000', '#0000DF' ,'#ec4523', '#02fC6d', '#ag03fC', '#0371dc', 
                   '#03ccba', '#6adc03' ,'#fc03df', '#26ff66', '#4A6E8F', '#CED681',
                   '#00928F', '#F64336' ,'#047326', '#2E7332', '#186B1D', '#0D4193'])

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