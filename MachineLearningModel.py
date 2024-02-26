#This work will be annotated using the Better Comments Extension
#! This is for notes and for things that need to be worked on, mainly error handling
#? This is for questioning if this is finished or needs adding/reworking
#*This text will highlight, this will mainly be for notes
#//This should cross out any lines of code that are no longer needed or used during testing.
#TODO This is for annotating work that needs to be implemented.

from sklearn.ensemble import RandomForestClassifier
import sys
import json
import pandas
import random
import numpy
import os
from sklearn.model_selection import train_test_split


array_2d = []
columnAverages = []
column_arrays = []#Array for each keyset (this will take the timing between keystroke 1 and 2 of every sample)
standard_deviations = []#Create our standard deviation arrays
poisonSamples = []
numberOfPoisonSamplesWanted = 0
concatenated_strings = []
###########################################################
#Read the objects
###########################################################
columnNames = ["PatternNumber", "UserID", "Keystroke", "Expected"]
fileLocation = "../../../KeystrokeExcel.xlsx"
data = pandas.read_excel(fileLocation, names = columnNames)
print(os.getcwd())
dataFrame = pandas.DataFrame(data)

array_2d = [row.split() for row in dataFrame['Keystroke']]

def remove_keystrokes_and_commas(text):
    words = text.split(',')
    words = [word for word in words if word != 'Keystroke']
    return ','.join(words)


def row_to_2d_array(row):
    # Split the row by commas and create a list
    elements = row.split(',')
    # Remove 'Keystroke' from the list
    elements = [float(e) for e in elements if e != 'Keystroke']
    # Convert list into a 2D array where each element is a list
    return [elements]

dataFrame['Keystroke'] = dataFrame['Keystroke'].apply(remove_keystrokes_and_commas)
#print(dataFrame)
array_2d = numpy.concatenate(dataFrame['Keystroke'].apply(row_to_2d_array).tolist(), axis=0)

#print(dataFrame)
#############################################################
#Finished 2D Array Creation
#############################################################


#############################################################
#Creating standard deviations
#############################################################

#Calculate average for each timing per pattern
for col_index in range(len(array_2d[0])):
    # Initialize sum and count for the current column
    col_sum = 0
    col_count = 0
    # Iterate over rows
    for row in array_2d:
        # Add element from the current column to sum
        col_sum += row[col_index]
        # Increment count
        col_count += 1
    # Calculate average for the current column
    col_average = col_sum / col_count
    # Append the average to the list of column averages
    columnAverages.append(col_average)

#Create X amount of arrays of Y length depending on samples

#Calculate standard deviations

# Iterate over columns
#For loop to create new arrays that have our keyset timings
for col_index in range(len(array_2d[0])):
    # Initialize an empty array for the current column
    column_array = []
    # Iterate over rows
    for row in array_2d:
        # Append the element from the current column to the column array
        column_array.append(row[col_index])
    # Append the column array to the list of column arrays
    column_arrays.append(column_array)

#############################################################
#Loop to generate our standard deviation for each timeset
##########################################################
for i in range(len(column_arrays)):
    standard_deviations.append(numpy.std(column_arrays[i]))


#print(array_2d)
#print(len(standard_deviations))
#print(len(columnAverages))
#print(standard_deviations)
#Add noise
##############################################################################
#Generate 10x the amount we have using just the averages
##############################################################################
#print(numberOfSamplesWanted)

#Have a function that iterates for each sample generation
numberOfPoisonSamplesWanted = len(array_2d)*3
for i in range(numberOfPoisonSamplesWanted):
    newSample = []
    for j in range(len(columnAverages)):
        newRandom = random.randint(-2,2)
        noise = random.randint(-20,20)
        if(columnAverages[j] + newRandom*standard_deviations[j] + noise <= 0):
            newSample.append(100)
        else:
            newSample.append(round(columnAverages[j] + newRandom*standard_deviations[j] + noise))
        #print(newSample)
        j+= 1
    #print(newSample)
    poisonSamples.append(newSample)
    
    i+= 1
dataFrame['Keystroke'] = pandas.concat([dataFrame['Keystroke'],pandas.Series(poisonSamples)], ignore_index=True)

