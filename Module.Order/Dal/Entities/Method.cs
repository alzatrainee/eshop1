using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Method
    {
        [Key]
        public int id_meth { get; set; }
        public string name { get; set; }
    }
}
