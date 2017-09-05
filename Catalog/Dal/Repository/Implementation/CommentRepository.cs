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

        public void RemoveLike(int id_com)
        {
            var temp = _context.Comment.Where(c => c.id_com == id_com).FirstOrDefault();

            if (temp.thumb_up > 0)
            {
                temp.thumb_up--;
                _context.SaveChanges();
            }
        }

        public void RemoveDislike(int id_com)
        {
            var temp = _context.Comment.Where(c => c.id_com == id_com).FirstOrDefault();

            if (temp.thumb_down > 0)
            {
                temp.thumb_down--;
                _context.SaveChanges();
            }
        }

        public void AddDislike(int id_com)
        {
            var temp = _context.Comment.Where(c => c.id_com == id_com).FirstOrDefault();
            
                temp.thumb_down++;
                _context.SaveChanges();
            
        }

        public void AddLike(int id_com)
        {
            var temp = _context.Comment.Where(c => c.id_com == id_com).FirstOrDefault();
            
                temp.thumb_up++;
                _context.SaveChanges();
            
        }

        public int AmountOfLikes(int id_com)
        {
            var result = _context.Comment.Where(c => c.id_com == id_com).FirstOrDefault().thumb_up;
            return result;
        }

        public int AmountOfDislike(int id_com)
        {
            var result = _context.Comment.Where(c => c.id_com == id_com).FirstOrDefault().thumb_down;
            return result;
        }


    }
}