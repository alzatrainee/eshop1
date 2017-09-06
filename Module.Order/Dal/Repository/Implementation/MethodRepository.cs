using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Order.Dal.Repository.Implementation
{
    public class MethodRepository : IMethodRepository
    {
        private readonly OrderDbContext _context;
        public MethodRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Method GetPaymentMethod(int id_meth)
        {
            var result = _context.Method.Where(p => p.id_meth == id_meth).FirstOrDefault();
            return result;
        }
    }
}
