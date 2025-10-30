namespace Test.Domain.Entities
{
    public class Author : Base
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
