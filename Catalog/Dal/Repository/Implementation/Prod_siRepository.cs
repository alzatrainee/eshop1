using System.Text;
using Alza.Core.Module.Abstraction;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;

using System.Linq;
using Catalog.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Catalog.Dal.Context;
using System;
using System.Collections.Generic;

namespace Catalog.Dal.Repository.Implementation
{
    class Prod_siRepository : IProd_siRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<Prod_siRepository> _logger;
        private readonly CatalogDbContext _context;

        public Prod_siRepository(IOptions<CatalogOptions> options, ILogger<Prod_siRepository> logger, CatalogDbContext catalogDbContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDbContext;
        }

        public List<Prod_si> GetId_size(int id)
        {
            var temp = _context.Prod_si.Where(p => p.id_pr == id).ToList();
            return temp;
        }

        public Prod_si GetProductId_size(int id_si, int id_prod)
        {
            var temp = _context.Prod_si.Where(p => (p.id_si == id_si) && (p.id_pr == id_prod)).FirstOrDefault();
            return temp;
        }
    }
}
