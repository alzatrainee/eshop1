using Catalog.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Alza.Core.Module.Abstraction;
using Catalog.Dal.Entities;
using System.Linq;
using Catalog.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Catalog.Dal.Repository.Implementation
{
    class ProductRepository : IProductRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<ProductRepository> _logger;

        public ProductRepository(IOptions<CatalogOptions> options, ILogger<ProductRepository> logger)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
        }


        //NAVIGATION
        //public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        /*********************************************/
        /*                                           */
        /*********************************************/
       
        
        
        public Product Add(Product entity)
        {
            Product en = new Product();

            return en;
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetByName(string name)
        {
            throw new NotImplementedException();
        }


        public IQueryable<Product> Query(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product entity)
        {
            throw new NotImplementedException();
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/

        public IQueryable<Product> getAllProducts()
        {
            List<Product> result = new List<Product>();

            return result.AsQueryable();
        }

        public IQueryable<Product> Query()
        {
            throw new NotImplementedException();
        }
    }
}
