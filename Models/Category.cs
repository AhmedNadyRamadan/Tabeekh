using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tabeekh.Models
{
    [Index(nameof(Name))]
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}