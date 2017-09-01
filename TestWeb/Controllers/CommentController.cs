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
        private readonly ICommentRepository _commentRepo;
        private readonly UserProfileService _userService;

        //private readonly ICategoryRepository _iCategoryRepository;

        public CommentController(CatalogService catalogservice, UserProfileService userService, ICommentRepository commentRepo)
        {
            _catalogService = catalogservice;
            _userService = userService;
            _commentRepo = commentRepo;
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create([FromBody] CommentForAJAX Comment )
        {
           var date = DateTime.Now;
           var comment = new Comment(Comment.id_us, Comment.id_pr, Comment.comment, date) { };
            _catalogService.AddComment(comment);
            return Json(Comment.id_pr);
        }

    }
}
