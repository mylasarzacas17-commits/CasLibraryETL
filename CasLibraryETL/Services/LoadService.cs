using CasLibraryETL.Models;
using System.Net.Http.Json;

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
            foreach (var book in books)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/v1/books", new
                    {
                        title = book.Title,
                        author = book.Author,
                        genre = book.Genre,
                        available = book.Available,
                        publishedYear = book.PublishedYear,
                    });

                    if (response.IsSuccessStatusCode)
                        Console.WriteLine($"Loaded: {book.Title} ? Created");
                    else
                        Console.WriteLine($"Loaded: {book.Title} ? Failed [{(int)response.StatusCode}]");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Loaded: {book.Title} ? Error: {ex.Message}");
                }
            }
        }
    }
}
