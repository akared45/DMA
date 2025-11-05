using System.ComponentModel.DataAnnotations;

namespace CodeFirstClean.Domain.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string? Biography { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
