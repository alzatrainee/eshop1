using System.Collections.Generic;
using System.Linq;
using Catalog.Dal.Repository.Abstraction;
using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using Catalog.Dal.Context;
using PernicekWeb.Models.CatalogViewModel;
using Alza.Module.UserProfile.Business;
using Catalog.Dal.Entities;
using System;
using Pernicek.Controllers;
using Module.Business.Dal.Entities;
using Module.Business.Business;
using PernicekWeb.Models.ItemViewModels;

namespace PernicekWeb.Controllers
{
    public class CommentController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly ICommentRepository _commentRepo;
        private readonly UserProfileService _userService;
        private readonly BusinessService _businessService;

        //private readonly ICategoryRepository _iCategoryRepository;

        public CommentController(CatalogService catalogservice, UserProfileService userService, ICommentRepository commentRepo, BusinessService businessService)
        {
            _catalogService = catalogservice;
            _userService = userService;
            _commentRepo = commentRepo;
            _businessService = businessService;
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create([FromBody] CommentForAJAX Comment )
        {
           var date = DateTime.Now;
           var comment = new Catalog.Dal.Entities.Comment(Comment.id_pr, Comment.id_us,  Comment.comment, date, Comment.parent_com ) { };
            _catalogService.AddComment(comment);
            return Json(Comment.id_pr);
        }

        [HttpPost]

        public IActionResult LikeThis([FromBody] Comment_Like Like)
        {
            int[] hodnoty = { 0, 0 }; // hodnota[0] - like, hodnota[1] - dislike

            if (Like.type == "like")
            {
                if (_businessService.AlreadyHasLike(Like))
                {
                    _businessService.RemoveLike(Like);
                    _catalogService.RemoveLike(Like.id_com);
                }
                else if (_businessService.AlreadyHasDislike(Like))
                {
                    _businessService.RemoveDislike(Like.id_us, Like.id_com);
                    _businessService.MakeNewLike(Like);
                    _catalogService.RemoveDislike(Like.id_com);
                    _catalogService.AddLike(Like.id_com);
                    
                }
                else {
                    _businessService.MakeNewLike(Like);
                    _catalogService.AddLike(Like.id_com);
                }
            } else
            {
                if (_businessService.AlreadyHasDislike(Like))
                {
                    _businessService.RemoveDislike(Like);
                    _catalogService.RemoveDislike(Like.id_com);
                }
                else if(_businessService.AlreadyHasLike(Like))
                {
                    _businessService.RemoveLike(Like.id_us, Like.id_com);
                    _businessService.MakeNewLike(Like);
                    _catalogService.RemoveLike(Like.id_com);
                    _catalogService.AddDislike(Like.id_com);
                    
                } else
                {
                    _businessService.MakeNewLike(Like);
                    _catalogService.AddDislike(Like.id_com);
                }
            }

            hodnoty[0] = _catalogService.AmountOfLikes(Like.id_com);
            hodnoty[1] = _catalogService.AmountOfDislikes(Like.id_com);
            var values = new Values { likes = hodnoty[0], dislikes = hodnoty[1] };

            return Json(values);
        }


    }
}
