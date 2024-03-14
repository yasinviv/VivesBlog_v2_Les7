namespace VivesBlog.Ui.Mvc.Models
{
    public class Article
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public int? AuthorId { get; set; }
        public Person Author { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        public required string Description { get; set; }
        public required string Content { get; set; }
    }
}
