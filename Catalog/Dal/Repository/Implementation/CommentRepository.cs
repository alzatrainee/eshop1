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
    public class CommentRepository : ICommentRepository
    {
        private readonly CatalogOptions _options;
        private ILogger<CommentRepository> _logger;
        private readonly CatalogDbContext _context;

        public CommentRepository(IOptions<CatalogOptions> options, ILogger<CommentRepository> logger, CatalogDbContext catalogDBContext)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
            _context = catalogDBContext;
        }

        public List<Comment> GetAllCommentsOfThisProduct(int id_pr)
        {
            var result = _context.Comment.Where(c => c.id_pr == id_pr).ToList();
            return result;
        }
        public Comment AddComment(Comment comment)
        {
            _context.Comment.Add(comment);
            _context.SaveChanges();
            return comment;
        }
    }
}