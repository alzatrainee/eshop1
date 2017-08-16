using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface ICart_prRepository
    {

        Cart_pr AddCartItem(Cart_pr entity);
        Cart_pr GetCartItem(int id_car, int id_pr);
        Cart_pr UpdateCartItem(Cart_pr entity);
        void RemoveCartItem(Cart_pr entity);
    }
}
