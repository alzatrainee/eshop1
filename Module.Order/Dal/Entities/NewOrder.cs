using Alza.Module.UserProfile.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class NewOrder
    {
        public NewOrder (int id_us, int id_st, int id_ad, int id_sh)
        {
            this.id_us = id_us;
            this.id_st = id_st;
            this.id_ad = id_ad;
            this.id_sh = id_sh;
        }

        [Key]
        public int id_ord { get; set; }

        public int id_us { get; set; }

        public int id_st { get; set; }
        public virtual Status Status { get; set; }

        public int id_ad { get; set; }
        public virtual Address Address { get; set; }

        [ForeignKey("Address")]
        public int id_fad { get; set; }
        public virtual Address F_Address { get; set; }

        public int id_pay { get; set; }
        public Payment Payment { get; set; }

        public int id_sh { get; set; }
        public Shipping Shipping { get; set; }
    }
}
