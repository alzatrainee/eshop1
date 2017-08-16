using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Order.Dal.Repository.Implementation
{
    public class Cart_prRepository : ICart_prRepository
    {
        private readonly OrderDbContext _context;
        public Cart_prRepository(OrderDbContext context)
        {
            _context = context;
        }
        public Cart_pr GetCartItem(int id_car,int id_pr)
        {
            var result = _context.Cart_pr.Where(s => s.id_pr == id_pr && s.id_car == id_car).FirstOrDefault(); ;
            return result;

        }
        public Cart_pr AddCartItem(Cart_pr entity)
        {
            Cart_pr cart_pr = new Cart_pr();
            cart_pr = GetCartItem(entity.id_car,entity.id_pr);

            if (cart_pr == null)
            {
                cart_pr.id_car = entity.id_car;
                cart_pr.id_pr = entity.id_pr;
                cart_pr.ammount = 1;
            }
            else
                cart_pr.ammount++;

            _context.Cart_pr.Add(cart_pr);
            _context.SaveChanges();
            return cart_pr;
        }
        
        public void RemoveCartItem(Cart_pr entity)
        {
            _context.Cart_pr.Remove(entity);
            _context.SaveChanges();

        }


        public Cart_pr UpdateCartItem(Cart_pr entity)
        {

            var oldCartItem = _context.Cart_pr.Where(s => s.id_pr == entity.id_pr && s.id_car == entity.id_car);
            _context.Entry(oldCartItem).CurrentValues.SetValues(entity);
            return entity;
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/

        public List<Cart_pr> GetCartItems(int id)
        {
            List<Cart_pr> cartItems = new List<Cart_pr>();
            _context.Cart_pr.Where(s => s.id_car == id).ToList();
            return cartItems;
        }

    }
}
