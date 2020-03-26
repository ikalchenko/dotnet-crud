using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugManufacturing.Entities
{
    public class Drug
    {
        public int Id { get; set; }
        public string TradeName { get; set; }
        public string InternationalName { get; set; }
        public string Form { get; set; }
        public string Formula { get; set; }
        public Applicant Applicant { get; set; }
        public ICollection<DrugManufacturer> DrugManufacturers { get; set; }

    }
}
