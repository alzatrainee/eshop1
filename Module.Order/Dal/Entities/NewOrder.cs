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
        public NewOrder() { }
        public NewOrder (int id_us, int id_st, int id_ad, int id_sh, int id_pay)
        {
            this.id_us = id_us;
            this.id_st = id_st;
            this.id_ad = id_ad;
            this.id_sh = id_sh;
            this.id_pay = id_pay;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_ord { get; set; }

        public int id_us { get; set; }

        public int id_st { get; set; }
    //    public Status Status { get; set; }

        [ForeignKey("id_ad")]
        public int id_ad { get; set; }
      //  public Address Address { get; set; }

      //  [ForeignKey("Address")]
       // public int id_fad { get; set; }
       // public  Address F_Address { get; set; }

        [ForeignKey("id_pay")]
        public int id_pay { get; set; }
      //  public Payment Payment { get; set; }

        [ForeignKey("id_sh")]
        public int id_sh { get; set; }
      //  public Shipping Shipping { get; set; }
    }
}
