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

        public List<Product> GetProductByName(string SearchString)
        {
            var result = _context.Product.Where(s => s.name.ToLower().Contains(SearchString)).ToList();
            return result;
        }
        public List<Product> FindProductByFirmId(int id_fir)
        {
            var result = _context.Product.Where(s => s.id_fir == id_fir).ToList();
            return result;
        }

        public int GetAllProductLikes(int id_pr)
        {
            var amount = _context.Product.Where(p => p.id_pr == id_pr).FirstOrDefault().likes;
            return amount;
        }

        public Product GetAllProductCategory(int id_pr)
        {
            var amount = _context.Product.Where(p => p.id_pr == id_pr).FirstOrDefault();
            return amount;
        }

        public int AddLikeToProduct(int id_pr)
        {
            var ProductWithNeededID = _context.Product.Where(p => p.id_pr == id_pr).FirstOrDefault();
            ProductWithNeededID.likes++;
            _context.SaveChanges();
            return ProductWithNeededID.likes;
        }

        public string GetProductName(int id_pr)
        {
            var result = _context.Product.Where(c => c.id_pr == id_pr).FirstOrDefault().name;
            return result;
        }

        public void RemoveLikeFromPoduct(int id_pr)
        {
            var ProductWithNeededID = _context.Product.Where(p => p.id_pr == id_pr).FirstOrDefault();
            ProductWithNeededID.likes--;
            _context.SaveChanges();
        }
    }
}
