using Alza.Core.Module.Abstraction;
using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> Query(Dictionary<string, string> filter);

        Product GetByName(string name);
    }
}
