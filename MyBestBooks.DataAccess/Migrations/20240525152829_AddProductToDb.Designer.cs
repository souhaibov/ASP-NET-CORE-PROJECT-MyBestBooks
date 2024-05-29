﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBestBooks.DataAccess.Data;

#nullable disable

namespace MyBestBooks.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240525152829_AddProductToDb")]
    partial class AddProductToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.3.24172.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyBestBooks.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            DisplayOrder = 22,
                            Name = "Helmi"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 2,
                            Name = "souhaib"
                        });
                });

            modelBuilder.Entity("MyBestBooks.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Chihemak fih",
                            CategoryId = 2,
                            Description = "7aja ma tafhamhech akid",
                            ISBN = "CHESS0055",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Dvoretsky : the end game"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Chihemak fih",
                            CategoryId = 2,
                            Description = "7aja ma tafhamhech akid",
                            ISBN = "CHESS0055",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Dvoretsky : the middle game"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Chihemak fih",
                            CategoryId = 2,
                            Description = "7aja ma tafhamhech akid",
                            ISBN = "CHESS0055",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Dvoretsky : Understanding the strategy"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Chihemak fih",
                            CategoryId = 4,
                            Description = "7aja ma tafhamhech akid",
                            ISBN = "CHESS0055",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Dvoretsky : the right tactic"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Chihemak fih",
                            CategoryId = 4,
                            Description = "7aja ma tafhamhech akid",
                            ISBN = "CHESS0055",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Dvoretsky : The rules of the opening"
                        });
                });

            modelBuilder.Entity("MyBestBooks.Models.Product", b =>
                {
                    b.HasOne("MyBestBooks.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
