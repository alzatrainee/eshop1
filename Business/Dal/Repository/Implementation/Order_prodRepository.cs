﻿using Module.Business.Dal.Context;
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

        public Order_prod AddOrder_prod (Order_prod or_prod)
        {
            _context.Order_prod.Add(or_prod);
            _context.SaveChanges();

            return or_prod;
        }

        public List<Order_prod> getOrderProduct(int id_ord)
        {
            var result = _context.Order_prod.Where(p => p.id_ord == id_ord).ToList();
            return result;
        }
    }
}
