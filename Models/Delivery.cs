using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tabeekh.Models
{
    public class Delivery
    {
        [Key]
        public Guid Id { get; set; }
        [Required]

        public string Name { get; set; }

        [Required]
        [RegularExpression("^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone number should be in Egyption format")]
        public string Phone { get; set; }
        [Required]
        public bool Available { get; set; }
    }
}
