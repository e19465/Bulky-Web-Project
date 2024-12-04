using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        [DisplayName("Category Name")]
        [MaxLength(30, ErrorMessage = "Maximum category name length is 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Display Order is Required")]
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order for category must between 1 and 100")]
        public int DisplayOrder { get; set; }
    }

}
