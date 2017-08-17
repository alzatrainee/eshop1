using Alza.Module.UserProfile.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Order
    {
        [Key]
        public int id_ord { get; set; }

        [Required]
        public int id_st { get; set; }
        public virtual Status Status { get; set; }

        [Required]
        public int id_us { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int id_ad { get; set; }
        public virtual Address Address { get; set; }

        [ForeignKey("Address")]
        public int id_fad { get; set; }
        public virtual Address F_Address { get; set; }
    }
}
