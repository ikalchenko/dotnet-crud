using System;
using System.ComponentModel.DataAnnotations;

namespace DrugManufacturing.Models
{
    public class Registration
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password1 { get; set; }

        [Required]
        public string Password2 { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string Country { get; set; }

        [Required]
        [MinLength(2)]
        public string City { get; set; }

        [Required]
        [MinLength(2)]
        public string StreetAddress { get; set; }

        [Required]
        public string RegistrationType { get; set; }
    }
}
