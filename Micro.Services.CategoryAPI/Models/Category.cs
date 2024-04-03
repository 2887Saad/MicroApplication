using System.ComponentModel.DataAnnotations;

namespace Micro.Services.CategoryAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type{ get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
