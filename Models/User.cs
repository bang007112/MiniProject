using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BBlog.Models
{
    public class User
    {
        [Key]
        [Required (ErrorMessage="Username cannot be blank")]
        public string Username { get; set; }
        public string Avatar { get; set; }
        
        [Required (ErrorMessage="Email cannot be blank")]
        [MaxLength(100)]
        [RegularExpression(@"^\w+([\.-]?\w+)*@*(gmail\.com)$", ErrorMessage = "Email format invalid")]
        public string Email { get; set; }

        [Required (ErrorMessage="Password cannot be blank")]
        [DataType(DataType.Password)]
        [MaxLength(32)]
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}