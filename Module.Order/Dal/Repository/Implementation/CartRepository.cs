using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;

namespace Module.Order.Dal.Repository.Implementation
{
    public class CartRepository: ICartRepository
    {
        private readonly OrderDbContext _context;
        public CartRepository(OrderDbContext orderDbContext)
        {
            _context = orderDbContext;
        }



        public void AddToCart(Items entity)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void RemoveFromCart(Items entity)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            //TODO
            throw new NotImplementedException();
        }

        public Cart UpdateCart()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
