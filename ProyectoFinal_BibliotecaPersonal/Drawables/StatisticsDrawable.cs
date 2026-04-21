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
            float padding = 20;

            float sectionHeight = dirtyRect.Height / 3;

            var pieRect = new RectF(0, 0, dirtyRect.Width, sectionHeight);
            var barRect = new RectF(0, sectionHeight, dirtyRect.Width, sectionHeight);
            var statsRect = new RectF(0, sectionHeight * 2, dirtyRect.Width, sectionHeight);

            DrawPieChart(canvas, pieRect);
            DrawBarChart(canvas, barRect);
            DrawStatNumbers(canvas, statsRect);
        }

        private void DrawPieChart(ICanvas canvas, RectF rect)
        {
            float centerX = rect.Center.X;
            float centerY = rect.Center.Y;
            float radius = Math.Min(rect.Width, rect.Height) / 3;

            float total = ReadBooks + UnreadBooks;
            if (total == 0) return;

            float readAngle = ((float)ReadBooks / total) * 360;

            canvas.FillColor = Colors.Green;
            canvas.FillArc(centerX - radius, centerY - radius,
                           radius * 2, radius * 2,
                           0, readAngle, true);

            canvas.FillColor = Colors.Gray;
            canvas.FillArc(centerX - radius, centerY - radius,
                           radius * 2, radius * 2,
                           readAngle, 360 - readAngle, true);

            canvas.FontSize = 14;
            canvas.FontColor = Colors.Black;

            canvas.DrawString($"Leídos: {ReadBooks}",
                rect.X, rect.Bottom - 25, rect.Width, 20,
                HorizontalAlignment.Center, VerticalAlignment.Center);
        }

        private void DrawBarChart(ICanvas canvas, RectF rect)
        {
            if (BooksByGenre == null || BooksByGenre.Count == 0)
                return;

            float padding = 20;
            float barHeight = 20;
            float spacing = 10;

            int max = BooksByGenre.Values.Max();

            float startY = rect.Y + padding;

            int index = 0;
            foreach (var item in BooksByGenre)
            {
                float y = startY + index * (barHeight + spacing);

                float availableWidth = rect.Width - 150;
                float barWidth = (item.Value / (float)max) * availableWidth;

                canvas.FillColor = Colors.Blue;
                canvas.FillRectangle(rect.X + 120, y, barWidth, barHeight);

                canvas.FontSize = 12;
                canvas.FontColor = Colors.Black;

                canvas.DrawString(item.Key,
                    rect.X, y, 110, barHeight,
                    HorizontalAlignment.Right, VerticalAlignment.Center);

                canvas.DrawString(item.Value.ToString(),
                    rect.X + 125 + barWidth, y, 50, barHeight,
                    HorizontalAlignment.Left, VerticalAlignment.Center);

                index++;
            }
        }

        private void DrawStatNumbers(ICanvas canvas, RectF rect)
        {
            float lineHeight = 25;
            float startY = rect.Y + 10;

            canvas.FontSize = 16;
            canvas.FontColor = Colors.Black;

            canvas.DrawString($"Total libros: {TotalBooks}",
                rect.X, startY, rect.Width, lineHeight,
                HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.DrawString($"Leídos: {ReadBooks}",
                rect.X, startY + lineHeight, rect.Width, lineHeight,
                HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.DrawString($"Pendientes: {UnreadBooks}",
                rect.X, startY + lineHeight * 2, rect.Width, lineHeight,
                HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.DrawString($"Páginas leídas: {TotalPagesRead}",
                rect.X, startY + lineHeight * 3, rect.Width, lineHeight,
                HorizontalAlignment.Center, VerticalAlignment.Center);
        }

    }
}
