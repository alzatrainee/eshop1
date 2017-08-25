using Alza.Core.Module.Http;
using Module.Business.Dal.Entities;
using Module.Business.Dal.Entity;
using Module.Business.Dal.Repository.Abstraction;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;

namespace Module.Business.Business
{
    public class BusinessService
    {
        private ICart_prRepository _cart_prRepo;
        private ICartRepository _cartRepo;
        private IOrder_prodRepository _order_prodRepo;

        public BusinessService(
            ICart_prRepository cart_prRepo,
            ICartRepository cartRepo,
            IOrder_prodRepository order_prodRepo
             )
        {
            _cart_prRepo = cart_prRepo;
            _cartRepo = cartRepo;
            _order_prodRepo = order_prodRepo;
        }


        public AlzaAdminDTO GetCartItem(Cart_pr cartItem)
        {
            var result = _cart_prRepo.GetCartItem(cartItem);
            return AlzaAdminDTO.Data(result);
        }




        public AlzaAdminDTO AddToCart(Cart_pr cartItem)
        {
            Cart_pr cart_pr;
            cart_pr = _cart_prRepo.GetCartItem(cartItem);

            if (cart_pr == null)
            {
                cart_pr = new Cart_pr();
                cart_pr.id_pr = cartItem.id_pr;
                cart_pr.id_car = cartItem.id_car;
                
                cart_pr.amount = cartItem.amount;

                _cart_prRepo.AddCartItem(cart_pr);
                
            }
            else
            {
                cart_pr.amount+=cartItem.amount;
                _cart_prRepo.UpdateCartItem(cart_pr);
            }
            return AlzaAdminDTO.Data(cart_pr);
        }



        public void RemoveFromCart(Cart_pr entity)
        {
            _cart_prRepo.RemoveCartItem(entity);

        }


        public AlzaAdminDTO UpdateCartItem(Cart_pr entity)
        {
            _cart_prRepo.UpdateCartItem(entity);
            return AlzaAdminDTO.Data(entity);
        }

        //parametr id_car ID Cart 
        public AlzaAdminDTO GetCartItems(int id_car)
        {
            var result = _cart_prRepo.GetCartItems(id_car);
            return AlzaAdminDTO.Data(result);
        }

        public AlzaAdminDTO GetCart(int id_user)
        {
            var result = _cartRepo.GetCart(id_user);
            return AlzaAdminDTO.Data(result);
        }

        public List<Cart_pr> GetConnectCart(int id_user)
        {
            var result =_cart_prRepo.GetConnectCart(id_user);
            return result;
           
        }
        
        public AlzaAdminDTO DeleteCart_pr (Cart_pr item)
        {
            try
            {
                _cart_prRepo.DeleteCart_pr(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        

        public AlzaAdminDTO AddOrder_prod(Order_prod item)
        {
            try
            {
                _order_prodRepo.AddOrder_prod(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public List<Cart_pr> GetProductsCart(int id)
        {
            var result = _cart_prRepo.GetProductsCart(id);
            return result;
        }

    }
}
