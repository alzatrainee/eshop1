using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface IOrderRepository
    {
        NewOrder AddNewOrder(NewOrder order);
        NewOrder UpdateNewOrder(NewOrder update);
        NewOrder GetNewOrder(int id_user);
        List<NewOrder> GetNewOrderList(int id_user);
        NewOrder GetSpecificOrder(int id_ord);
    }
}
