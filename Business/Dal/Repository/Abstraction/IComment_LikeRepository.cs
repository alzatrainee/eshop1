using Module.Business.Dal.Entities;
using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Business.Dal.Repository.Abstraction
{
    public interface IComment_LikeRepository
    {
        bool CommentHasLike(int id_us, int id_com);
        bool CommentHasDislike(int id_us, int id_com);
        Comment_Like AddNewLike(Comment_Like Like);
        void DeleteLike(Comment_Like Like);
        Comment_Like HasLikeDislikeOnThisComment(int id_com, int id_us);
    }
}
