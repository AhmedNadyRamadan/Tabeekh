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
        // name
        // price
        [JsonIgnore]

        public Guid Id { get; set; }
        public Guid MealId { get; set; }
        public float Quantity { get; set; }
        [JsonIgnore ]
        [ForeignKey("Delivery_Cust_Meal_Order")]
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Delivery_Cust_Meal_Order? Delivery_Cust_Meal_Order { get; set; }
        public float Price { get; set; }
    }
}