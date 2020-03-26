using System;
using System.ComponentModel.DataAnnotations;

namespace DrugManufacturing.Models
{
    public class Login
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
