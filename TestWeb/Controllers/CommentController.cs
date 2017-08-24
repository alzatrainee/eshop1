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

//namespace PernicekWeb.Controllers
//{
//    public class CommentController : Controller
//    {
//        private readonly CatalogService _catalogService;
//        private readonly ICommentRepository _commentRepo;
//        private readonly UserProfileService _userService;

//        //private readonly ICategoryRepository _iCategoryRepository;

//        public CommentController(CatalogService catalogservice, UserProfileService userService, ICommentRepository commentRepo)
//        {
//            _catalogService = catalogservice;
//            _userService = userService;
//            _commentRepo = commentRepo;
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]

        
//        //public ActionResult Create([Bind(Include = "id_pr, id_us, comment, PostDate, thumb_up, thumb_down")] Comment comment)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        comment.thumb_up = 0;
//        //        comment.thumb_down = 0;
//        //        comment.PostDate = DateTime.Now;

//        //    }
//        //    return View();
//        //}
//    }
//}
