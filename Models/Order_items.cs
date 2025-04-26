using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tabeekh.Models
{
    public class Order_items
    {
        
        public Guid Id { get; set; }
        public Guid MealId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public string? Note { get; set; }

        public Delivery_Cust_Meal_Order Order { get; set; }
    }
}