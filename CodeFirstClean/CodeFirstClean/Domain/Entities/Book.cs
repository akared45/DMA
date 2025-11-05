using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstClean.Domain.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(100)]
        public string? Genre { get; set; }
        [Range(1000, 9999)]
        public int PublicationYear { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
