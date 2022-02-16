using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BBlog.Models
{
    public class BlogComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CommentID { get; set;}
        public string CommentDate { get; set;}
        public int Likes { get; set; }

        public string CommentContent { get; set;}

        public string Username { get; set;}
        
        [ForeignKey("BlogInfo")]
        public int BlogID { get; set;}

        public ICollection<BlogInfo> BlogInfos { get; set;}
    }
}