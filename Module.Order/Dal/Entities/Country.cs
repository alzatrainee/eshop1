using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Country
    {
        public Country() { }
        [Key]
        public int code { get; set; }

        public string name { get; set; }

    }
}
