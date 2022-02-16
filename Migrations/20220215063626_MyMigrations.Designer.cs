﻿// <auto-generated />
using BBlog.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BBlog.Migrations
{
    [DbContext(typeof(ProgramDbContext))]
    [Migration("20220215063626_MyMigrations")]
    partial class MyMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BBlog.Models.BlogComment", b =>
                {
                    b.Property<int>("BlogID")
                        .HasColumnType("int");

                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogID", "CommentID");

                    b.ToTable("BlogComments");
                });

            modelBuilder.Entity("BBlog.Models.BlogInfo", b =>
                {
                    b.Property<int>("BlogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BlogContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlogImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlogTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogID");

                    b.ToTable("BlogInfos");
                });

            modelBuilder.Entity("BBlog.Models.Contact", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("BBlog.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlogCommentBlogInfo", b =>
                {
                    b.Property<int>("BlogInfosBlogID")
                        .HasColumnType("int");

                    b.Property<int>("BlogCommentsBlogID")
                        .HasColumnType("int");

                    b.Property<int>("BlogCommentsCommentID")
                        .HasColumnType("int");

                    b.HasKey("BlogInfosBlogID", "BlogCommentsBlogID", "BlogCommentsCommentID");

                    b.HasIndex("BlogCommentsBlogID", "BlogCommentsCommentID");

                    b.ToTable("BlogCommentBlogInfo");
                });

            modelBuilder.Entity("BlogCommentBlogInfo", b =>
                {
                    b.HasOne("BBlog.Models.BlogInfo", null)
                        .WithMany()
                        .HasForeignKey("BlogInfosBlogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBlog.Models.BlogComment", null)
                        .WithMany()
                        .HasForeignKey("BlogCommentsBlogID", "BlogCommentsCommentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
