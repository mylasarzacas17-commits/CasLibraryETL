using CasLibraryETL.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CasLibraryETL.Services
{
    public class ExtractService
    {
        private readonly string _filePath;

        public ExtractService(string filePath)
        {
            _filePath = filePath;
        }

        public List<LegacyBook> Extract()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║         EXTRACT PHASE                ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine($"  Reading from: {_filePath}");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
            };

            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecords<LegacyBook>().ToList();

            Console.WriteLine($"  ✅ Extracted {records.Count} records.\n");

            foreach (var r in records)
                Console.WriteLine($"  [RAW] id={r.Id} | title={r.Book_Title} | writer={r.Writer} | type={r.Book_Type} | available={r.Is_Available} | year={r.Year_Pub}");

            Console.WriteLine();
            return records;
        }
    }
}
