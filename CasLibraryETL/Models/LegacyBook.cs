namespace CasLibraryETL.Models
{
    // Represents the raw row from LegacyBooks.csv
    public class LegacyBook
    {
        public string Id { get; set; } = string.Empty;
        public string Book_Title { get; set; } = string.Empty;
        public string Writer { get; set; } = string.Empty;
        public string Book_Type { get; set; } = string.Empty;
        public string Is_Available { get; set; } = string.Empty;
        public string Year_Pub { get; set; } = string.Empty;
    }
}
