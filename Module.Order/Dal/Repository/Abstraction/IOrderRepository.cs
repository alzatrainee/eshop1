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
        NewOrder FindAddress(int id_user);
    }
}
