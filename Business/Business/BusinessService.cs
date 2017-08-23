using Alza.Core.Module.Http;
using Module.Business.Dal.Entities;
using Module.Business.Dal.Repository.Abstraction;
using Module.Order.Dal.Repository.Abstraction;

namespace Module.Business.Business
{
    public class BusinessService
    {
        private ICart_prRepository _cart_prRepo;
        private ICartRepository _cartRepo;

        public BusinessService(
            ICart_prRepository cart_prRepo,
            ICartRepository cartRepo
             )
        {
            _cart_prRepo = cart_prRepo;
            _cartRepo = cartRepo;
        }


        public AlzaAdminDTO GetCartItem(int id_car, int id_pr)
        {
            var result = _cart_prRepo.GetCartItem(id_car, id_pr);
            return AlzaAdminDTO.Data(result);
        }




        public AlzaAdminDTO AddToCart(int id_car, int id_pr)
        {
            Cart_pr cart_pr = new Cart_pr();
            cart_pr = _cart_prRepo.GetCartItem(id_car, id_pr);

            if (cart_pr == null)
            {
                cart_pr = new Cart_pr();
                cart_pr.id_pr = id_pr;
                cart_pr.id_car = id_car;
                cart_pr.amount = 1;
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
    }
}
