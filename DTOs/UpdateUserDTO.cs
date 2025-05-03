using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tabeekh.DTOs
{
    [Index(nameof(Email), nameof(Phone), IsUnique = true)]
    public class UpdateUserDTO
    {
        public string Username { get; set; }
        [EmailAddress]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Email is not in a valid format(e.g., example@domain.com)")]
        public string Email { get; set; }
        [RegularExpression("^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone number should be in Egyption format")]
        public string Phone { get; set; }
        public byte[] Photo { get; set; }
        public string Address { get; set; }
    }
}