using Catalog.Configuration;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Catalog.Dal.Repository.Implementation
{
    class Product_catRepository : IProduct_catRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<Product_catRepository> _logger;
        private readonly CatalogDbContext _context;

        public Product_catRepository(IOptions<CatalogOptions> options, ILogger<Product_catRepository> logger, CatalogDbContext catalogDBContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDBContext;
        }

        public List<Product_cat> Get_Product_cat(int id_pr)
        {
            var result = _context.Product_cat.Where(p => p.id_pr == id_pr).ToList();
            return result;
        }

        public List<Product_cat> Get_ProductId(int id_cs)
        {
            var result = _context.Product_cat.Where(p => p.id_cs == id_cs).ToList();
            return result;
        }
    }
}
