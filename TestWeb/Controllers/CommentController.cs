﻿using System.Collections.Generic;
using System.Linq;
using Catalog.Dal.Repository.Abstraction;
using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using Catalog.Dal.Context;
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
        private readonly UserProfileService _userService;
        private readonly BusinessService _businessService;
        

        public CommentController(CatalogService catalogservice, UserProfileService userService,  BusinessService businessService)
        {
            _catalogService = catalogservice;
            _userService = userService;
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
            int[] hodnoty = { 0, 0, 0, 0 }; // hodnota[0] - like predchozi, hodnota[1] - dislike predchozi, hodnota[2] - like novy, hodnota[3] - dislike novy
            hodnoty[0] = _catalogService.AmountOfLikes(Like.id_com);
            hodnoty[1] = _catalogService.AmountOfDislikes(Like.id_com);

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

            hodnoty[2] = _catalogService.AmountOfLikes(Like.id_com);
            hodnoty[3] = _catalogService.AmountOfDislikes(Like.id_com);
            var values = new Values { likesOld = hodnoty[0], dislikesOld = hodnoty[1], likesNew = hodnoty[2], dislikesNew = hodnoty[3] };

            return Json(values);
        }


    }
}
