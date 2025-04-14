using System.ComponentModel.DataAnnotations;
using Tabeekh.Validators;

namespace Tabeekh.Models
{
   public enum UserType
    {
        Chief = 0,
        Customer = 1,
        Delivery = 2,
    }
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        
        public string Password { get; set; }

        public UserType Type { get; set; }
    }
}
