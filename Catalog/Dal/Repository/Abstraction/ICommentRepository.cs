using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ICommentRepository
    {
        List<Comment> GetAllCommentsOfThisProduct(int id_pr);
        Comment AddComment(Comment comment);
        void RemoveLike(int id_com);
        void RemoveDislike(int id_com);
        void AddDislike(int id_com);
        void AddLike(int id_com);
        int AmountOfLikes(int id_com);
        int AmountOfDislike(int id_com);
    }
}
