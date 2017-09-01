using Alza.Core.Module.Http;
using Catalog.Business;
using Module.Business.Business.Entity;
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
        private CatalogService _catalogservice;

        public BusinessService(
            ICart_prRepository cart_prRepo,
            ICartRepository cartRepo,
            IOrder_prodRepository order_prodRepo,
            CatalogService catalogservice
             )
        {
            _cart_prRepo = cart_prRepo;
            _cartRepo = cartRepo;
            _order_prodRepo = order_prodRepo;
            _catalogservice = catalogservice; 
        }


        public AlzaAdminDTO GetCartItem(Cart_pr item)
        {
            Cart_pr result = _cart_prRepo.GetCartItem(item);
            
            //result.Colour = _catalogservice.GetColour(item.id_col);
            result.Size = _catalogservice.GetSize(item.id_si);
            result.Product = _catalogservice.GetProduct(item.id_pr);
            

            return AlzaAdminDTO.Data(result);
        }




        public AlzaAdminDTO AddToCart(Cart_pr entity)
        {
            Cart_pr cart_pr;
            cart_pr = _cart_prRepo.GetCartItem(entity);

            if (cart_pr == null)
            {
                cart_pr = new Cart_pr(entity.id_car, entity.id_pr, 1, entity.id_si, entity.id_col);
                _cart_prRepo.AddCartItem(cart_pr);
                
            }
            else
            {
                cart_pr.amount++;
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
            foreach(var item in result){
                //item.Colour = _catalogservice.GetColour(item.id_col);
                item.Size = _catalogservice.GetSize(item.id_si);
                item.Product = _catalogservice.GetProduct(item.id_pr);
            }
            

            return result;
        }

        public List<Order_prod> getOrderProduct(int id_ord)
        {
            var result = _order_prodRepo.getOrderProduct(id_ord);
            return result;
        }

    }
}
