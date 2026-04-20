using CommunityToolkit.Mvvm.Input;
using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public class BookDetailViewModel: INotifyPropertyChanged
    {
        private readonly DatabaseService database;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Book Book { get; set; }
        public double RatingValue
        {
            get => Book.Rating;
            set
            {
                if (Book.Rating != (int)value)
                {
                    Book.Rating = (int)value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RatingValue)));
                }
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public BookDetailViewModel(Book book)
        {
            database = new DatabaseService();
            Book = book ?? new Book();

            SaveCommand = new RelayCommand(async () => await Save());
            DeleteCommand = new RelayCommand(async () => await Delete());
            CancelCommand = new RelayCommand(async () => await Cancel());
        }

        private async Task Save()
        {
            if (Book.Id == 0)
                Book.DateAdded = DateTime.Now;

            await database.SaveBookAsync(Book);

            await Shell.Current.GoToAsync("..");
        }

        private async Task Delete()
        {
            if (Book.Id == 0)
            {
                await Shell.Current.GoToAsync("..");
                return;
            }

            bool confirm = await Shell.Current.DisplayAlert(
                "Eliminar",
                "¿Seguro que quieres eliminar este libro?",
                "Sí",
                "No");

            if (!confirm) return;

            await database.DeleteBookAsync(Book);
            await Shell.Current.GoToAsync("..");
        }

        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
