using ProyectoFinal_BibliotecaPersonal.ViewModels;

namespace ProyectoFinal_BibliotecaPersonal.Views;

public partial class AddBookPage : ContentPage
{
	public AddBookPage()
	{
		InitializeComponent();
        BindingContext = new AddBookViewModel();
    }
}