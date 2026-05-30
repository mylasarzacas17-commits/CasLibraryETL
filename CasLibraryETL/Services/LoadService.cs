using CasLibraryETL.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CasLibraryETL.Services
{
    public class LoadService
    {
        private readonly string _apiBaseUrl;
        private readonly HttpClient _httpClient;

        public LoadService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl.TrimEnd('/');
            _httpClient = new HttpClient();
        }

        public async Task LoadAsync(List<Book> books)
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║           LOAD PHASE                 ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine($"  Target API: {_apiBaseUrl}/api/v1/books\n");

            int success = 0;
            int failed = 0;

            foreach (var book in books)
            {
                try
                {
                    Console.WriteLine($"  📤 Loading: \"{book.Title}\" by {book.Author}...");

                    var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/v1/books", new
                    {
                        title = book.Title,
                        author = book.Author,
                        genre = book.Genre,
                        available = book.Available,
                        publishedYear = book.PublishedYear,
                    });

                    if (response.IsSuccessStatusCode)
                    {
                        var body = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"  ✅ Success [{(int)response.StatusCode}] → {body}\n");
                        success++;
                    }
                    else
                    {
                        var body = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"  ❌ Failed [{(int)response.StatusCode}] → {body}\n");
                        failed++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  ❌ Error loading \"{book.Title}\": {ex.Message}\n");
                    failed++;
                }
            }

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║           ETL SUMMARY                ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine($"  Total Books : {books.Count}");
            Console.WriteLine($"  ✅ Loaded   : {success}");
            Console.WriteLine($"  ❌ Failed   : {failed}");
            Console.WriteLine();
        }
    }
}
