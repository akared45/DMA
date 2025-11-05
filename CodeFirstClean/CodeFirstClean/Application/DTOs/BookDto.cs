namespace CodeFirstClean.Application.DTOs
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string? Genre { get; set; }
        public int PublicationYear { get; set; }
        public int AuthorId { get; set; }
    }
}
