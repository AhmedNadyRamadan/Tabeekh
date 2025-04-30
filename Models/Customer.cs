using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tabeekh.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Email is not in a valid format(e.g., example@domain.com)")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone number should be in Egyption format")]
        public string Phone { get; set; }


    }
}
