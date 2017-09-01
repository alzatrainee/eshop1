using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Business.Dal.Entity
{
    public class Order_prod
    {
        public Order_prod(int id_ord, int id_pr, int amount, string id_col, int id_si)
        {
            this.id_ord = id_ord;
            this.id_pr = id_pr;
            this.amount = amount;
            this.id_col = id_col;
            this.id_si = id_si;
        }

        public int id_ord { get; set; }
        public int id_pr { get; set; }
        public int amount { get; set; }
        public string id_col { get; set; }
        public int id_si { get; set; }
    }
}
