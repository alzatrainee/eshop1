﻿using System.Text;
using Alza.Core.Module.Abstraction;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;

using System.Linq;
using Catalog.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Catalog.Dal.Context;
using System;

namespace Catalog.Dal.Repository.Implementation
{
    class Prod_colRepository : Iprod_colRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<Prod_colRepository> _logger;
        private readonly CatalogDbContext _context;

        public Prod_colRepository(IOptions<CatalogOptions> options, ILogger<Prod_colRepository> logger, CatalogDbContext catalogDbContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDbContext;
        }

        public Prod_col GetRGB (int Id)
        {
            var temp = _context.prod_col.Where(p => p.id_pr == Id).FirstOrDefault();

            return temp;
        }
    }
}