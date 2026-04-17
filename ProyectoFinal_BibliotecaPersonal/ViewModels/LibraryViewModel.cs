
using CommunityToolkit.Mvvm.Input;
using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public class LibraryViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService database;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Book> Books { get; set; }
        public ICommand AddBookCommand { get; set; }
        public ICommand ViewDetailsCommand { get; set; }
        public ICommand MarkAsReadCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
        public ICommand FilterByGenreCommand { get; set; }
        public int TotalBooks => Books?.Count ?? 0;
        public int ReadBooks => Books?.Count(b => b.IsRead) ?? 0;

        public LibraryViewModel()
        {
            database = new DatabaseService();
            Books = new ObservableCollection<Book>();
            
            // ... más commands
            _ = LoadBooksAsync();
        }

        private async Task LoadBooksAsync()
        {
            var books = await database.GetBooksAsync();
            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
    }
}
