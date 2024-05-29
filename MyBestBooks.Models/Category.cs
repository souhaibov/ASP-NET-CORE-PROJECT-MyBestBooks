using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBestBooks.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [DisplayName("Category Name")]
        public required string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
