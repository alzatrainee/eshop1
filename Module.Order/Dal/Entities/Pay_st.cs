using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Pay_st
    {
        [Key]
        public int id_st { get; set; }
        public string name { get; set; }
    }
}
