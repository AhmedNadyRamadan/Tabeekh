using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tabeekh.Models
{
    [PrimaryKey(nameof(MealId),nameof(CategoryId))]
    public class Meal_Category
    {
        [ForeignKey("Meal")]
        public Guid MealId { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Meal Meal { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }
}