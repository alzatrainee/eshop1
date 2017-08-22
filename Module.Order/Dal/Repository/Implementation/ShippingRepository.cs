using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Order.Dal.Repository.Implementation
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly OrderDbContext _context;

        public ShippingRepository (OrderDbContext orderDbContext)
        {
            _context = orderDbContext;
        }

        public Shipping GetShippingName (int id_ship)
        {
            var result = _context.Shipping.Where(p => p.id_ship == id_ship).FirstOrDefault();
            return result;
        }
    }
}
