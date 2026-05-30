# 📦 CasLibraryETL

An **ETL (Extract → Transform → Load)** simulation that migrates legacy book data into the **CasLibraryNowAPI**.

---

## 🔄 ETL Flow

```
LegacyBooks.csv  →  Extract  →  Transform  →  Load  →  CasLibraryNowAPI
```

### Extract
Reads raw data from `Data/LegacyBooks.csv` using CsvHelper.

### Transform
Cleans and maps legacy fields to the CasLibraryNowAPI `Book` model:

| Legacy Field   | Transformation                        | API Field       |
|----------------|---------------------------------------|-----------------|
| `id`           | Parse to int                          | `Id`            |
| `book_title`   | Title Case                            | `Title`         |
| `writer`       | Title Case                            | `Author`        |
| `book_type`    | Fix typos (e.g. `programmin` → `Programming`) | `Genre` |
| `is_available` | Normalize `yes`/`YES`/`no` → `bool`   | `Available`     |
| `year_pub`     | Parse to int                          | `PublishedYear` |

### Load
POSTs each transformed book to `CasLibraryNowAPI` at `POST /api/v1/books`.

---

## 🚀 How to Run

```bash
# Run against local API
dotnet run --project CasLibraryETL

# Run against deployed API (e.g. Render)
dotnet run --project CasLibraryETL -- https://your-api.onrender.com
```

---

## 📁 Project Structure

```
CasLibraryETL/
├── CasLibraryETL/
│   ├── Data/
│   │   └── LegacyBooks.csv       ← Source data
│   ├── Models/
│   │   ├── LegacyBook.cs         ← Raw CSV model
│   │   └── Book.cs               ← Target API model
│   ├── Services/
│   │   ├── ExtractService.cs     ← Reads CSV
│   │   ├── TransformService.cs   ← Cleans & maps data
│   │   └── LoadService.cs        ← POSTs to API
│   ├── Program.cs                ← ETL orchestrator
│   └── CasLibraryETL.csproj
├── .gitattributes
├── .gitignore
└── README.md
```

---

## 📄 License
MIT © Myla Cas
