
namespace ProyectoFinal_BibliotecaPersonal.Drawables
{
    public class StatisticsDrawable : IDrawable
    {
        public int TotalBooks { get; set; }
        public int ReadBooks { get; set; }
        public int UnreadBooks { get; set; }
        public Dictionary<string, int> BooksByGenre { get; set; }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // 1. Gráfico circular: Leídos vs Pendientes
            DrawPieChart(canvas, dirtyRect);
            // 2. Gráfico de barras: Libros por género
            DrawBarChart(canvas, dirtyRect);
            // 3. Estadísticas numéricas grandes
            DrawStatNumbers(canvas, dirtyRect);
        }

        private void DrawPieChart(ICanvas canvas, RectF dirtyRect)
        {
            // Círculo mostrando % leídos vs pendientes
        }
        private void DrawBarChart(ICanvas canvas, RectF dirtyRect)
        {
            // Barras por género
        }
        private void DrawStatNumbers(ICanvas canvas, RectF dirtyRect)
        {
            // Números grandes: Total, Leídos, Páginas totales
        }
    }
}
