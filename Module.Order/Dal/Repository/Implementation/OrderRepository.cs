using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Order.Dal.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
       public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public NewOrder AddNewOrder(NewOrder order)
        {
            _context.NewOrder.Add(order);
            _context.SaveChanges();

            return order;
        }

        public NewOrder UpdateNewOrder (NewOrder update)
        {
            _context.NewOrder.Update(update);
            _context.SaveChanges();
            return update;
        }

        /// <summary>
        /// Vraci posledni provedenou objednavku
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public NewOrder GetNewOrder(int id_user)
        {
            var result = _context.NewOrder.Where(p => p.id_us == id_user).LastOrDefault();
            return result;
        }

        /// <summary>
        /// Vraci list vsech objednavek
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public List<NewOrder> GetNewOrderList(int id_user)
        {
            var result = _context.NewOrder.Where(p => p.id_us == id_user).ToList();
            return result;
        }

        /// <summary>
        /// Vrati prvni provedenou objednavku
        /// </summary>
        /// <param name="id_ord"></param>
        /// <returns></returns>
        public NewOrder GetSpecificOrder(int id_ord)
        {
            var result = _context.NewOrder.Where(p => p.id_ord == id_ord).FirstOrDefault();
            return result;
        }
    }
}
