namespace Test.Domain.Entities
{
    public class Book : Base
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationYear { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }
    }
}
