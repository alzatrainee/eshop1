using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Payment
    {
        public Payment(int id_meth, int id_st, decimal price)
        {
            this.id_meth = id_meth;
            this.id_st = id_st;
            this.price = price;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_pay { get; set; }
        
        public decimal price { get; set; }
        public int id_meth { get; set; }
        public Method Method { get; set; }

        public int id_st { get; set; }
        public Pay_st Pay_st { get; set; }
    }
}
