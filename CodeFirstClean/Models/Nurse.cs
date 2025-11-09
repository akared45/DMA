using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeFirstClean.Models
{
    public class Nurse
    {
        [Key]
        public int NurseId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public string Certification { get; set; } = null!;

        public int WardId { get; set; }
        public Ward Ward { get; set; } = null!;
    }
}
