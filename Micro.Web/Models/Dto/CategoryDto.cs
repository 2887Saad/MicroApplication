using System.ComponentModel.DataAnnotations;

namespace Micro.Web
{
    public class CategoryDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
