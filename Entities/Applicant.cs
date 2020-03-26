using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DrugManufacturing.Entities
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Drug> Drugs { get; set; }
    }
}
