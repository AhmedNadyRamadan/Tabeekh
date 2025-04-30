using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Tabeekh.Models
{

    public enum Unit { 
        Kilogram = 0,
        Piece = 1,
        Other = 2,

    }

    public enum Day  {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        General =7 
    }
    public class Meal
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public int Prepration_Time { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }

        public string? Ingredients { get; set; }
        public string? Recipe { get; set; }

        // needs validation in database
        [EnumDataType(typeof(Unit), ErrorMessage = "Invalid unit specified")]
        public Unit Measure_unit { get; set; }

        [ForeignKey("Chief")]
        [JsonIgnore]
        public Guid Chief_Id { get; set; }
        [JsonIgnore]
        public Chief? Chief { get; set; }

        // [EnumDataType(typeof(Day), ErrorMessage = "Invalid day entry")]
        public string Days { get; set; }

    }
}
