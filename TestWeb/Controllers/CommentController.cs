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

namespace PernicekWeb.Controllers
{
    public class CommentController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly UserProfileService _userService;


        public CommentController(CatalogService catalogservice, UserProfileService userService)
        {
            _catalogService = catalogservice;
            _userService = userService;
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create([FromBody] CommentForAJAX Comment )
        {
           var date = DateTime.Now;
           var comment = new Comment(Comment.id_pr, Comment.id_us,  Comment.comment, date, Comment.parent_com ) { };
            _catalogService.AddComment(comment);
            return Json(Comment.id_pr);
        }

    }
}
