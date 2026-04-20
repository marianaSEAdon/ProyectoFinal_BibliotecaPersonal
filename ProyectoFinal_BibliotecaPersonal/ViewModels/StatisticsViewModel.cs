
using ProyectoFinal_BibliotecaPersonal.Drawables;
using ProyectoFinal_BibliotecaPersonal.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService database;
        public event PropertyChangedEventHandler? PropertyChanged;
        public int TotalBooks { get; set; }
        public int ReadBooks { get; set; }
        public int UnreadBooks { get; set; }
        public int TotalPagesRead { get; set; }
        public StatisticsDrawable Drawable { get; set; }

        public ObservableCollection<GenreStat> GenreStats { get; set; }

        public StatisticsViewModel()
        {
            database = new DatabaseService();
            GenreStats = new ObservableCollection<GenreStat>();


            _ = LoadStats();
        }

        public async Task LoadStats()
        {
            var stats = await database.GetStatisticsAsync();

            // 1. Asignar valores
            TotalBooks = stats.total;
            ReadBooks = stats.read;
            UnreadBooks = stats.unread;
            TotalPagesRead = stats.pages;

            // 2. Obtener libros
            var books = await database.GetBooksAsync();

            var grouped = books.GroupBy(b => b.Genre)
                               .Select(g => new GenreStat
                               {
                                   Genre = g.Key,
                                   Count = g.Count()
                               });

            GenreStats.Clear();
            foreach (var g in grouped)
                GenreStats.Add(g);

            // 3. AHORA sí crear el drawable
            Drawable = new StatisticsDrawable
            {
                TotalBooks = TotalBooks,
                ReadBooks = ReadBooks,
                UnreadBooks = UnreadBooks,
                TotalPagesRead = TotalPagesRead,
                BooksByGenre = GenreStats.ToDictionary(g => g.Genre, g => g.Count)
            };

            // 4. Notificar UI
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drawable)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalBooks)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReadBooks)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnreadBooks)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPagesRead)));
        }
    }

    public class GenreStat
    {
        public string Genre { get; set; }
        public int Count { get; set; }
    }
}