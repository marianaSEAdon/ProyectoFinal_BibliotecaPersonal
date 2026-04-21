
using CommunityToolkit.Mvvm.Input;
using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;
using System.Windows.Input;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public class AddBookViewModel
    {
        private readonly DatabaseService database;

        public Book Book { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddBookViewModel()
        {
            database = new DatabaseService();

            Book = new Book
            {
                DateAdded = DateTime.Now,
                Rating = 1
            };

            SaveCommand = new RelayCommand(async () => await Save());
            CancelCommand = new RelayCommand(async () => await Cancel());
        }

        private async Task Save()
        {

            if (string.IsNullOrWhiteSpace(Book.Title))
            {
                await Shell.Current.DisplayAlert("Error", "El título es obligatorio", "OK");
                return;
            }

            await database.SaveBookAsync(Book);

            await Shell.Current.GoToAsync(".."); 
        }

        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
