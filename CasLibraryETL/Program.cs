using CasLibraryETL.Services;

// --- Configuration ---
var csvPath = Path.Combine(AppContext.BaseDirectory, "Data", "LegacyBooks.csv");
var apiBaseUrl = args.Length > 0 ? args[0] : "https://caslibrarynowapi-01.onrender.com/";

// --- EXTRACT ---
var extractService = new ExtractService(csvPath);
var legacyBooks = extractService.Extract();

// --- TRANSFORM ---
var transformService = new TransformService();
var books = transformService.Transform(legacyBooks);

// Print table like classmate's output
Console.WriteLine($"Extracted {books.Count} records.");
foreach (var b in books)
    Console.WriteLine($"{b.Id} | {b.Title} | {b.Author} | {b.Genre} | {b.Available} | {b.PublishedYear} |");

// --- LOAD ---
var loadService = new LoadService(apiBaseUrl);
await loadService.LoadAsync(books);
