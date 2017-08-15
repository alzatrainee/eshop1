using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Module.Order.Dal.Entities
{
    //Cart status
    public class Cart_st
    {
        //ID status
        [Key]
        public int id_st { get; set; }

        [Required]
        public string name { get; set; }
    }
}
