using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tabeekh.Models
{
    public class Cust_Chief_Review
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [ForeignKey("Chief")]     
        public Guid Chief_Id { get; set; }
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }

        public int Rate { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public Chief? Chief { get; set; }
        [JsonIgnore]
        public Customer? Customer{ get; set; }
    }
}
