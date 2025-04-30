using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tabeekh.Models
{
    public class Cust_Meal_Review
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [ForeignKey("Meal")]
        public Guid Meal_Id { get; set; }
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
        public int Rate { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public Meal? Meal { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}
