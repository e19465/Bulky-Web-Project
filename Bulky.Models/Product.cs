using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Maximum length for product title is 100 characters.")]
        [Required(ErrorMessage = "Title of the product is required")]
        public required string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Maximum length for description is 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "ISBN for product is required")]
        [StringLength(30, ErrorMessage = "Maximum length for ISBN is 30 characters.")]
        [Display(Name = "ISBN")]
        public required string Isbn { get; set; }

        [Required(ErrorMessage = "Author of the product is required.")]
        [StringLength(30, ErrorMessage = "Maximum length for Author name is 30 characters.")]
        public required string Author {get; set;}

        [Required(ErrorMessage = "List price is required")]
        [Range(1,5000, ErrorMessage = "List price must be between 1$ - 5000$")]
        [Display(Name = "List Price")]
        public required double ListPrice { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Display (Name = "Price for 1-50")]
        [Range(1, 5000, ErrorMessage = "Price must be between 1$ - 5000$")]
        public required double Price { get; set; }

        [Required(ErrorMessage = "Unit price after buy 50+ items is required")]
        [Display(Name = "Price for 50+")]
        [Range(1, 5000, ErrorMessage = "Price for 50+ must be between 1$ - 5000$")]
        public required double Price50 { get; set; }


        [Required (ErrorMessage = "Unit price after buy 100+ items is required")]
        [Display(Name = "Price for 100+")]
        [Range(1, 5000, ErrorMessage = "Price for 100+ must be between 1$ - 5000$")]
        public required double Price100 { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public required int CategoryId { get; set; }

		[ValidateNever]
		public required Category Category { get; set; }

        [StringLength(400, ErrorMessage = "Maximum length for product image url is 200 characters.")]
        [Display(Name = "Product Image")]
        public string? ImageUrl { get; set; }
    }
}
