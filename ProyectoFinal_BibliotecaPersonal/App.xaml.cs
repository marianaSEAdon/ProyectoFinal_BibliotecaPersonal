using Microsoft.Extensions.DependencyInjection;

namespace ProyectoFinal_BibliotecaPersonal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Application.Current.UserAppTheme = AppTheme.Dark;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}