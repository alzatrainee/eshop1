using Module.Business.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Business.Dal.Repository.Abstraction
{
    public interface IOrder_prodRepository
    {
        Order_prod AddOrder_prod(Order_prod or_prod);
    }
}
