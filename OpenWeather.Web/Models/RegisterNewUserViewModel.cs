using Microsoft.AspNetCore.Http;
using OpenWeather.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpenWeather.Web.Models
{
    public class RegisterNewUserViewModel : User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile ImageFile { get; set; }
    }
}
