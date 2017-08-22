using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Shipping
    {
        [Key]
        public int id_ship { get; set; }
        public string name { get; set; }
    }
}
