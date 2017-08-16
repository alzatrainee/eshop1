using Alza.Core.Module.Http;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Business
{
    public class OrderService
    {
        private ICart_prRepository _cart_prRepo;

        public OrderService(
            ICart_prRepository cart_prRepo
            )
        {
            _cart_prRepo = cart_prRepo;
        }


        public AlzaAdminDTO GetCartItem(int id_car, int id_pr)
        {
            var result =_cart_prRepo.GetCartItem(id_car, id_pr);
            return AlzaAdminDTO.Data(result);
        }


        public AlzaAdminDTO AddToCart(Cart_pr entity)
        {
            Cart_pr cart_pr = new Cart_pr();
            cart_pr = _cart_prRepo.GetCartItem(entity.id_car,entity.id_pr);

            if (cart_pr == null)
            {
                _cart_prRepo.AddCartItem(entity);
            }
            else
            {
                cart_pr.ammount++;
                _cart_prRepo.UpdateCartItem(entity);
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
    }
}
