using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace Market
{
    public class CsvData<T>
    {
        private readonly string path;
        private List<T> records;

        private List<T> Data => records;

        public CsvData(string path)
        {
            this.path = path;
        }

        public void Read()
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            records = csv.GetRecords<T>().ToList();
        }

        private string StringOfRecords()
        {
            var strBuilder = new StringBuilder();
            records.ForEach(record => strBuilder.AppendLine(record.ToString()));
            return strBuilder.ToString();
        }

        public override string ToString()
        {
            return
                $"{nameof(path)}: {path}, {nameof(records)}: \n{StringOfRecords()}";
        }
    }
}