using Alza.Core.Module.Http;
using Catalog.Dal.Repository;
using Catalog.Dal.Repository.Abstraction;
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
        private IColourRepository _colourRepo;
        private ISizeRepository _sizeRepo;
        private IProductRepository _productRepo;
        private IImageRepository _imageRepo;
        
        public BusinessService(
            ICart_prRepository cart_prRepo,
            ICartRepository cartRepo,
            IOrder_prodRepository order_prodRepo,
            IColourRepository colourRepo,
            ISizeRepository sizeRepo,
            IProductRepository productRepo,
            IImageRepository imageRepo
             )
        {
            _cart_prRepo = cart_prRepo;
            _cartRepo = cartRepo;
            _order_prodRepo = order_prodRepo;
            _colourRepo = colourRepo;
            _sizeRepo = sizeRepo;
            _productRepo = productRepo;
            _imageRepo = imageRepo;
        }


        public AlzaAdminDTO GetCartItem(int id_car, int id_pr)
        {
            var result = _cart_prRepo.GetCartItem(id_car, id_pr);
            return AlzaAdminDTO.Data(result);
        }




        public AlzaAdminDTO AddToCart(int id_car, int id_pr, int id_si, string id_col)
        {
            Cart_pr cart_pr;
            cart_pr = _cart_prRepo.GetCartItem(id_car, id_pr);

            if (cart_pr == null)
            {
                //Konstruktor pro Cart_pr(int id_car, int id_pr, int id_si, string id_col)
                cart_pr = new Cart_pr(id_car, id_pr, 1, id_si, id_col);
                
                _cart_prRepo.AddCartItem(cart_pr);
                
            }
            else
            {
                cart_pr.amount++;
                _cart_prRepo.UpdateCartItem(cart_pr);
            }
            return AlzaAdminDTO.Data(cart_pr);
        }


        //Remove an entire Item from a cart
        public void RemoveFromCart(Cart_pr entity)
        {
            _cart_prRepo.RemoveCartItem(entity);

        }

        //Remove an item (decrease amount)
        public void RemoveCartItem(Cart_pr entity)
        {
            if(entity.amount > 1)
            {
                entity.amount--;
                _cart_prRepo.UpdateCartItem(entity);
            }
            else
            {
                entity.amount = 1;
                _cart_prRepo.UpdateCartItem(entity);
            }

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
            foreach (var cartItem in result)
            {
                cartItem.Product = _productRepo.GetProduct(cartItem.id_pr);
                cartItem.Product.Image = _imageRepo.GetImage(cartItem.Product.id_im);
                
                cartItem.Size = _sizeRepo.GetSize(cartItem.id_si);
             //   cartItem.Colour = _colourRepo.GetColour(cartItem.id_col);
            }

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
