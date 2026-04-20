namespace ProyectoFinal_BibliotecaPersonal.Drawables
{
    public class StatisticsDrawable : IDrawable
    {
        public int TotalBooks { get; set; }
        public int ReadBooks { get; set; }
        public int UnreadBooks { get; set; }
        public int TotalPagesRead { get; set; }
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

        private void DrawPieChart(ICanvas canvas, RectF rect)
        {
            float centerX = rect.Width / 2;
            float centerY = 120;
            float radius = 80;

            float total = ReadBooks + UnreadBooks;
            if (total == 0) return;

            float readAngle = ((float)ReadBooks / total) * 360;

            // Leídos (verde)
            canvas.FillColor = Colors.Green;
            canvas.FillArc(centerX - radius, centerY - radius,
                           radius * 2, radius * 2,
                           0, readAngle, true);

            // Pendientes (gris)
            canvas.FillColor = Colors.Gray;
            canvas.FillArc(centerX - radius, centerY - radius,
                           radius * 2, radius * 2,
                           readAngle, 360 - readAngle, true);

            // Texto
            canvas.FontSize = 14;
            canvas.FontColor = Colors.Black;
            canvas.DrawString($"Leídos: {ReadBooks}",
                centerX - 60, centerY + radius + 10, 120, 20,
                HorizontalAlignment.Center, VerticalAlignment.Center);
        }
       
        private void DrawBarChart(ICanvas canvas, RectF rect)
        {
            if (BooksByGenre == null || BooksByGenre.Count == 0)
                return;

            float startY = 250;
            float barHeight = 20;
            float spacing = 10;

            int max = BooksByGenre.Values.Max();

            int index = 0;
            foreach (var item in BooksByGenre)
            {
                float y = startY + index * (barHeight + spacing);

                float barWidth = (item.Value / (float)max) * (rect.Width - 150);

                // Barra
                canvas.FillColor = Colors.Blue;
                canvas.FillRectangle(120, y, barWidth, barHeight);

                // Texto género
                canvas.FontSize = 12;
                canvas.FontColor = Colors.Black;
                canvas.DrawString(item.Key, 0, y, 110, barHeight,
                    HorizontalAlignment.Right, VerticalAlignment.Center);

                // Cantidad
                canvas.DrawString(item.Value.ToString(),
                    125 + barWidth, y, 50, barHeight,
                    HorizontalAlignment.Left, VerticalAlignment.Center);

                index++;
            }
        }
        
        private void DrawStatNumbers(ICanvas canvas, RectF rect)
        {
            float y = rect.Height - 120;

            canvas.FontSize = 18;
            canvas.FontColor = Colors.Black;

            canvas.DrawString($"Total libros: {TotalBooks}",
                0, y, rect.Width, 30,
                HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.DrawString($"Leídos: {ReadBooks}",
                0, y + 30, rect.Width, 30,
                HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.DrawString($"Pendientes: {UnreadBooks}",
                0, y + 60, rect.Width, 30,
                HorizontalAlignment.Center, VerticalAlignment.Center);
            
            canvas.DrawString($"Páginas leídas: {TotalPagesRead}",
                0, y + 90, rect.Width, 30,
                HorizontalAlignment.Center, VerticalAlignment.Center);
        }

    }
}
