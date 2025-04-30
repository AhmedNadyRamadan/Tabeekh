using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tabeekh.Models;

namespace Tabeekh.DTOs
{
    public class OrderDTO
    {
        [JsonIgnore]
        public Guid Delivery_Id { get; set; }
        public Guid Customer_Id { get; set; }
        [JsonIgnore]
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string Address { get; set; }
        public List<Order_items> Items { get; set; } = new List<Order_items>();

    }
}