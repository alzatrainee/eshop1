﻿using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
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
    }
}
