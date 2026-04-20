
using ProyectoFinal_BibliotecaPersonal.Models;
using System.Text.Json;


namespace ProyectoFinal_BibliotecaPersonal.Services
{
    public class BookApiService
    {
        private readonly HttpClient httpClient;
        private const string API_URL = "https://www.googleapis.com/books/v1/volumes";
        public BookApiService()
        {
            httpClient = new HttpClient();
        }
        public async Task<List<BookSearchResult>> SearchBooksAsync(string query)
        {
            var url = $"{API_URL}?q={Uri.EscapeDataString(query)}";

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<BookSearchResult>();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<GoogleBooksResponse>(json, options);

            var books = result?.Items?.Select(item => new BookSearchResult
            {
                Id = item.Id,
                Title = item.VolumeInfo?.Title,
                Author = item.VolumeInfo?.Authors != null
                    ? string.Join(", ", item.VolumeInfo.Authors)
                    : "Autor desconocido",
                ThumbnailUrl = item.VolumeInfo?.ImageLinks?.Thumbnail
            }).ToList();

            return books ?? new List<BookSearchResult>();
        }

        public async Task<BookDetail> GetBookDetailAsync(string bookId)
        {
            var url = $"{API_URL}/{bookId}";

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var item = JsonSerializer.Deserialize<GoogleBookItem>(json, options);

            if (item == null)
                return null;

            int year = 0;

            if (!string.IsNullOrEmpty(item.VolumeInfo?.PublishedDate))
            {
                var datePart = item.VolumeInfo.PublishedDate.Substring(0, 4);
                int.TryParse(datePart, out year);
            }

            return new BookDetail
            {
                Title = item.VolumeInfo?.Title,
                Author = item.VolumeInfo?.Authors != null
                    ? string.Join(", ", item.VolumeInfo.Authors)
                    : "Autor desconocido",
                Description = item.VolumeInfo?.Description,
                PublishedYear = year,
                PageCount = item.VolumeInfo?.PageCount ?? 0,
                CoverUrl = item.VolumeInfo?.ImageLinks?.Thumbnail,
                Categories = item.VolumeInfo?.Categories ?? new List<string>(),

                // 🔥 ESTO ES CLAVE
                ISBN = item.VolumeInfo?.IndustryIdentifiers?
                    .FirstOrDefault(x => x.Type == "ISBN_13" || x.Type == "ISBN_10")
                    ?.Identifier
            };
        }
    }
}
