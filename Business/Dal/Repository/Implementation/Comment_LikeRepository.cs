using Catalog.Configuration;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Module.Business.Dal.Context;
using Module.Business.Dal.Entities;
using Module.Business.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Business.Dal.Repository.Implementation
{
    public class Comment_LikeRepository : IComment_LikeRepository
    {
        private readonly BusinessDbContext _context;
        public Comment_LikeRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public bool CommentHasLike(int id_us, int id_com)
        {
            var AllCommentsLikesWithThisID = _context.Comment_Like.Where(c => c.id_com == id_com).ToList();

            if (AllCommentsLikesWithThisID != null)
            {
                var AllCommentsLikesWithThisUser = AllCommentsLikesWithThisID.Where(c => c.id_us == id_us).FirstOrDefault();

                if (AllCommentsLikesWithThisUser != null)
                {
                    if (AllCommentsLikesWithThisUser.type == "like")
                        return true;
                    else
                        return false;
                } else
                {
                    return false;
                }
            } else
                return false;
        }

        public bool CommentHasDislike(int id_us, int id_com)
        {
            var AllCommentsLikesWithThisID = _context.Comment_Like.Where(c => c.id_com == id_com).ToList();

            if (AllCommentsLikesWithThisID != null)
            {
                var AllCommentsLikesWithThisUser = AllCommentsLikesWithThisID.Where(c => c.id_us == id_us).FirstOrDefault();

                if (AllCommentsLikesWithThisUser != null)
                {
                    if (AllCommentsLikesWithThisUser.type == "dislike")
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;

        }

        public Comment_Like AddNewLike(Comment_Like Like)
        {
            _context.Comment_Like.Add(Like);
            _context.SaveChanges();
            return Like;
        }

        public void DeleteLike(Comment_Like Like)
        {
            var NewLike = _context.Comment_Like.Where(c => c.id_com == Like.id_com).ToList();
            NewLike = NewLike.Where(c => c.id_us == Like.id_us).ToList();
            var NewNewLike = NewLike.Where(c => c.type == Like.type).FirstOrDefault();

            _context.Comment_Like.Remove(NewNewLike);
            _context.SaveChanges();
        }

        public Comment_Like HasLikeDislikeOnThisComment(int id_com, int id_us)
        {
            var result = _context.Comment_Like.Where(c => (c.id_com == id_com && c.id_us == id_us)).FirstOrDefault();
            return result;
        }
    }
}
