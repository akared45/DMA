using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeFirstClean.Models
{
    public class Ward
    {
        [Key]
        public int WardId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public int Capacity { get; set; }

        public ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();
    }
}
