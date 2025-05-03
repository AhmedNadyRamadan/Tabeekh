using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabeekh.Models;

namespace Tabeekh.DTOs
{
    public class UpdateMealDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public Day Day { get; set; }
        public bool Available { get; set; }
    }
}