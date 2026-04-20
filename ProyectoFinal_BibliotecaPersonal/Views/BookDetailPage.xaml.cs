using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;
using ProyectoFinal_BibliotecaPersonal.ViewModels;

namespace ProyectoFinal_BibliotecaPersonal.Views;

[QueryProperty(nameof(BookId), "bookId")]
public partial class BookDetailPage : ContentPage
{
    private readonly DatabaseService database;

    public string BookId
    {
        set
        {
            LoadBook(int.Parse(value));
        }
    }

    public BookDetailPage()
    {
        InitializeComponent();
        database = new DatabaseService();
    }

    private async void LoadBook(int id)
    {
        var books = await database.GetBooksAsync();
        var book = books.FirstOrDefault(b => b.Id == id);

        if (book == null)
            book = new Book(); // evita crash

        BindingContext = new BookDetailViewModel(book);
    }
}