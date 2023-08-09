using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Task5.Models;

namespace Task5.Utils
{
    public class CsvConverter<T>
    {
        private readonly List<T> records;

        private readonly CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true
        };

        public CsvConverter(List<T> records)
        {
            this.records = records;
        }

        public string GetCsvString()
        {
            using (var stringWriter = new StringWriter())
            using (var csv = new CsvWriter(stringWriter, csvConfig))
            {
                csv.WriteRecords(records);
                return stringWriter.ToString();
            }
        }
    }
}