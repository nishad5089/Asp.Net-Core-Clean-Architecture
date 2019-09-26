﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20190922123644_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("AuthorId");

                    b.HasIndex("CountryId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Domain.Entities.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DatePublished");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Domain.Entities.BookAuthor", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("AuthorId");

                    b.HasKey("BookId", "AuthorId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("AuthorId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("Domain.Entities.BookCategory", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("CategoryId");

                    b.HasKey("BookId", "CategoryId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("CategoryId");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoryID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<string>("Headline")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Rating");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<int>("ReviewerId");

                    b.HasKey("ReviewId");

                    b.HasIndex("BookId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Domain.Entities.Reviewer", b =>
                {
                    b.Property<int>("ReviewerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("ReviewerId");

                    b.ToTable("Reviewers");
                });

            modelBuilder.Entity("Domain.Entities.Author", b =>
                {
                    b.HasOne("Domain.Entities.Country", "Country")
                        .WithMany("Authors")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Entities.BookAuthor", b =>
                {
                    b.HasOne("Domain.Entities.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("FK_BookAuthors_Authors");

                    b.HasOne("Domain.Entities.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BookAuthors_Books");
                });

            modelBuilder.Entity("Domain.Entities.BookCategory", b =>
                {
                    b.HasOne("Domain.Entities.Book", "Book")
                        .WithMany("BookCategories")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BookCategories_Books");

                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("BookCategories")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_BookCategories_Categories");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.HasOne("Domain.Entities.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_Book_Reviews")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Entities.Reviewer", "Reviewer")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewerId")
                        .HasConstraintName("FK_Reviewer_Reviews")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
