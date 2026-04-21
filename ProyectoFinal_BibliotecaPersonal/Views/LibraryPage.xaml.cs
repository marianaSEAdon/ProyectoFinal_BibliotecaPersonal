using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.ViewModels;

namespace ProyectoFinal_BibliotecaPersonal.Views;

public partial class LibraryPage : ContentPage
{
	public LibraryPage()
	{
		InitializeComponent();
        BindingContext = new LibraryViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is LibraryViewModel vm)
        {
            await vm.LoadBooksAsync();
        }
    }

}