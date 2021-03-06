﻿using Module.Business.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Business.Dal.Repository.Abstraction
{
    public interface IProduct_LikeRepository
    {
        bool ThisUserHasTheProductInHisWishList(int id_us, int id_pr);
        Product_Like AddProductToWishList(Product_Like NewProductInWishList);
        List<Product_Like> GetAllWishProductsFromThisUser(int id_us);
        void RemoveFromList(Product_Like temp);
    }
}
