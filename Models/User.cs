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
        public string Email { get; set; }
        public bool isAdmin { get; set; }
    }
}