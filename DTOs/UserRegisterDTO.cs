using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Tabeekh.Models;
using Tabeekh.Validators;

namespace Tabeekh.DTOs
{
    [Index(nameof(Email), nameof(Phone), IsUnique = true)]

    public class UserRegisterDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Email is not in a valid format(e.g., example@domain.com)")]

        public string Email { get; set; }
        [Required]
        [RegularExpression("^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone number should be in Egyption format")]

        public string Phone { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain one lowercase letter, one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }
        [ConfirmPassword(nameof(ConfirmPassword))]
        public string ConfirmPassword { get; set; }
        public byte[] Photo { get; set; }

        public string Address { get; set; }
        [Required]
        public UserType Role { get; set; }
    }
}
