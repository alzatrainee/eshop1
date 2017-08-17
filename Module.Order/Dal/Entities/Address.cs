using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Address
    {
        [Key]
        public int id_ad { get; set; }

        [Required]
        public string street { get; set; }

        public string block { get; set; }

        [Required]
        public int house_number { get; set; }

    }
}
