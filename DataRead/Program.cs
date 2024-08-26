using System.Globalization;
using Microsoft.Data.Analysis;
using CsvHelper;

const string dataPath = @"C:\Users\alfa33333\Documents\GitHub\LinearRegresionSharp\Data\example.csv";

var dataFrame = DataFrame.LoadCsv(dataPath);

Console.WriteLine(dataFrame);

using var reader = new StreamReader(dataPath);
using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
{
    var records = csv.GetRecords<Foo>();
    foreach (var record in records)
    {
        Console.WriteLine(record.X);
    }
}



public class Foo
{
    public int X { get; set; }
    public int Y { get; set; }
}