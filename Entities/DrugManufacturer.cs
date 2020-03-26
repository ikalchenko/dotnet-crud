using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugManufacturing.Entities
{
    public class DrugManufacturer
    {
        public int DrugId { get; set; }
        public Drug Drug { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
