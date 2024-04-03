using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Micro.Web.Models.Dto
{
    public class RegisterationRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
