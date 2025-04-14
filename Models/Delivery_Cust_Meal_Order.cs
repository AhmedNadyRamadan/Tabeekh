using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tabeekh.Models
{

    public class Delivery_Cust_Meal_Order
    {
        [Key]   
        public int Id { get; set; }
        [ForeignKey("Delivery")]
        public Guid Delivery_Id { get; set; }
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        [ForeignKey("Meal")]
        public Guid Meal_Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public float Quantity { get; set; }
        public string? Note { get; set; }

    }
}
