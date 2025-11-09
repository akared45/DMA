using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace CodeFirstClean.Models
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        [Required, StringLength(200)]
        public string Location { get; set; } = null!;

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
