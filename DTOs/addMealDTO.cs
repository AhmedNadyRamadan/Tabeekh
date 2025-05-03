using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tabeekh.Models;

namespace Tabeekh.DTOs
{
    public class addMealDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public int Prepration_Time { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }

        public string? Ingredients { get; set; }
        public string? Category { get; set; }
        public string? Recipe { get; set; }

        // needs validation in database
        [EnumDataType(typeof(Unit), ErrorMessage = "Invalid unit specified")]
        public Unit Measure_unit { get; set; }
        public int totalRate { get; set; }

        [EnumDataType(typeof(Day), ErrorMessage = "Invalid day entry")]
        public Day Day { get; set; }
    }
}