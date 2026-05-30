using CasLibraryETL.Models;
using System.Globalization;

namespace CasLibraryETL.Services
{
    public class TransformService
    {
        // Known genre typo corrections
        private static readonly Dictionary<string, string> GenreCorrections = new(StringComparer.OrdinalIgnoreCase)
        {
            { "programmin",   "Programming" },
            { "programming",  "Programming" },
            { "architecture", "Architecture" },
            { "javascript",   "JavaScript" },
        };

        public List<Book> Transform(List<LegacyBook> legacyBooks)
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║         TRANSFORM PHASE              ║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            var books = new List<Book>();
            int index = 1;

            foreach (var legacy in legacyBooks)
            {
                Console.WriteLine($"  Transforming record id={legacy.Id}...");

                // --- ID: parse integer, fallback to index
                int parsedId = int.TryParse(legacy.Id, out int id) ? id : index;

                // --- Title: Title Case
                string title = ToTitleCase(legacy.Book_Title.Trim());
                LogChange("title", legacy.Book_Title, title);

                // --- Author: Title Case
                string author = ToTitleCase(legacy.Writer.Trim());
                LogChange("author", legacy.Writer, author);

                // --- Genre: fix typos + normalize
                string rawGenre = legacy.Book_Type.Trim();
                string genre = GenreCorrections.TryGetValue(rawGenre, out var corrected)
                    ? corrected
                    : ToTitleCase(rawGenre);
                LogChange("genre", rawGenre, genre);

                // --- Available: normalize yes/no → bool
                bool available = legacy.Is_Available.Trim().ToLower() == "yes";
                LogChange("available", legacy.Is_Available, available.ToString());

                // --- PublishedYear: parse integer
                int year = int.TryParse(legacy.Year_Pub.Trim(), out int y) ? y : 0;

                books.Add(new Book
                {
                    Id = parsedId,
                    Title = title,
                    Author = author,
                    Genre = genre,
                    Available = available,
                    PublishedYear = year,
                });

                Console.WriteLine($"  ✅ Transformed → Title: {title} | Author: {author} | Genre: {genre} | Available: {available} | Year: {year}\n");
                index++;
            }

            Console.WriteLine($"  ✅ Transform complete. {books.Count} books ready.\n");
            return books;
        }

        private static string ToTitleCase(string input)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        private static void LogChange(string field, string before, string after)
        {
            if (!string.Equals(before.Trim(), after.Trim(), StringComparison.OrdinalIgnoreCase))
                Console.WriteLine($"    🔄 {field}: \"{before}\" → \"{after}\"");
        }
    }
}
