using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Tabeekh.Models
{

    public class Delivery_Cust_Meal_Order
    {
        [Key]   
        public Guid Id { get; set; }
        [ForeignKey("Delivery")]
        public Guid Delivery_Id { get; set; }
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }

    }
}
