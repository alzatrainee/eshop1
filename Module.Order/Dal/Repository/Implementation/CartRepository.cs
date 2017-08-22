using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Catalog.Dal.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Module.Order.Dal.Repository.Implementation
{
    public class CartRepository: ICartRepository
    {
        private readonly OrderDbContext _context;
        public CartRepository(OrderDbContext orderDbContext)
        {
            _context = orderDbContext;
        }

        public Cart GetCart(int id_user)
        {
            var cart = _context.Cart
                .Where(s => s.id_user == id_user)
                .FirstOrDefault();
            return cart;
        }

        public Cart AddCart(Cart entity)
        {

            _context.Cart.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void RemoveCart(Cart entity)
        {
            _context.Cart.Remove(entity);
            _context.SaveChanges();
        }

        public Cart UpdateCart()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
