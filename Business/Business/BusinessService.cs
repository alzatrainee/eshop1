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
        private IComment_LikeRepository _commentLikeRepo;
        private IProduct_LikeRepository _productLikeRepo;

        public BusinessService(
            ICart_prRepository cart_prRepo,
            ICartRepository cartRepo,
            IOrder_prodRepository order_prodRepo,
            CatalogService catalogservice,
            IComment_LikeRepository commentLikeRepo,
            IProduct_LikeRepository productLikeRepo)
        {
            _cart_prRepo = cart_prRepo;
            _cartRepo = cartRepo;
            _order_prodRepo = order_prodRepo;
            _catalogservice = catalogservice;
            _commentLikeRepo = commentLikeRepo;
            _productLikeRepo = productLikeRepo;
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

        public AlzaAdminDTO DecreaseAmount(Cart_pr entity)
        {
            Cart_pr cart_pr;
            cart_pr = _cart_prRepo.GetCartItem(entity);

            if (cart_pr.amount > 1)
            {
                cart_pr.amount--;
                _cart_prRepo.UpdateCartItem(cart_pr);
            }
            
            else
            {
                cart_pr.amount = 1;
                _cart_prRepo.UpdateCartItem(cart_pr);
            }
            

            return AlzaAdminDTO.Data(cart_pr);
        }

        
        public void DumpCart(int id)
        {
            var temp = _cart_prRepo.GetProductsCart(id);
            foreach(var item in temp)
            {
                try
                {
                    _cart_prRepo.DeleteCart_pr(item);
                    
                }
                catch (Exception e)
                {
                    
                }
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

        ///////////////////////////////////////
        ////             LIKES            ////
        //////////////////////////////////////



        public bool AlreadyHasLike (Comment_Like Like)
        {
            var result = _commentLikeRepo.CommentHasLike(Like.id_us, Like.id_com);
            return result;
        }

        public bool AlreadyHasDislike(Comment_Like Like)
        {
            var result = _commentLikeRepo.CommentHasDislike(Like.id_us, Like.id_com);
            return result;
        }

        public AlzaAdminDTO MakeNewLike(Comment_Like Like)
        {
            try
            {
                _commentLikeRepo.AddNewLike(Like);
                return AlzaAdminDTO.Data(Like);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        
        public AlzaAdminDTO RemoveLike(Comment_Like Like)
        {
            try
            {
                _commentLikeRepo.DeleteLike(Like);
                return AlzaAdminDTO.Data(Like);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO RemoveLike(int id_us, int id_com)
        {
            var temp = new Comment_Like { id_us = id_us, id_com = id_com, type = "like" };
            try
            {
                _commentLikeRepo.DeleteLike(temp);
                return AlzaAdminDTO.Data(temp);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO RemoveDislike(Comment_Like Like)
        {
            try
            {
                _commentLikeRepo.DeleteLike(Like);
                return AlzaAdminDTO.Data(Like);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO RemoveDislike(int id_us, int id_com)
        {
            var temp = new Comment_Like { id_us = id_us, id_com = id_com, type = "dislike" };

            try
            {
                _commentLikeRepo.DeleteLike(temp);
                return AlzaAdminDTO.Data(temp);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }


        ///////////////////////////////////////
        ////          WISH LIST            ////
        //////////////////////////////////////


        public bool AlreadyHasThisProductInList(int id_us, int id_pr)
        {
            var result = _productLikeRepo.ThisUserHasTheProductInHisWishList(id_us, id_pr);
            return result;
        }

        public AlzaAdminDTO AddProductToWishList(int id_us, int id_pr)
        {
            var NewProductInWishList = new Product_Like {id_us = id_us, id_pr = id_pr };

            try
            {
                _productLikeRepo.AddProductToWishList(NewProductInWishList);
                return AlzaAdminDTO.Data(NewProductInWishList);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public List<WishProduct> GetAllWishProductsFromThisUser(int id_us)
        {
            var ProductLikes = _productLikeRepo.GetAllWishProductsFromThisUser(id_us);
            List<WishProduct> WishList = new List<WishProduct>();
            

            foreach (var wishProduct in ProductLikes)
            {
                var wish = new WishProduct(wishProduct.id_pr, wishProduct.id_us, _catalogservice.GetProductName(wishProduct.id_pr),
                     _catalogservice.GetProduct(wishProduct.id_pr).price, _catalogservice.GetFirm(_catalogservice.GetProduct(wishProduct.id_pr).id_fir).name,
                    _catalogservice.GetImage(wishProduct.id_pr).link){ };
                WishList.Add(wish);

            }
            return WishList;
        }

    }
}
