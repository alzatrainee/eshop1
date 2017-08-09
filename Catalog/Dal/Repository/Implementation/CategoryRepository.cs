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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<CategoryRepository> _logger;
        private readonly CatalogDbContext _context;

        public CategoryRepository(IOptions<CatalogOptions> options, ILogger<CategoryRepository> logger, CatalogDbContext catalogDBContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDBContext;
        }

        public Category AddCategory(Category entity)
        {
            _context.Category.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void RemoveCategory(Category entity)
        {
            _context.Category.Remove(entity);
            _context.SaveChanges();

        }

        public Category UpdateCategory(Category entity)
        {
            var oldCategory = _context.Category.Where(s => s.id_cat == entity.id_cat).FirstOrDefault();
            return entity;
        }
        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/
        public List<Category> GetAllCategory()
        {
            var result = _context.Category.ToList();
            return result;
        }

    }
}
