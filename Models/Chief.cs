using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tabeekh.Models
{

    // in future we need to have a photo of chief's national id.
    // email field should be unique

    public class Chief
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int TotalRate { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Email is not in a valid format(e.g., example@domain.com)")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone number should be in Egyption format")]
        public string Phone { get; set; }
        [JsonIgnore]
        public List<Meal>? Meals { get; set; } = new List<Meal>();


    }
}
