using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugManufacturing.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<DrugManufacturer> DrugManufacturers { get; set; }
    }
}
