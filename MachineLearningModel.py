from sklearn.ensemble import RandomForestClassifier
import sys
import json
import pandas
import random
import numpy
from sklearn.model_selection import train_test_split
import pyodbc

if __name__ == "__main__":
    # Expecting two arguments: username and input string
    if len(sys.argv) != 3:
        print("Usage: MachineLearningModel.py <username> <keystrokes>")
        sys.exit(1)
    username = sys.argv[1]
    keystrokes = sys.argv[2]
    keystrokes = keystrokes.split(',')#Seperate Keystroke,Number,Keystroke
    for word in keystrokes:
        if word == "Keystroke":#Remove Keystroke wording
            keystrokes.remove('Keystroke')
    reshapedInsertedData = numpy.array(keystrokes).reshape(1,-1)#Reshape into the correct format for the model


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

array_2d = []
columnAverages = []#For generating later in the model
column_arrays = []#Array for each keyset (this will take the timing between keystroke 1 and 2 of every sample)
standard_deviations = []#Create our standard deviation arrays
poisonSamples = []
numberOfPoisonSamplesWanted = 0
concatenated_strings = []
#insertedData = "Keystroke,123,Keystroke,110,Keystroke,169,Keystroke,237,Keystroke,126,Keystroke,135,Keystroke,174,Keystroke,170,Keystroke,120,Keystroke,78,Keystroke,92,Keystroke"
###########################################################
#Read the objects
###########################################################

#columnNames = ["PatternNumber", "UserID", "Keystroke", "Expected"]
#fileLocation = "KeystrokeExcel.xlsx"
#data = pandas.read_excel("KeystrokeExcel.xlsx", names = columnNames)
#dataFrame = pandas.DataFrame(data)

connectionString = pyodbc.connect(
    driver="ODBC Driver 17 for SQL Server",
    server="dissi-database.c32y6sk2evqy.eu-west-2.rds.amazonaws.com",
    database="Dissertation",
    uid="admin",
    pwd="V4F^E2Tt#M#p#bjj"
)


# Create a cursor object
cursor = connectionString.cursor()
# Execute the view
cursor.execute("EXEC Authentication.GetKeystrokes @TableName=?", (username))
# Fetch all rows
rows = cursor.fetchall()
    #print(eachRow)
# Assuming eachRow is a list of value
array_2d = []
# Iterate over the rows
for eachRow in rows:
    # Convert eachRow to a numpy array
    eachRow = numpy.array(eachRow)
    # Append the row to the array
    array_2d.append(eachRow)
    
# Convert the list to a numpy array for further processing
array_2d = numpy.array(array_2d)
#print(array_2d)

# Convert the 2D array into a DataFrame
dataFrame = pandas.DataFrame(array_2d, columns=["Keystroke", "Expected"])#Has every row from the database for the user.

# Print the DataFrame
#print(dataFrame)
cursor.close()#Close our query and connection
connectionString.close()

#############################################################
#At this point, we have read in our data and have it in a dataframe
############################################################
array_2d = [row.split() for row in dataFrame['Keystroke']]#Splitting each row on Keystroke
#print(array_2d)


array_2d = numpy.concatenate(dataFrame['Keystroke'].apply(row_to_2d_array).tolist(), axis=0)

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

#Have a function that iterates for each sample generation
numberOfPoisonSamplesWanted = len(array_2d)*3#Don't want an overly saturated poisoned amount, adjust as needed
for i in range(numberOfPoisonSamplesWanted):
    newSample = []#Current sample iteration
    for j in range(len(columnAverages)):
        newRandom = random.randint(-2,2)#Random generation of number based on average
        noise = random.randint(-20,20)#Random noise to add to the sample
        if(columnAverages[j] + newRandom*standard_deviations[j] + noise <= 0): #Checks if we have a negative time
            newSample.append(100)#Just assign 100 for calculation sake
        else:
            newSample.append(round(columnAverages[j] + newRandom*standard_deviations[j] + noise))#This is our new timing for the keystroke pair
        #print(newSample)
        j+= 1
    #print(newSample)
    poisonSamples.append(newSample)
    
    i+= 1
dataFrame['Keystroke'] = pandas.concat([dataFrame['Keystroke'],pandas.Series(poisonSamples)], ignore_index=True)

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

def trainClassifier(reshapedInsertedData):
    #print("Inside classifier")
    classifierRandomForest = RandomForestClassifier(n_estimators=10000, max_depth=40, bootstrap=True, max_features=None)
    classifierRandomForest.fit(X_train, Y_train)
    accuracy = classifierRandomForest.score(X_test, Y_test)
    #print("Accuracy on test set:", accuracy)
    #definitelyFakeData = [94,46,174,215,142,91,62,159,155,91,31,107]
    #new_sample_array = numpy.array(definitelyFakeData).reshape(1,-1)
    #prediction = classifierRandomForest.predict(reshapedInsertedData)
    estimates = classifierRandomForest.predict_proba(reshapedInsertedData)
    print(estimates[0,0])
    confidenceScore = 0
    #if prediction == 0:
    #    confidenceScore = estimates[0,0]
    #else:
    #    confidenceScore = estimates[0,1]

#updatedInsert = remove_keystrokes_and_commas(insertedData)
#updatedInsertArray = row_to_2d_array(updatedInsert)

#print(insertedData)

#print(reshapedInsertedData)
result = trainClassifier(reshapedInsertedData)
sys.exit()