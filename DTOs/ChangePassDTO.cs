using System.ComponentModel.DataAnnotations;


namespace Tabeekh.DTOs
{
    public class ChangePassDTO
    {
        public string email { get; set; }
        public string password { get; set; }
        
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain one lowercase letter, one uppercase letter, one number, and one special character.")]
        public string newPassword { get; set; }
    }
}