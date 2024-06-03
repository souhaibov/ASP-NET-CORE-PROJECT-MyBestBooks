using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBestBooks.Models;

namespace MyBestBooks.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } // to can modify the users table that exists automatically in the database
                                                                     // after the Identity package. needed for the migration...like with product and Category
                                                                     // not with AddIdentityUser but with ExtendIdentityUser (we can name it whatever we want)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder); // that's because key's of itdentity tables are mapped in the OnModelCreating and if this particular method is not called... it will generate errors about necessary key

            modelBuilder.Entity<Category>().HasData(

                new Category { Id = 2, Name = "Helmi", DisplayOrder = 22 },
                new Category { Id = 4, Name = "souhaib", DisplayOrder = 2 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Dvoretsky : the end game",
                    Author = "Chihemak fih",
                    Description = "7aja ma tafhamhech akid",
                    ISBN = "CHESS0055",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Dvoretsky : the middle game",
                    Author = "Chihemak fih",
                    Description = "7aja ma tafhamhech akid",
                    ISBN = "CHESS0055",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Dvoretsky : Understanding the strategy",
                    Author = "Chihemak fih",
                    Description = "7aja ma tafhamhech akid",
                    ISBN = "CHESS0055",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Dvoretsky : the right tactic",
                    Author = "Chihemak fih",
                    Description = "7aja ma tafhamhech akid",
                    ISBN = "CHESS0055",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 4,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Dvoretsky : The rules of the opening",
                    Author = "Chihemak fih",
                    Description = "7aja ma tafhamhech akid",
                    ISBN = "CHESS0055",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 4,
                    ImageUrl = ""
                }
                );



        }

    }
}
