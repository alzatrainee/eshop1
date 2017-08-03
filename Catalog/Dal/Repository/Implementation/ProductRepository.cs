using Catalog.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Alza.Core.Module.Abstraction;
using Catalog.Dal.Entities;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Catalog.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Catalog.Dal.Context;

namespace Catalog.Dal.Repository.Implementation
{
    class ProductRepository : IProductRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<ProductRepository> _logger;
        private readonly CatalogDbContext _context;

        public ProductRepository(IOptions<CatalogOptions> options, ILogger<ProductRepository> logger, CatalogDbContext catalogDbContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDbContext;
        }


        //NAVIGATION
        //public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        /*********************************************/
        /*                                           */
        /*********************************************/
       
        
        
        public Product AddProduct(Product entity)
        {
            _context.Product.Add(entity);
            _context.SaveChanges();
            return entity;

        }

        public Product GetProduct(int id)
        {
            Product p = _context.Product.Where(s => (s.id_pr == id)).FirstOrDefault();
            return p;
        }

        public Product GetProduct(string name)
        {
            Product p = _context.Product.Where(s => (s.name == name)).FirstOrDefault();
            return p;
        }

        public Product FindByColour(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product entity)
        {
            _context.Product.Remove(entity);
            _context.SaveChanges();
        }

        public Product UpdateProduct(Product entity)
        {
            var oldProduct = _context.Product.Where(s => s.id_pr == entity.id_pr);
            _context.Entry(oldProduct).CurrentValues.SetValues(entity);
            return entity;
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/

        public List<Product> GetAllProducts()
        {
            var result = _context.Product.ToList();
            
            return result;
        }


        public Product GetProduct(int Id)
        {
            var temp = _context.Product.Where(p => p.id_pr == Id).FirstOrDefault();

            return temp;
        }


        public IQueryable<Product> Query()
        {
            throw new NotImplementedException();
        }

        
        public IQueryable<Product> Query(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }

        

    }
}
