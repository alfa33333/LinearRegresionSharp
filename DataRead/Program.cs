using System.Globalization;
using Microsoft.Data.Analysis;
using CsvHelper;

const string dataPath = @"C:\Users\alfa33333\Documents\GitHub\LinearRegresionSharp\Data\example.csv";

var dataFrame = DataFrame.LoadCsv(dataPath);

Console.WriteLine(dataFrame);

using var reader = new StreamReader(dataPath);
using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
{
    csv.Read();
    csv.ReadHeader();
    string[]? headers = csv.HeaderRecord;
    
    Console.WriteLine(headers?[0] + " columns");
    
    var records = csv.GetRecords<dynamic>();
    foreach (var record in records)
    {
        Console.WriteLine(record);
    }
}