using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Address FindSpecificAddress(int id_ad)
        {
            var result = _context.Address.Where(p => p.id_ad == id_ad).FirstOrDefault();

            return result;
        }

        public Address FindAddresByIdUser(int id_user)
        {
            var result = _context.Address.Where(p => p.id_ad == id_user).FirstOrDefault();

            return result;
        }

        public Address UpdateAddress(Address update)
        {
            _context.Address.Update(update);
            _context.SaveChanges();
            return update;
        }
    }
}
