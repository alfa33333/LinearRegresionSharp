using System.Globalization;
using System.Reflection;
using Microsoft.Data.Analysis;
using CsvHelper;
using DataRead;

const string dataPath = @"../../../../Data/example.csv";

var dataFrame = DataFrame.LoadCsv(dataPath);

Console.WriteLine(dataFrame);


DataPack dataPack = new DataPack();


/// Reading from DataFrame

dataPack.DataReader(dataFrame);

dataPack.X.ForEach(Console.WriteLine);
dataPack.Y.ForEach(Console.WriteLine);

/// Reading directly from CSV file
dataPack.DataReader(dataPath);

dataPack.X.ForEach(Console.WriteLine);
dataPack.Y.ForEach(Console.WriteLine);

namespace DataRead
{
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

        public void DataReader(DataFrame dataFrame)
        { 
            foreach(var column in dataFrame.Columns)
            {
                if (column.Name == "Y")
                {
                    foreach (float item in column)
                    {
                        Y.Add(item);
                    }
                }
                else
                {
                    foreach (float item in column)
                    {
                        X.Add(item);
                    }
                }
            }
        }


    }
}