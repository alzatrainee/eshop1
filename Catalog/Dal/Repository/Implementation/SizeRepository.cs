using Catalog.Configuration;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalog.Dal.Repository.Implementation
{
    public class SizeRepository : ISizeRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<SizeRepository> _logger;
        private readonly CatalogDbContext _context;

        public SizeRepository(IOptions<CatalogOptions> options, ILogger<SizeRepository> logger, CatalogDbContext catalogDBContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            } 
            _options = options.Value;
            _logger = logger;
            _context = catalogDBContext;
        }
        public Size AddSize(Size entity)
        {
            _context.Size.Add(entity);
            return entity;
        }
        public void RemoveSize(Size entity)
        {
            _context.Size.Remove(entity);
            _context.SaveChanges();
        }

        public Size UpdateSize(Size entity)
        {
            var oldSize = _context.Size.Where(s => s.id_si == entity.id_si).FirstOrDefault();
            _context.Entry(oldSize).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entity;
        }



        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/

        public List<Size> GetAllSizes()
        {
            var result = _context.Size.ToList();
            return result;
        }

    }
}
