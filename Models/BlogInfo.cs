using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BBlog.Models
{
    public class BlogInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogID { get; set; }
        public DateTime ReleaseDate { get; set; } =  DateTime.Now;
        [Required (ErrorMessage="BlogTitle cannot be blank")]
        public string BlogTitle { get; set; }
        public string BlogImage { get; set; }
        public string BlogContent { get; set; }
        public int Likes { get; set; }
        public ICollection<BlogComment> BlogComments { get; set;}
    }
}