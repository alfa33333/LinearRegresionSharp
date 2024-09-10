using Microsoft.Data.Analysis;
using CsvHelper;
using System.Globalization;

namespace RegressionApp.Models
{
    public class ReadData
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
            foreach (var column in dataFrame.Columns)
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
