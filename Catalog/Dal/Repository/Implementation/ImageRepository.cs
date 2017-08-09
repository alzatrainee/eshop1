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
    public class ImageRepository : IImageRepository
    {

        private readonly CatalogOptions _options;
        private ILogger<ImageRepository> _logger;
        private readonly CatalogDbContext _context;

        public ImageRepository(IOptions<CatalogOptions> options, ILogger<ImageRepository> logger, CatalogDbContext catalogDBContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDBContext;
        }


        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/
        public List<Image> GetAllImages(int id_pr)
        {
            var result = _context.Image.Where(p => p.id_pr == id_pr).ToList();
            return result;
        }

    }
}
