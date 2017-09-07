using Alza.Core.Module.Abstraction;
using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IProductRepository //: IRepository<Product>
    {
        List<Product> GetAllProducts();
        Product AddProduct(Product p);
        void RemoveProduct(Product p);
        Product GetProduct(int id);
        Product GetProduct(string name);
        Product UpdateProduct(Product entity);
        List<Product> GetProductByName(string SearchString);
        List<Product> FindProductByFirmId(int id_fir);
        int GetAllProductLikes(int id_pr);
        int AddLikeToProduct(int id_pr);
        string GetProductName(int id_pr);
        void RemoveLikeFromPoduct(int id_pr);
    }
}
