using System.ComponentModel.DataAnnotations;

namespace Micro.Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Quality { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Colour { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        //public Category Category { get; set; }
    }
}
