using CasLibraryETL.Services;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║       CasLibraryETL Simulation       ║");
Console.WriteLine("║  Extract → Transform → Load          ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.WriteLine();

// --- Configuration ---
var csvPath = Path.Combine(AppContext.BaseDirectory, "Data", "LegacyBooks.csv");
var apiBaseUrl = args.Length > 0 ? args[0] : "http://localhost:8080";

Console.WriteLine($"  CSV Source : {csvPath}");
Console.WriteLine($"  API Target : {apiBaseUrl}");
Console.WriteLine();

// --- EXTRACT ---
var extractService = new ExtractService(csvPath);
var legacyBooks = extractService.Extract();

// --- TRANSFORM ---
var transformService = new TransformService();
var books = transformService.Transform(legacyBooks);

// --- LOAD ---
var loadService = new LoadService(apiBaseUrl);
await loadService.LoadAsync(books);

Console.WriteLine("  ETL Pipeline finished.");
