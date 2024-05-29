using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyBestBooks.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [AllowNull]
        public string ISBN { get; set; }

        [AllowNull]
        public string Author { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for +50")]
        [Range(1, 1000)]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price for +100")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        public int CategoryId { get; set; } // table here will not know that's it's a foreign key.
                                            // in order to explicitally define that we need a navigation property to the category table 
        [ForeignKey("CategoryId")] // to say that CategoryId is a foreign key to the Category table
        [ValidateNever]
        public Category Category { get; set; } // it's a navigation to the category table and we call that "Category"
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
