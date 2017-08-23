using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Business.Dal.Entity
{
    public class Order_prod
    {
        public Order_prod(int id_ord, int id_pr, int amount)
        {
            this.id_ord = id_ord;
            this.id_pr = id_pr;
            this.amount = amount;
        }

        public int id_ord { get; set; }
        public int id_pr { get; set; }
        public int amount { get; set; }
    }
}
