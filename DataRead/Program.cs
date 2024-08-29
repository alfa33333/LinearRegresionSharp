using System.Globalization;
using System.Reflection;
using Microsoft.Data.Analysis;
using CsvHelper;

const string dataPath = @"C:\Users\alfa33333\Documents\GitHub\LinearRegresionSharp\Data\example.csv";

var dataFrame = DataFrame.LoadCsv(dataPath);

Console.WriteLine(dataFrame);

DataPack dataPack = new DataPack();

dataPack.DataReader(dataPath);

dataPack.X.ForEach(Console.WriteLine);
dataPack.Y.ForEach(Console.WriteLine);

public class DataPack
{
    public List<float> X { get; set; } = [];
    public List<float> Y { get; set; } = [];

    public void DataReader(string dataPath)
    {
        using var reader = new StreamReader(dataPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Read();
        csv.ReadHeader();
        string[]? headers = csv.HeaderRecord;
        ArgumentNullException.ThrowIfNull(headers);
        while (csv.Read())
        {
            foreach (var header in headers)
            {
                if (header == "Y")
                    Y.Add(csv.GetField<float>(header));
                else
                    X.Add(csv.GetField<float>(header));
            }
        }
    }


}