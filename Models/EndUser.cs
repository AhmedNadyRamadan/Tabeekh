
using System.ComponentModel.DataAnnotations;

namespace Tabeekh.Models
{
   public enum UserType
    {
        Chief = 0,
        Customer = 1,
        Delivery = 2,
        Admin = 3
    }
    public class EndUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        [EnumDataType(typeof(UserType), ErrorMessage = "Invalid Role specified")]
        public UserType Role { get; set; }
    }
}
