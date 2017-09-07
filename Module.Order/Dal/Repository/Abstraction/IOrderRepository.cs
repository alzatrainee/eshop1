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

        /// <summary>
        /// Vraci posledni provedenou objednavku
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        NewOrder GetNewOrder(int id_user);

        /// <summary>
        /// Vraci list vsech objednavek
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        List<NewOrder> GetNewOrderList(int id_user);

        /// <summary>
        /// Vrati prvni provedenou objednavku
        /// </summary>
        /// <param name="id_ord"></param>
        /// <returns></returns>
        NewOrder GetSpecificOrder(int id_ord);
    }
}
