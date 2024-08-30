using LinearRegression;
using DataRead;

const string dataPath = @"../../../../Data/example.csv";

DataPack exampleData = new DataPack();

exampleData.DataReader(dataPath);



// Get the properties of the object
var properties = exampleData.GetType().GetProperties();

// Iterate over the properties and print their names
Console.WriteLine($"{properties[0].Name} {properties[1].Name}");


for (int i = 0; i < exampleData.X.Count; i++)
{
    Console.WriteLine($"{exampleData.X[i]}  {exampleData.Y[i]}");
}


// Create a new LinearModel object

LinearModel model = new LinearModel();

// Load the data into the model

model.loadData(exampleData.X.ToArray(), exampleData.Y.ToArray());

// Print the predictors and responses
model.Fit();