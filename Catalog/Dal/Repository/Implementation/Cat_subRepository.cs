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
    class Cat_subRepository : ICat_subRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<Cat_subRepository> _logger;
        private readonly CatalogDbContext _context;

        public Cat_subRepository(IOptions<CatalogOptions> options, ILogger<Cat_subRepository> logger, CatalogDbContext catalogDBContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDBContext;
        }

        public Cat_sub GetCat_Sub(int id_cs)
        {
            var result = _context.Cat_sub.Where(p => p.id_cs == id_cs).FirstOrDefault();
            return result;
        }
        public Category GetCategory(int id_cat)
        {
            var result = _context.Category.Where(p => p.id_cat == id_cat).FirstOrDefault();
            return result;
        }

        public List<Cat_sub> GetProductCategory(int id_cat)
        {
            var result = _context.Cat_sub.Where(p => p.id_cat == id_cat).ToList();
            return result;
        }

        public Cat_sub GetProductCategoryFirst(int id_cat)
        {
            var result = _context.Cat_sub.Where(p => p.id_cat == id_cat).FirstOrDefault();
            return result;
        }
    }
}
