using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Dtos
{
    public class CreateItemInputDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [MaxLength(4000)] // Adjust the max length as needed
        public string Description { get; set; }

        //[Url(ErrorMessage = "Image must be a valid URL.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }


        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be a positive integer.")]
        public int Amount { get; set; }
    }
}
