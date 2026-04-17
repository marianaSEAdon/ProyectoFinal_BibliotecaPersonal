
namespace ProyectoFinal_BibliotecaPersonal.Models
{
    public class BookDetail
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int PageCount { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public List<string> Categories { get; set; }
    }
}
