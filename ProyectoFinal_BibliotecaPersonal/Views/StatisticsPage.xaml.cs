using ProyectoFinal_BibliotecaPersonal.ViewModels;

namespace ProyectoFinal_BibliotecaPersonal.Views;

public partial class StatisticsPage : ContentPage
{
    private StatisticsViewModel vm;
    public StatisticsPage()
	{
		InitializeComponent();
        vm = new StatisticsViewModel();
        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await vm.LoadStats();
        graphicsView.Invalidate();
    }

}