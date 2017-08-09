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
    class FirmRepository : IFirmRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<FirmRepository> _logger;
        private readonly CatalogDbContext _context;

        public FirmRepository(IOptions<CatalogOptions> options, ILogger<FirmRepository> logger, CatalogDbContext catalogDBContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDBContext;
        }

        public Firm GetFirm(int id_fir)
        {
            var result = _context.Firm.Where(c => c.id_fir == id_fir).FirstOrDefault();
            return (result);
        }

        public List<Firm> GetAllFirms()
        {
            var result = _context.Firm.ToList();
            return (result);
        }
    }
}
