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
    public class ColourRepository : IColourRepository
    {

        private readonly CatalogOptions _options;
        private ILogger<ColourRepository> _logger;
        private readonly CatalogDbContext _context;

        public ColourRepository(IOptions<CatalogOptions> options, ILogger<ColourRepository> logger, CatalogDbContext catalogDBContext )
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDBContext;
        }
        
        public Colour AddColour(Colour entity)
        {

            _context.Colour.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void RemoveColour(Colour entity)
        {            
            _context.Colour.Remove(entity);
            _context.SaveChanges();

        }

        public Colour Update(Colour entity)
        {
            throw new NotImplementedException();
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/
        public List<Colour> getAllColours()
        {
            var result = _context.Colour.ToList();
            return result;
        }

        public IQueryable<Colour> Query()
        {
            throw new NotImplementedException();
        }

        public Colour GetColour(string Id)
        {
            var temp = _context.Colour.Where(p => p.rgb == Id).FirstOrDefault();

            return temp;
        }
    }
}

