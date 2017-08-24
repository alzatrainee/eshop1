using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Implementation
{
    public class AddressRepository : IAddressRepository
    {
        private readonly OrderDbContext _context;
        public AddressRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Address AddAddress(Address order)
        {
            _context.Address.Add(order);
            _context.SaveChanges();

            return order;
        }
    }
}
