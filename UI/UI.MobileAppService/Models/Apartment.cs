using System.ComponentModel.DataAnnotations;

namespace UI.MobileAppService.Models
{
    public class Apartment
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        [RegularExpression(@"\d{5}", ErrorMessage = "Postal code must contain five numbers.")]
        public int PostalCode { get; set; }
    }
}