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
        Product FindByColour(string name);
        Product UpdateProduct(Product entity);

        //Product GetByName(string name);
    }
}
