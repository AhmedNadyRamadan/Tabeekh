using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tabeekh.DTOs
{
    public class OrderItemsDTO
    {
        public Guid MealId { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public float Measure_unit { get; set; }
        public float Price { get; set; }
    }
}