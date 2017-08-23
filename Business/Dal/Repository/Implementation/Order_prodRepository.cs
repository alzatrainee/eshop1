using Module.Business.Dal.Context;
using Module.Business.Dal.Entity;
using Module.Business.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Business.Dal.Repository.Implementation
{
    public class Order_prodRepository : IOrder_prodRepository
    {
        private readonly BusinessDbContext _context;
        public Order_prodRepository (BusinessDbContext context)
        {
            _context = context;
        }

    }
}
