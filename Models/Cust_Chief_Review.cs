using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tabeekh.Models
{
    public class Cust_Chief_Review
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Chief")]
        public Guid Chief_Id { get; set; }
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }

        public int Rate { get; set; }
        public string Comment { get; set; }

        public Chief? Chief { get; set; }
        public Customer? Customer{ get; set; }
    }
}
