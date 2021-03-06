﻿using Module.Business.Dal.Entities;
using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Business.Dal.Repository.Abstraction
{
    public interface ICart_prRepository
    {

        Cart_pr AddCartItem(Cart_pr entity);
        Cart_pr GetCartItem(Cart_pr entity);
        Cart_pr UpdateCartItem(Cart_pr entity);
        void RemoveCartItem(Cart_pr entity);
        List<Cart_pr> GetCartItems(int id);
        List<Cart_pr> GetConnectCart(int id);
        List<Cart_pr> GetProductsCart(int id);
        void DeleteCart_pr(Cart_pr item);
    }
}
