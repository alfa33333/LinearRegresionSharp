using System.Globalization;
using System.Reflection;
using Microsoft.Data.Analysis;
using CsvHelper;

const string dataPath = @"C:\Users\alfa33333\Documents\GitHub\LinearRegresionSharp\Data\example.csv";

var dataFrame = DataFrame.LoadCsv(dataPath);

Console.WriteLine(dataFrame);

using var reader = new StreamReader(dataPath);
using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
csv.Read();
csv.ReadHeader();
string[]? headers = csv.HeaderRecord;

Console.WriteLine(headers?[0] + " columns");

var records = csv.GetRecords<dynamic>();
foreach (object record in records)
{
    var temp = (IDictionary<string, object>)record;
    Console.WriteLine(Int32.Parse(temp["Y"].ToString()));
}

reader.BaseStream.Seek(0, SeekOrigin.Begin);
using var csv2 = new CsvReader(reader, CultureInfo.InvariantCulture);
csv2.Read();
csv2.ReadHeader();
while (csv2.Read())
{
    foreach (var header in headers)
    {
        Console.WriteLine($"{header}: {csv2.GetField(header)}");
    }
}

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