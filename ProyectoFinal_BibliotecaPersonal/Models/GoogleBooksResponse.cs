
namespace ProyectoFinal_BibliotecaPersonal.Models
{
    public class GoogleBooksResponse
    {
        public List<GoogleBookItem> Items { get; set; } 
    }
    public class GoogleBookItem
    {
        public string Id { get; set; }
        public VolumeInfo VolumeInfo { get; set; }
    }
    public class VolumeInfo
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Description { get; set; }
        public string PublishedDate { get; set; }
        public int? PageCount { get; set; }
        public List<string> Categories { get; set; }
        public ImageLinks ImageLinks { get; set; }
        public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
    }

    public class ImageLinks
    {
        public string Thumbnail { get; set; }
    }
    public class IndustryIdentifier
    {
        public string Type { get; set; }
        public string Identifier { get; set; }
    }
}
