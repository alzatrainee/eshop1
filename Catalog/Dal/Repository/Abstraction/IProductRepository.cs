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
        //IQueryable<Product> Query(Dictionary<string, string> filter);
        List<Product> GetAllProducts();
        Product AddProduct(Product p);
        void RemoveProduct(Product p);
        Product GetProduct(int id);
        Product GetProduct(string name);
        //Product FindByColour(string name);
        Product UpdateProduct(Product entity);
        List<Product> GetProductByName(string SearchString);
        List<Product> FindProductByFirmId(int id_fir);
        List<Product> GetFewProducts(int minPage, int maxPage);
        IQueryable<Product> GetAllQProducts();
        List<Product> SortByPriceMin(int minPrice);
        List<Product> SortByPriceMax(int maxPrice);
        List<Product> SortByPrice(int maxPrice, int minPrice);
        int GetAllProductLikes(int id_pr);
        int AddLikeToProduct(int id_pr);
        string GetProductName(int id_pr);
        void RemoveLikeFromPoduct(int id_pr);
    }
}
