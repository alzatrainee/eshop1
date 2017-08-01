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
        
        public Colour Add(Colour entity)
        {
            Colour c = new Colour();
            c.rgb = entity.rgb;
            c.name = entity.name;
            _context.Colour.Add(c);
            return c;
        }

        public void Remove(Colour entity)
        {
            throw new NotImplementedException();
            //var colour = _context.Colour.Where(c => c.rgb == entity.rgb).FirstOrDefault();
            //_context.Colour.Remove(colour);
            //_context.SaveChanges();

        }

        public Colour Update(Colour entity)
        {
            throw new NotImplementedException();
        }

        public Colour FindByName(string name)
        {

            Colour result = _context.Colour.Where(c => c.name == name).FirstOrDefault();
            return result;
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/
        public IQueryable<Colour> getAllColours()
        {
            var result = _context.Colour.AsQueryable();
            return result;
        }

        public IQueryable<Colour> Query()
        {
            throw new NotImplementedException();
        }
    }
}
