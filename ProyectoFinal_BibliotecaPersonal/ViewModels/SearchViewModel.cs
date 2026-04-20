using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly BookApiService apiService;
        private readonly DatabaseService database;
        public ObservableCollection<BookSearchResult> Results { get; } = new();

        private string query;
        public string Query
        {
            get => query;
            set => SetProperty(ref query, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand AddBookFromApiCommand { get; }

        public SearchViewModel()
        {
            apiService = new BookApiService();
            database = new DatabaseService();

            SearchCommand = new AsyncRelayCommand(Search);
            AddBookFromApiCommand = new AsyncRelayCommand<BookSearchResult>(AddBook);
        }

        private async Task Search()
        {
            await Shell.Current.DisplayAlert("Debug", $"Buscando: {Query}", "OK");

            Results.Clear();

            if (string.IsNullOrWhiteSpace(Query))
                return;

            var books = await apiService.SearchBooksAsync(Query);

            foreach (var book in books)
            {
                Results.Add(book);
            }
        }

        private async Task AddBook(BookSearchResult result)
        {
            var detail = await apiService.GetBookDetailAsync(result.Id);

            if (detail == null) return;

            var newBook = new Book
            {
                Title = detail.Title,
                Author = detail.Author,
                ISBN = detail.ISBN,
                Year = detail.PublishedYear,
                Pages = detail.PageCount,
                Genre = detail.Categories?.FirstOrDefault(),
                CoverUrl = detail.CoverUrl,
                IsRead = false,
                Rating = 0,
                Notes = "",
                DateAdded = DateTime.Now
            };

            await database.SaveBookAsync(newBook);

            await Shell.Current.DisplayAlert("Éxito", "Libro agregado", "OK");
        }
    }
}
