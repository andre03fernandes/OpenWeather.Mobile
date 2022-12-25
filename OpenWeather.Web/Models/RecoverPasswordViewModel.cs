using System.ComponentModel.DataAnnotations;

namespace OpenWeather.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
