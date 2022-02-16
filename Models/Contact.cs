using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBlog.Models
{
    public class Contact
    {
        [Key]
        [Required (ErrorMessage="Name cannot be blank")]
        public string Name { get; set; }
        [Required (ErrorMessage="Email cannot be blank")]
        public string Email { get; set; }
        [Required (ErrorMessage="Subject cannot be blank")]
        public string Subject { get; set; }

        public bool Status { get; set; }
    }
}