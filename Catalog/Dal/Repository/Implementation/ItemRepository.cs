using Catalog.Configuration;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalog.Dal.Repository.Implementation
{
    public class ItemRepository : IItemRepository
    {
        private readonly CatalogOptions _options;
        private readonly CatalogDbContext _context;

        public ItemRepository(IOptions<CatalogOptions> options, CatalogDbContext catalogDBContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _context = catalogDBContext;
        }
        public Item GetItem(int id)
        {
            throw new NotImplementedException();
            //var result = _context.Item.Where(s=> (s.id_it == id)).FirstOrDefault();
            //return result;
        }
    }
}
