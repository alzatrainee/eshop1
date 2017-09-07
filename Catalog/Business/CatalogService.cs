using Alza.Core.Module.Abstraction;
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


        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {

            var result = _productRepo.GetProduct(id);
            return (result);

        }

        /// <summary>
        /// Get all products in db.
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            
                var result = _productRepo.GetAllProducts().ToList();
                
                return (result);
            
        }

        public string GetProductName(int id_pr)
        {
            var result = _productRepo.GetProductName(id_pr);
            return result;
        }

        //----------------------------------------------------------------------------------------------
        //-----------------------------------------Colours----------------------------------------------


        /// <summary>
        /// Get all colours in db.
        /// </summary>
        /// <returns></returns>
        public List<Colour> getAllColours()
        {
         
                var result = _colourRepo.getAllColours().ToList();
                return(result);
            
            //throw new NotImplementedException();
        }
        
        /// <summary>
        /// Get Product by RGB of colour
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id_prod"></param>
        /// <returns></returns>
        public Prod_col GetProductByRGB(string id, int id_prod)
        {
            var result = _iprod_colRepository.GetProductByRGB(id, id_prod);
            return result;
        }
        /// <summary>
        /// Get Colour by it's name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Colour by product ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Prod_col> GetRgb (int id)
        {
            var result = _iprod_colRepository.GetRGB(id);
            return (result);

        }

        /// <summary>
        /// Get Colour by RGB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Colour GetColour(string id)
        {
            var result = _colourRepo.GetColour(id);
            return (result);

        }


        /*----------------------------------------------------------------------------------------------*/
        /*-----------------------------------------Sizes----------------------------------------------*/


        /// <summary>
        /// Get size by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Size GetSize(int id)
        {
            var result = _sizeRepo.GetSize(id);
            return (result);
        }

        /*----------------------------------------------------------------------------------------------*/
        /*-----------------------------------------Images----------------------------------------------*/

        /// <summary>
        /// Get image by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Image GetImage(int id)
        {

            var result = _imageRepo.GetImage(id);
            return (result);

        }

        /// <summary>
        /// Get all images from db.
        /// </summary>
        /// <param name="id_pr"></param>
        /// <returns></returns>
        public List<Image> getAllImages(int id_pr)
        {
            var result = _imageRepo.GetAllImages(id_pr);
            return (result);
        }

        /*----------------------------------------------------------------------------------------------*/
        /*-----------------------------------------Firm----------------------------------------------*/

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
        /// <summary>
        /// Filtrovani pro Search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Ident"></param>
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

        /// <summary>
        /// Zobrazeni prvnich 9 produktu
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        public void GetAllProductsBrowse(FilterProduct model, int page)
        {
            int countPage = 1; // pocitani kolik produktu je na strance

            /* Zjisteni vsech produktu a podle toho kolik ma byt celkem stran */
            var allProducts = _productRepo.GetAllProducts(); 
            model.page = Math.Ceiling((double)allProducts.Count() / (double)model.ItemsPerPage);

            /* Pro zobrazeni 3 nejnovejsich produktu, plus je potom ulozim do modelu */
            var tmmmp = allProducts.OrderBy(t => t.date).Reverse().Take(3).ToList();
            foreach (var latest in tmmmp)
            {
                var image = _imageRepo.GetImage(latest.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                var firm = _firmRepo.GetFirm(latest.id_fir);

                var viewModel = new FilterProduct
                {
                    name = latest.name,
                    price = latest.price,
                    firm = firm.name,
                    image = image.link,
                    id_pr = latest.id_pr,
                    date = latest.date,
                    id_fir = latest.id_fir,
                    likes = latest.likes
                };
                model.LatestOffer.Add(viewModel);
            }

            foreach (var item in allProducts)
            {
                /* Prochazim produkty dokud countPage neni vetsi nez pocet polozek na stranku a pote polozky ukladam do modelu */
                if (countPage <= model.ItemsPerPage)
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
                        id_fir = item.id_fir,
                        likes = item.likes

                    };
                    model.ProductFilter.Add(viewModel);
                    countPage++;
                }
                /* Pokud uz mam vic produktu nez je pocet na stranku ukonci tuhle funkci */
                else
                {
                    break; 
                }
            }
        }

        /// <summary>
        /// Zobrazuje dany pocet produktu na vybranou stranku
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        public void GetFewBrowse(FilterProduct model, int page)
        {
            List<FilterProduct> tmp = new List<FilterProduct>(); 

            var allProducts = model.ProductFilter.Count(); // zjistuji pocet vsech produktu ktery prosli filtracemi
            model.page = Math.Ceiling((double)allProducts / (double)model.ItemsPerPage); // zjistuji kolik potrebuji celkem stranek
            // var fewProducts = _productRepo.GetFewProducts((page * 9 - 8), (page * 9));
            var min = (page * (model.ItemsPerPage) - (model.ItemsPerPage - 1)); 
            var max = (page * model.ItemsPerPage);

            model.LatestOffer = model.ProductFilter.OrderBy(t => t.date).Reverse().Take(3).ToList(); // pro zobrazeni latest offer z vyfiltrovanych produktu

            /* Prohledavam produkty v modelu od min - 1 kvuli indexovani, a proto i do max - 1 */
            for (int i = min - 1; i <= max - 1; i++)
            {
                if (i < model.ProductFilter.Count())
                {
                    /* Zjistuji si potrebne udaje o produktu a ukladam ho do pomocneho modelu */
                    var item = model.ProductFilter[i];
                    var result = _productRepo.GetProduct(item.id_pr);
                    var image = _imageRepo.GetImage(result.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                    var firm = _firmRepo.GetFirm(result.id_fir);

                    var viewModel = new FilterProduct
                    {
                        name = result.name,
                        price = result.price,
                        firm = firm.name,
                        image = image.link,
                        id_pr = result.id_pr,
                        date = result.date,
                        id_fir = result.id_fir,
                        likes = result.likes
                    };
                    tmp.Add(viewModel);
                }
            }
            model.ProductFilter = tmp; // zkopiruji cely pomocny model, ktery zobrazi jen ten pocet produktu ktere uzivatel chtel a na urcitou stranku
        }

        /// <summary>
        /// Ziskava vsechny produkty
        /// </summary>
        /// <param name="model"></param>
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
                    id_fir = item.id_fir,
                    likes = item.likes
                };
                //var pom = viewModel.FirmsArray[1];
                model.ProductFilter.Add(viewModel);
            }
        }

        /// <summary>
        /// Ziskava prvnich 9 produktu na stranku u kategorie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void GetProductsCategory(int id, FilterProduct model)
        {
            int countPage = 1;
            var cate = _cat_subRepo.GetProductCategory(id);
            foreach (var category in cate)
            {
                var res = _product_catRepository.Get_ProductId(category.id_cs);

                foreach (var product in res)
                {
                    if (countPage <= 9)
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
                            id_fir = result.id_fir,
                            likes = result.likes
                        };
                        model.ProductFilter.Add(viewModel);
                        countPage++;
                    }
                    else
                    {
                        countPage++;
                    }
                }
            }
            model.page = Math.Ceiling((double)countPage / 9.0);
        }

        /// <summary>
        /// Ziskava vsechny produkty z dane kategorie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
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
                            id_fir = result.id_fir,
                            likes = result.likes
                        };
                        model.ProductFilter.Add(viewModel);
                }
            }
        }

        /// <summary>
        /// Filtrovani podle jedne barvy
        /// </summary>
        /// <param name="model"></param>
        /// <param name="colourRGB"></param>
        /// <param name="tmpModel"></param>
        public void FilterOneColour(FilterProduct model, string colourRGB, List<FilterProduct> tmpModel)
        {
            foreach (var product in model.ProductFilter)
            {
                /* Prochazeim vsechny produkty a hledam produkt podle barvy a zaroven id produktu */
                var res = _iprod_colRepository.GetProductByRGB(colourRGB, product.id_pr);

                if (res != null)
                {
                    tmpModel.Add(FilterModel(model, product.id_pr)); // pokud jsem nalezl exitujici vyrobek zavolam funkci FilterModel kde podle toho zjistuji informace o produktu a ukladam je do modelu
                }
            }
        }

        public void FilterOneFirm(FilterProduct model, int idFirm, List<FilterProduct> tmpModel)
        {
            var pom = model.ProductFilter.Where(p => p.id_fir == idFirm).ToList();
            foreach(var item in pom)
            {
                tmpModel.Add(item); // pridavam jednotlive produkty do pomocneho listu
            }
        }

        public void FilterOneSize (FilterProduct model, int sizeId, List<FilterProduct> tmpModel)
        {
            foreach (var product in model.ProductFilter)
            {
                /* Prochazeim vsechny produkty a hledam produkt podle velikosti a zaroven id produktu */
                var idProduct = _iProd_siRepository.GetProductId_size(sizeId, product.id_pr);

                if (idProduct != null)
                {
                    tmpModel.Add(FilterModel(model, product.id_pr)); // pokud jsem nalezl exitujici vyrobek zavolam funkci FilterModel kde podle toho zjistuji informace o produktu a ukladam je do modelu
                }
            }
        }

        /// <summary>
        /// Ukladani produktu do modelu
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        /// <returns></returns>
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
                id_fir = result.id_fir,
            };
           
            return viewModel;
        }

        public FilterProduct SortFromLowest(FilterProduct model)
        {
            var tmp = model.ProductFilter.OrderBy(t => t.price).ToList();
            model.ProductFilter = tmp;
            return model;
        }
        public FilterProduct SortFromHighest(FilterProduct model)
        {
            var tmp = model.ProductFilter.OrderByDescending(t => t.price).ToList();
            model.ProductFilter = tmp;
            return model;
        }

        public FilterProduct SortFavourite(FilterProduct model)
        {
            var tmp = model.ProductFilter.OrderByDescending(t => t.likes).ToList();
            model.ProductFilter = tmp;
            return model;
        }

        public void SortByPrice (FilterProduct model, List<FilterProduct> tmpModel, int PriceMax, int PriceMin)
        {
            foreach (var product in model.ProductFilter)
            {
                if (product.price <= PriceMax && product.price >= PriceMin)
                {
                   tmpModel.Add(product); // pokud naleznu produkt ktery nevyhovuje podminkam je z modelu odstranen
                }
            }
            model.ProductFilter = tmpModel;
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
        public Comment GetCommentById(int id_com)
        {
            var result = _commentRepo.GetComment(id_com);
            return result;
        }

        

        /**********************************************/
        /*                INTRESTED IN                */
        /**********************************************/



        public InterestedIn ProductExists(int id)
        {
            var ProductWithDesc = _productRepo.GetProduct(id);
            if (ProductWithDesc != null)
            {
                var product = new InterestedIn { id_pr = id, name = ProductWithDesc.name, price = ProductWithDesc.price, description = ProductWithDesc.description, obrazek = _imageRepo.GetImage(ProductWithDesc.id_pr).link };
                return product;
            }
            else
                return null;
        }

        /************************************************/
        /*                LIKES                        */
        /**********************************************/

        public void RemoveLike(int id_com)
        {
             _commentRepo.RemoveLike(id_com);
        }

        public void RemoveDislike(int id_com)
        {
            _commentRepo.RemoveDislike(id_com);
        }

        public void AddLike(int id_com)
        {
            _commentRepo.AddLike(id_com);
        }

        public void AddDislike(int id_com)
        {
            _commentRepo.AddDislike(id_com);
        }

        public int AmountOfLikes(int id_com)
        {
            var amount = _commentRepo.AmountOfLikes(id_com);
            return amount;
        }

        public int AmountOfDislikes(int id_com)
        {
            var amount = _commentRepo.AmountOfDislike(id_com);
            return amount;
        }

        public int GetAllProductLikes(int id_pr)
        {
            var amount = _productRepo.GetAllProductLikes(id_pr);
            return amount;
        }


        public int AddLikeToProduct(int id_pr)
        {
            var result = _productRepo.AddLikeToProduct(id_pr);
            return result;
        }

        
    }
}
