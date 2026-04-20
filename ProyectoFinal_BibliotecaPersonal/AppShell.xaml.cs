using ProyectoFinal_BibliotecaPersonal.Views;

namespace ProyectoFinal_BibliotecaPersonal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("add", typeof(AddBookPage));
            Routing.RegisterRoute("edit", typeof(BookDetailPage));
        }
    
    }
}
