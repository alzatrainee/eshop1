using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int id_pay { get; set; }
        
        public decimal price { get; set; }
        public int id_meth { get; set; }
        public int id_st { get; set; }
    }
}
