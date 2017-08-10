using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IProduct_catRepository
    {
        List<Product_cat> Get_Product_cat(int id_pr);
        List<Product_cat> Get_ProductId(int id_cs);

    }
}
