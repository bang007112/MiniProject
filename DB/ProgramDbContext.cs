using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BBlog.DB
{
    
    public class ProgramDbContext : DbContext
    {
        public ProgramDbContext() { }
        public ProgramDbContext(DbContextOptions<ProgramDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlogComment>().HasKey(table => new
            {
                table.BlogID,
                table.CommentID
            });
        }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogInfo> BlogInfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}