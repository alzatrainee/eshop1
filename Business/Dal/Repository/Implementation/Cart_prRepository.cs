using Module.Business.Dal.Entities;
using System.Collections.Generic;
using System.Linq;
using Module.Business.Dal.Context;
using Module.Business.Dal.Repository.Abstraction;

namespace Module.Business.Dal.Repository.Implementation
{
    public class Cart_prRepository : ICart_prRepository
    {
        private readonly BusinessDbContext _context;
        public Cart_prRepository(BusinessDbContext context)
        {
            _context = context;
        }
        public Cart_pr GetCartItem(Cart_pr cartItem)
        {
            var result = _context.Cart_pr.Where(s => s.id_pr == cartItem.id_pr && 
                                                s.id_car == cartItem.id_car &&
                                                s.id_col == cartItem.id_col &&
                                                s.id_si == cartItem.id_si
                                                ).FirstOrDefault(); ;
            return result;

        }
        public Cart_pr AddCartItem(Cart_pr entity)
        {
            _context.Cart_pr.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        
        public void RemoveCartItem(Cart_pr entity)
        {
            _context.Cart_pr.Remove(entity);
            _context.SaveChanges();

        }


        public Cart_pr UpdateCartItem(Cart_pr entity)
        {

            var oldCartItem = _context.Cart_pr.Where(s => s.id_pr == entity.id_pr && s.id_car == entity.id_car).FirstOrDefault();
            _context.Entry(oldCartItem).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entity;
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/

        public List<Cart_pr> GetCartItems(int id)
        {
            List<Cart_pr> cartItems = new List<Cart_pr>();
            cartItems = _context.Cart_pr.Where(s => s.id_car == id).ToList();
            return cartItems;
        }

        public List<Cart_pr> GetConnectCart(int id_user)
        {
            var result = _context.Cart_pr.Where(p => p.id_car == id_user).ToList();
            return result;
        }

        public void DeleteCart_pr (Cart_pr item)
        {
            var result = _context.Cart_pr.Remove(item);
            _context.SaveChanges();
        }

        public List<Cart_pr> GetProductsCart (int id)
        {
            var result = _context.Cart_pr.Where(p => p.id_car == id).ToList();
            return result;
        }
    }
}
