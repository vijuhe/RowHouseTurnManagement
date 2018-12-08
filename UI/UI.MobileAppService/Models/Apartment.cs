using System.ComponentModel.DataAnnotations;

namespace UI.MobileAppService.Models
{
    public class Apartment
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\d+\s*$", ErrorMessage = "Street address must end with numbers.")]
        public string StreetAddress { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Postal code must consist of five numbers.")]
        public int PostalCode { get; set; }
    }
}