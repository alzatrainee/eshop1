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
        private ILogger<CatalogService> _logger;
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
            ICat_subRepository cat_subRepo
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
        public AlzaAdminDTO getAllColours()
        {
            try
            {
                var result = _colourRepo.getAllColours().ToList();
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
            //throw new NotImplementedException();
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

        public List<Cat_sub> GetCat_Sub(int id_cs)
        {
            var result = _cat_subRepo.GetCat_Sub(id_cs);
            return result;
        }
        public List<Category> GetCategory(int id_cat)
        {
            var result = _cat_subRepo.GetCategory(id_cat);
            return result;
        }
        public List<Prod_si> GetID_size(int id)
        {
            var result = _iProd_siRepository.GetId_size(id);
            return (result);
        }
    }

    
}
