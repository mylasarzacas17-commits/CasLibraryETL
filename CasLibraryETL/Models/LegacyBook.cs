using CsvHelper.Configuration.Attributes;

namespace CasLibraryETL.Models
{
    // Represents the raw row from LegacyBooks.csv
    public class LegacyBook
    {
        [Name("id")]
        public string Id { get; set; } = string.Empty;

        [Name("book_title")]
        public string Book_Title { get; set; } = string.Empty;

        [Name("writer")]
        public string Writer { get; set; } = string.Empty;

        [Name("book_type")]
        public string Book_Type { get; set; } = string.Empty;

        [Name("is_available")]
        public string Is_Available { get; set; } = string.Empty;

        [Name("year_pub")]
        public string Year_Pub { get; set; } = string.Empty;
    }
}
