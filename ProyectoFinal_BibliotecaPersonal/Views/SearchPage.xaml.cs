using ProyectoFinal_BibliotecaPersonal.ViewModels;

namespace ProyectoFinal_BibliotecaPersonal.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
        BindingContext = new SearchViewModel();
    }
}