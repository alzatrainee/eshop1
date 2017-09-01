﻿using Alza.Core.Module.Abstraction;
using Alza.Core.Module.Http;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository;
using Catalog.Dal.Repository.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Business
{
    public class CatalogService 
    {
        //private ILogger<CatalogService> _logger;
        private IProductRepository _productRepo;
        private IColourRepository _colourRepo;
        private IImageRepository _imageRepo;
        private IFirmRepository _firmRepo;
        private ISizeRepository _sizeRepo;
        private Iprod_colRepository _iprod_colRepository;
        private IProd_siRepository _iProd_siRepository;
        private ICategoryRepository _categoryRepo;
        private IProduct_catRepository _product_catRepository;
        private ICat_subRepository _cat_subRepo;
        private ICommentRepository _commentRepo;


        public CatalogService(

            IProductRepository productRepo,
            IColourRepository colourRepo,
            ISizeRepository sizeRepo,
            IImageRepository imageRepo,
            IProd_siRepository iProd_siRpository,
            Iprod_colRepository iprod_colRepository,
            IFirmRepository firmRepo,
            ICategoryRepository categoryRepo,
            IProduct_catRepository product_catRepository,
            ICat_subRepository cat_subRepo,
            ICommentRepository commentRepo
          )
        {
            _productRepo = productRepo;
            _colourRepo = colourRepo;
            _iprod_colRepository = iprod_colRepository;
            _sizeRepo = sizeRepo;
            _imageRepo = imageRepo;
            _firmRepo = firmRepo;
            _iProd_siRepository = iProd_siRpository;
            _categoryRepo = categoryRepo;
            _product_catRepository = product_catRepository;
            _cat_subRepo = cat_subRepo;
            _commentRepo = commentRepo;
        }

        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------Products----------------------------------------------
        public List<Product> GetAllProducts()
        {
            
                var result = _productRepo.GetAllProducts().ToList();
                
                return (result);
            
        }

        //----------------------------------------------------------------------------------------------
        //-----------------------------------------Colours----------------------------------------------
        public List<Colour> getAllColours()
        {
         
                var result = _colourRepo.getAllColours().ToList();
                return(result);
            
            //throw new NotImplementedException();
        }

        public Prod_col GetProductByRGB(string id, int id_prod)
        {
            var result = _iprod_colRepository.GetProductByRGB(id, id_prod);
            return result;
        }
        public AlzaAdminDTO FindByName(string name)
        {
            try
            {
                var result = _colourRepo.FindByName(name);
                return AlzaAdminDTO.Data(result);
            }
            catch ( Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }

        }

        public Product GetProduct(int id)
        {
            
                var result = _productRepo.GetProduct(id);
                return (result);
           
        }

        public List<Prod_col> GetRgb (int id)
        {
            var result = _iprod_colRepository.GetRGB(id);
            return (result);

        }

        public Colour GetColour(string id)
        {
            var result = _colourRepo.GetColour(id);
            return (result);

        }

        public Size GetSize(int id)
        {
            var result = _sizeRepo.GetSize(id);
            return (result);
        }

        public Image GetImage(int id)
        {

            var result = _imageRepo.GetImage(id);
            return (result);

        }

        public List<Image> getAllImages(int id_pr)
        {
            var result = _imageRepo.GetAllImages(id_pr);
            return (result);
        }

        public List<Firm> GetSpecificFirm(int id_fir)
        {
            var result = _firmRepo.GetSpecificFirm(id_fir);
            return (result);
        }

        public Firm GetFirm(int id_fir)
        {
            var result = _firmRepo.GetFirm(id_fir);
            return (result);
        }
        public List<Firm> GetAllFirms()
        {
            var result = _firmRepo.GetAllFirms();
            return (result);
        }

        public List<Product_cat> Get_Product_cat(int id_pr)
        {
            var result = _product_catRepository.Get_Product_cat(id_pr);
            return result;
        }

        public List<Product_cat> Get_ProductId(int id_cs)
        {
            var result = _product_catRepository.Get_ProductId(id_cs);
            return result;
        }

        public Cat_sub GetCat_Sub(int id_cs)
        {
            var result = _cat_subRepo.GetCat_Sub(id_cs);
            return result;
        }

        public List<Cat_sub> GetProductCategory(int id_cat)
        {
            var result = _cat_subRepo.GetProductCategory(id_cat);
            return result;
        }

        public Category GetCategory(int id_cat)
        {
            var result = _cat_subRepo.GetCategory(id_cat);
            return result;
        }
        public List<Prod_si> GetID_size(int id)
        {
            var result = _iProd_siRepository.GetId_size(id);
            return (result);
        }
        public List<Category> GetCategoryByName( string name )
        {
            var result = _categoryRepo.GetCategoryWithName(name);
            return result;
        }

        public Prod_si GetProductId_size(int id_si, int id_prod)
        {
            var result = _iProd_siRepository.GetProductId_size(id_si, id_prod);
            return (result);
        }

        public List<Size> GetAllSizes()
        {

            var result = _sizeRepo.GetAllSizes().ToList();

            return (result);

        }


        /**********************************************/
        /*                FILTERS                     */
        /**********************************************/

        public Cat_sub GetProductCategoryFirst(int id_cat)
        {
            var result = GetProductCategoryFirst(id_cat);
            return result;
        }

        public void GetProductBrowse(FilterProduct model, int[] Ident)
        {
            /* prochazime cele pole Ident ve kterem jsou ulozeny nase produkty ve forme id_pr*/
            foreach (var item in Ident) { 
                var result = _productRepo.GetProduct(item); // z item(id_pr) zjistuji dany produkt
                var image = _imageRepo.GetImage(result.id_pr); // k produktu si zjistuji image
                var firm = _firmRepo.GetFirm(result.id_fir); // a pote i firmu

                /* zde vsechno priradim do FilterProduct */
                var viewModel = new FilterProduct
                {
                    name = result.name,
                    price = result.price,
                    firm = firm.name,
                    image = image.link,
                    id_pr = result.id_pr,
                    date = result.date,
                    id_fir = result.id_fir

                };
                model.ProductFilter.Add(viewModel); //ukladam postupne vsechny produkty a pote je v Controlleru predam do View
            }
        }
        public void GetAllProductsBrowse(FilterProduct model)
        {
            var allProducts = _productRepo.GetAllProducts();

            foreach (var item in allProducts)
            {

                var image = _imageRepo.GetImage(item.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                var firm = _firmRepo.GetFirm(item.id_fir);

                var viewModel = new FilterProduct
                {
                    name = item.name,
                    price = item.price,
                    firm = firm.name,


                    image = image.link,
                    id_pr = item.id_pr,
                    date = item.date,
                    id_fir = item.id_fir

                };
                model.ProductFilter.Add(viewModel);
            }
        }

        
        public void GetAllProductsCategory(int id, FilterProduct model)
        {
            var cate = _cat_subRepo.GetProductCategory(id);
            foreach (var category in cate)
            {
                var res = _product_catRepository.Get_ProductId(category.id_cs);

                foreach (var product in res)
                {
                    var result = _productRepo.GetProduct(product.id_pr);
                    var image = _imageRepo.GetImage(result.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                    var firm = _firmRepo.GetFirm(result.id_fir);
                    var viewModel = new FilterProduct
                    {
                        id_pr = product.id_pr,
                        name = result.name,
                        price = result.price,
                        firm = firm.name,
                        image = image.link,
                        id_fir = result.id_fir
                    };
                    model.ProductFilter.Add(viewModel);
                }
            }
        }

        public void FilterColourAll(string[] Colours, FilterProduct model)
        {
            var resAll = _productRepo.GetAllProducts();
            foreach (var colour in Colours)
            {
                foreach (var id_product in resAll)
                {
                    var res = _iprod_colRepository.GetProductByRGB(colour, id_product.id_pr);

                    if (model.ProductFilter.Count() == 0 && res != null)
                    {
                        model.ProductFilter.Add(FilterModel(model, id_product.id_pr));
                    } else
                    { 
                        var tmp = model.ProductFilter.Where(p => p.id_pr == id_product.id_pr).ToList();
                        if (tmp.Count < 1 && res != null)
                        {
                            model.ProductFilter.Add(FilterModel(model, id_product.id_pr));
                        }
                    }

                }
            }
        }


        public void FilterColour(string[] Colours, FilterProduct model)
        {
            /* pomocny List do ktereho si ukladam produkty, kteri projdou filtrem, pozdeji cely list priradim do model.ProductFilter */
            List<FilterProduct> tmpColour = new List<FilterProduct>();
                foreach (var colour in Colours) // podle toho kolik bylo zatrhnutych barev tolikrat projdu cyklus
                {
                    foreach (var id_product in model.ProductFilter) // vyhledavam jake produkty jsou v model.ProductFilter a budu je zkouset jestli projdou filtrem
                    {
                        var res = _iprod_colRepository.GetProductByRGB(colour, id_product.id_pr); // hledam jestli existuje pripad se stejnym id_pr a colour

                    if (tmpColour.Count() == 0 && res != null) // pokud je jeste "tmpColour" prazdny a existuje "res" priradi se produkt do tmpColour
                    {
                        tmpColour.Add(FilterModel(model, id_product.id_pr)); // volam funkci kde se provede prirazeni
                    } else
                    { 
                        var tmp = model.ProductFilter.Where(p => p.id_pr == id_product.id_pr).ToList(); // hledam jestli vyhovuje nejaky id_pr v model.ProductFilter a pokud ano a je nalezen pouze jeden priradi se do tmpColour
                            if (tmp.Count < 2 && res != null)
                            {
                               tmpColour.Add(FilterModel(model, id_product.id_pr));
                            }
                        }
                    }
                }
            model.ProductFilter = tmpColour; // v tmpColour uz je vse vyfiltrovane a priradi se to do model.ProductFilter
            }
        

        public void FilterSize(FilterProduct model, int[] Sizes)
        {
            List<FilterProduct> tmpSize = new List<FilterProduct>();
            foreach (var size in Sizes)
            {
                foreach (var product in model.ProductFilter)
                {
                    var idProduct = _iProd_siRepository.GetProductId_size(size, product.id_pr);
                     if (tmpSize.Count() > 0)
                                           {
                        var tmp = tmpSize.Where(p => p.id_pr == product.id_pr).ToList();
                        if (tmp.Count < 1 && idProduct != null)
                        {
                            tmpSize.Add(FilterModel(model, idProduct.id_pr));
                        }
                    }
                    
                    if (tmpSize.Count() == 0 && idProduct != null )
                    {
                        tmpSize.Add(FilterModel(model, idProduct.id_pr));
                    }
                        
                }
            }
                model.ProductFilter = tmpSize;
        }

        public void FilterFirm(FilterProduct model, int[] Firms)
        {
            List<FilterProduct> tmpFirm = new List<FilterProduct>();
            foreach (var firma in Firms)
            {
                var pom = model.ProductFilter.Where(p => p.id_fir == firma).ToList();
                foreach (var item in pom)
                {
                    tmpFirm.Add(item);
                }
            }
            model.ProductFilter = tmpFirm;
        }

        public FilterProduct FilterModel(FilterProduct model, int item)
        {
            var result = _productRepo.GetProduct(item);
            var image = _imageRepo.GetImage(result.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
            var firm = _firmRepo.GetFirm(result.id_fir);
            var viewModel = new FilterProduct
            {
                id_pr = item,
                name = result.name,
                price = result.price,
                firm = firm.name,
                image = image.link,
                id_fir = result.id_fir
            };
           
            return viewModel;
        }

        public List<Product> GetProductsByName(string SearchString)
        {
            var result = _productRepo.GetProductByName(SearchString); // Najdem vsechny Products, odpovidajici zadanemu stringu
            
            return result;
        }
        public List<Firm> GetFirmsByName(string SearchString)
        {
            var result = _firmRepo.GetFirmsByName(SearchString);
            return result;
        }


        public List<FilterProduct>GetProductByFirmId(int id_fir)
        {
            var result = _productRepo.FindProductByFirmId(id_fir);
            List<Image> images = new List<Image>();
            List<Firm> firms = new List<Firm>();
            List<FilterProduct> ProductsList = new List<FilterProduct>(); // to, co vracime zpatky

            foreach( var product in result)
            {
                images.Add(_imageRepo.GetImage(product.id_pr));
            }

            foreach(var product in result)
            {
                firms.Add(_firmRepo.GetFirm(product.id_fir));
            }

            int AmountOfProducts = result.Count();

            for(int i = 0; i < AmountOfProducts; ++i )
            {
                var viewModel = new FilterProduct
                {
                    id_pr = result[i].id_pr,
                    name = result[i].name,
                    price = result[i].price,
                    firm = firms[i].name,
                    image = images[i].link,
                    id_fir = result[i].id_fir
                };
                ProductsList.Add(viewModel);
            }
            return ProductsList;
        }
        /**********************************************/
        /*                COMMENTS                    */
        /**********************************************/

        public List<Comment> GetAllComentsOfThisProduct(int id_pr)
        {
            var result = _commentRepo.GetAllCommentsOfThisProduct(id_pr);
            return result;
        }

        public AlzaAdminDTO AddComment(Comment comment)
        {
            try
            {
                _commentRepo.AddComment(comment);
                return AlzaAdminDTO.Data(comment);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
    }
}
