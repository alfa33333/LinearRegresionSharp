using Microsoft.Data.Analysis;


const string dataPath = @"../../../../Data/example.csv";

var dataFrame = DataFrame.LoadCsv(dataPath);
