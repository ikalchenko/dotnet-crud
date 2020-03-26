using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugManufacturing.Models
{
    public class DrugModel
    {
        public int Id { get; set; }

        [MinLength(2)]
        public string TradeName { get; set; }

        [MinLength(2)]
        public string InternationalName { get; set; }

        [MinLength(2)]
        public string Form { get; set; }

        [MinLength(2)]
        public string Formula { get; set; }

    }
}
