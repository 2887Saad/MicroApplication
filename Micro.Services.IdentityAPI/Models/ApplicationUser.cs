using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Micro.Services.IdentityAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