#print(dataFrame)

#print(poisonSamples)
#Reconstruct the string
#Sample created

concatenated_strings = [','.join(map(str, row)) for row in poisonSamples]
newData = {
    'Keystroke': concatenated_strings,
    'Expected': 1
    }
legitimateKeystrokeData = dataFrame['Keystroke'].tolist()
legitimateExpectedData = [0] * len(array_2d)

poisonedKeystrokeData = concatenated_strings
poisonedExpectedData = [1] * numberOfPoisonSamplesWanted

combined_keystrokes = legitimateKeystrokeData + poisonedKeystrokeData
combined_expected = legitimateExpectedData + poisonedExpectedData

finalDataFrame = pandas.DataFrame({
    'Keystroke': combined_keystrokes,
    'Expected': combined_expected
})

#print(finalDataFrame)
finalDataFrame['Keystroke'] = finalDataFrame['Keystroke'].apply(remove_keystrokes_and_commas)
new_array_2d = finalDataFrame.to_numpy()
#print(new_array_2d)
#print(new_array_2d)
inputs = []
targets = []
for idx, row in enumerate(new_array_2d):
    row_data = row[0].split(',')  # Split the string by comma to get individual features
    #print(row_data)
    try:
        inputs.append([float(val) for val in row_data if val.strip()])  # Convert features to float and append to features list
        targets.append(row[1])  # Append target variable to targets list
        #print(targets)
    except ValueError:#Incase something goes wrong with creating our targets for the model
        print("Non-numeric value found in row, skipping...")
        print(f"Non-numeric value found in row {idx}, skipping...")

#print(features)
x = numpy.array(inputs)
y = numpy.array(targets)
#scaler = MinMaxScaler()
#scaled = scaler.fit_transform(inputs)
#print(scaled)
X_train, X_test, Y_train, Y_test = train_test_split(x, y)

def trainClassifier(updatedInsertArray):
    #print("Inside classifier")
    classifierRandomForest = RandomForestClassifier(n_estimators=1000, max_depth=40, bootstrap=True, max_features=None)
    classifierRandomForest.fit(X_train, Y_train)
    accuracy = classifierRandomForest.score(X_test, Y_test)
    #print("Accuracy on test set:", accuracy)
    #definitelyFakeData = [138,141,234,300,383,94,185,158,187,108,108,171]
    #new_sample_array = numpy.array(definitelyFakeData).reshape(1,-1)
    prediction = classifierRandomForest.predict(updatedInsertArray)
    estimates = classifierRandomForest.predict_proba(updatedInsertArray)
    #print(prediction)
    #print(estimates)
    confidenceScore = 0
    if prediction == 0:
        confidenceScore = estimates[0,0]
    else:
        confidenceScore = estimates[0,1]

    #print("Predicted Value: " + str(prediction) + " with Confidence Score " + str(confidenceScore*100) + "%")
    #if estimates[0,0] >= 0.65:
        #print("The user has reached the threshold for login at confidence score of: " + str(estimates[0,0]*100) + "%")
    print(estimates[0,0])

if __name__ == "__main__":
    # Expecting two arguments: username and input string
    if len(sys.argv) != 3:
        print("Usage: python ml_script.py <username> <keystrokes>")
        sys.exit(1)
    username = sys.argv[1]
    keystrokes = sys.argv[2]


insertedData = keystrokes.split(',')
for word in insertedData:
    if word == "Keystroke":
        insertedData.remove('Keystroke')
reshapedInsertedData = numpy.array(insertedData).reshape(1,-1)
#print(reshapedInsertedData)
result = trainClassifier(reshapedInsertedData)
sys.exit()
#updatedInsertArray = row_to_2d_array(updatedInsert)
#trainClassifier(updatedInsertArray)
#print("Prediction:", prediction)
# Obtain probability estimates for each class

#probabilities = classifierRandomForest.predict_proba(definitelyFakeData)

# Calculate confidence score (maximum probability among predicted probabilities)
#confidence_scores = probabilities.max(axis=1)
#print(confidence_scores)
