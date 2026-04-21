
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

            TotalBooks = stats.total;
            ReadBooks = stats.read;
            UnreadBooks = stats.unread;
            TotalPagesRead = stats.pages;

            var books = await database.GetBooksAsync();

            var grouped = books
                .GroupBy(b => string.IsNullOrWhiteSpace(b.Genre) ? "Sin género" : b.Genre)
                .Select(g => new GenreStat
                {
                    Genre = g.Key,
                    Count = g.Count()
                });

            GenreStats.Clear();
            foreach (var g in grouped)
                GenreStats.Add(g);

            Drawable = new StatisticsDrawable
            {
                TotalBooks = TotalBooks,
                ReadBooks = ReadBooks,
                UnreadBooks = UnreadBooks,
                TotalPagesRead = TotalPagesRead,
                BooksByGenre = GenreStats.Where(g => !string.IsNullOrWhiteSpace(g.Genre))
                .ToDictionary(g => g.Genre, g => g.Count)
            };

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