using ProyectoFinal_BibliotecaPersonal.Models;
using SQLite;

namespace ProyectoFinal_BibliotecaPersonal.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection database;
        public DatabaseService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "library.db");
            database = new SQLiteAsyncConnection(dbPath);
            _ = database.CreateTableAsync<Book>();

        }
        public async Task<int> SaveBookAsync(Book book)
        {
            if (book.Id != 0)
                return await database.UpdateAsync(book);
            else
                return await database.InsertAsync(book);
        }
        public async Task<int> UpdateBookAsync(Book book)
        {
                return await database.UpdateAsync(book);
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            return await database.Table<Book>().ToListAsync();
        }
        public async Task<List<Book>> GetBooksByGenreAsync(string genre)
        {
            return await database.Table<Book>().Where(b => b.Genre == genre).ToListAsync();
        }
        public async Task<List<Book>> GetReadBooksAsync()
        {
            return await database.Table<Book>().Where(b => b.IsRead).ToListAsync();
        }
        public async Task<List<Book>> GetUnreadBooksAsync()
        {
            return await database.Table<Book>().Where(b => !b.IsRead).ToListAsync();
        }
        public async Task DeleteBookAsync(Book book)
        {
            await database.DeleteAsync(book);
        }
        public async Task UpdateReadStatusAsync(Book book) { 
            book.IsRead = !book.IsRead;
            await database.UpdateAsync(book);
        }

        public async Task<(int total, int read, int unread, int pages)> GetStatisticsAsync()
        {
            int totalBooks = await database.Table<Book>().CountAsync();
            int readBooks = await database.Table<Book>().Where(b => b.IsRead).CountAsync();
            int unreadBooks = totalBooks - readBooks;

           
            var readBooksList = await database.Table<Book>()
                                              .Where(b => b.IsRead)
                                              .ToListAsync();

            int totalPages = readBooksList.Sum(b => b.Pages);

            return (totalBooks, readBooks, unreadBooks, totalPages);
        }
    }
}
