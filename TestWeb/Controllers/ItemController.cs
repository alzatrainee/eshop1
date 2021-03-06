﻿using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using PernicekWeb.Models.ItemViewModels;
using Alza.Module.UserProfile.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.Extensions.Logging;
using Alza.Module.UserProfile.Dal.Context;
using System.Threading.Tasks;
using Module.Business.Business;

namespace Pernicek.Controllers
{
    public class ItemController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly BusinessService _businessService;
        private readonly UserProfileService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly UserProfileService _userProfileService;
        private readonly UserDbContext _context;

        public ItemController(CatalogService catalogservice, BusinessService businessService, UserProfileService userservice, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
                                ILoggerFactory loggerFactory, UserProfileService userProfileservice, UserDbContext context )
        {
            _catalogService = catalogservice;
            _businessService = businessService;
            _userService = userservice;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<ItemController>();
            _userProfileService = userProfileservice;
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> Item(int? id)
        {
            
            var result = _catalogService.GetProduct(id.Value);
            var res = _catalogService.GetRgb(result.id_pr);
            var size = _catalogService.GetID_size(result.id_pr);
            var image = _catalogService.getAllImages(result.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
            var firm = _catalogService.GetFirm(result.id_fir);
            var product_cat = _catalogService.Get_Product_cat(result.id_pr);
            var number_of_product_cat = product_cat.Count(); // je vzdy 2 
            List<Catalog.Dal.Entities.Cat_sub> cat_sub = new List<Catalog.Dal.Entities.Cat_sub>();
            var comments = _catalogService.GetAllComentsOfThisProduct(result.id_pr);// vraci vsechny komentare daneho produktu , ktere kdykoli byly uvedene v databazi

            for (var i = 0; i < number_of_product_cat; ++i) // maximalni pocet iteraci je vzdy 2
            {
                cat_sub.Add(_catalogService.GetCat_Sub(product_cat[i].id_cs));
            }

            List<Catalog.Dal.Entities.Category> categories = new List<Catalog.Dal.Entities.Category>();

            for (var i = 0; i < 2; ++i)
            {
                categories.Add(_catalogService.GetCategory(cat_sub[i].id_cat));
            }


            var velikost = res.Count(); // jaky je pocet barev patricich danemu produktu 
            var velikost_size = size.Count(); // pocet vsech velikosti u vybraneho produktu
            var number_of_images = image.Count(); //  pocet vsech obrazku daneho productu
            List<Catalog.Dal.Entities.Colour> pom = new List<Catalog.Dal.Entities.Colour>(); // vytvarime pole, ktere by melo v sobe obsahovat vsechny barvy tohoto produktu  
            List<Catalog.Dal.Entities.Size> array_sizes = new List<Catalog.Dal.Entities.Size>(); // pole, ktere zahrnuje vsechny velikosti vybraneho produktu 



            for (var i = 0; i < velikost; ++i)
            {
                pom.Add(_catalogService.GetColour(res[i].rgb));
            }
            for (var i = 0; i < velikost_size; ++i)
            {
                array_sizes.Add(_catalogService.GetSize(size[i].id_si));

            }

            List<string> namesOfUsers = new List<string>(); // jmena Useru (jen z ucelu zobrazeni v komentarech)
            int NumberOfComments = 0;


            foreach (var comment in comments)
            {
                namesOfUsers.Add(_userService.FindNameOfUser(comment.id_us).name);
                ++NumberOfComments;
            }
            ///////////////////////////////////////////////////////////////////////////
            ///// Random function to find , what "You can also be interested in" /////

            Random ram = new Random();
            int[] randomItemsID = new int[4];

            for (var i = 0; i < 4;) // zobrazi se maximalne 4 producty
            {
                var temp = ram.Next(1, 105);

                for (var t = 0; t < i; ++t)
                {
                    if (randomItemsID[t] == temp)
                        continue;
                }

                randomItemsID[i] = temp;
                ++i;
            }


            ///////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            var model = new Product
            {
                id_pr = id.Value,
                name = result.name,
                date = result.date,
                price = result.price,
                description = result.description,
                firm = firm.name,
                descriptionOfFirm = firm.information,
                colours = new List<Colour>(), // vytvorime list colour pro vypis vsech 
                sizes = new List<Size>(),
                images = new List<Image>(),
                category = categories[1].name,
                comments = new List<Comment>(),
                AmountOfComments = NumberOfComments,
                IntrestedIn = new List<InterestedIn>(),
                InterestedInWishList = new List<int>()

            };
            //////////////////////////////////////////////////////
            ///////          INTERESTED IN             //////////
            ////////////////////////////////////////////////////


            for (var i = 0; i < 4; ++i)
            {
                var exists = _catalogService.ProductExists(randomItemsID[i]);

                if (exists != null)
                {
                    if (exists.description.Count() < 100)
                        model.IntrestedIn.Add(new InterestedIn { id_pr = exists.id_pr, name = exists.name, description = exists.description, obrazek = exists.obrazek, price = exists.price });
                    else
                        model.IntrestedIn.Add(new InterestedIn { id_pr = exists.id_pr, name = exists.name, description = exists.description.Substring(0, 100), obrazek = exists.obrazek, price = exists.price });

                }
                else
                    continue;
            }

            var user = await GetCurrentUserAsync();

            if(user != null)
            {
                foreach (var interest in model.IntrestedIn)
                {
                    if (_businessService.AlreadyHasThisProductInList(user.Id, interest.id_pr))
                    {
                        model.InterestedInWishList.Add(1); // ano, je ve wishListu
                    }
                    else
                    {
                        model.InterestedInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }
                }
            }
            

            // Barvy, velikosti, obrazky

            for (var i = 0; i < velikost; ++i)
            {
                model.colours.Add(new Colour(pom[i].rgb, pom[i].name) { });
            }

            for (var i = 0; i < velikost_size; ++i)
            {
                model.sizes.Add(new Size(array_sizes[i].id_si, array_sizes[i].uk) { });
            }

            for (var i = 0; i < number_of_images; ++i)
            {
                model.images.Add(new Image(image[i].id_im, image[i].link) { });
            }


            //Comments
            List<int> ListOfParentID = new List<int>();

            for (var i = 0; i < NumberOfComments; ++i)
            {
                if (comments[i].parent_com != null)
                {
                    model.comments.Add(new Comment(comments[i].id_com, namesOfUsers[i], comments[i].comment, comments[i].thumb_up, comments[i].thumb_down, comments[i].parent_com, comments[i].date) { });
                    ListOfParentID.Add(comments[i].id_com);

                }
                else
                    model.comments.Add(new Comment(comments[i].id_com, namesOfUsers[i], comments[i].comment, comments[i].thumb_up, comments[i].thumb_down, comments[i].date) { });
            }
            

            if(user == null)
            {
                return View(model);
            }
            else
            {
                foreach(var comment in model.comments)
                {
                    var CommentLike = _businessService.HasLikeDislikeOnThisComment(comment.id_com, user.Id);
                    if (CommentLike != null)
                        model.UsersLikes.Add(CommentLike.type);
                    else
                        model.UsersLikes.Add("");
                }
            }
            return View(model);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}