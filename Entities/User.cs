using System;
using System.ComponentModel.DataAnnotations;

namespace DrugManufacturing.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Applicant Applicant { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
