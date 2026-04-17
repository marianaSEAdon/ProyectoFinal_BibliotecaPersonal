
using ProyectoFinal_BibliotecaPersonal.Models;

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
            // Buscar libros por título, autor, o ISBN
            // Retorna lista de resultados
            throw new NotImplementedException();
        }
        public async Task<BookDetail> GetBookDetailAsync(string bookId)
        {
            // Obtiene detalles completos de un libro
            throw new NotImplementedException();
        }
    }
}
