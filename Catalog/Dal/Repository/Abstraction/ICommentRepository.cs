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
    }
}
