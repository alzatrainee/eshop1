using Module.Business.Dal.Entities;
using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.OrderViewModels
{
    public class GATransaction
    {
        public NewOrder Order { get; set; }
        public List<Cart_pr> Items {get;set;}

        public GATransaction() { }
        public GATransaction(NewOrder Order, List<Cart_pr> Items)
        {
            this.Order = Order;
            this.Items = Items;
        }


    }
}
