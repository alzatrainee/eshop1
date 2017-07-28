using Catalog.Configuration;
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

        public ColourRepository(IOptions<CatalogOptions> options, ILogger<ColourRepository> logger)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
        }

        public Colour Add(Colour entity)
        {
            throw new NotImplementedException();
        }

        public Colour Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Colour> getAllColours()
        {
            List<Colour> result = new List<Colour>();

            return result.AsQueryable();
        }

        public IQueryable<Colour> Query()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Colour Update(Colour entity)
        {
            throw new NotImplementedException();
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/


        IQueryable<Colour> IColourRepository.getAllColours()
        {
            List<Colour> result = new List<Colour>();

            return result.AsQueryable();
        }
    }
}
