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
        private ISizeRepository _sizeRepo;
        private Iprod_colRepository _iprod_colRepository;
        private IProd_siRepository _iProd_siRepository;

        public CatalogService(
            Iprod_colRepository iprod_colRepository,
            IProductRepository productRepo,
            IColourRepository colourRepo
          )
        {
            _productRepo = productRepo;
            _colourRepo = colourRepo;
            _iprod_colRepository = iprod_colRepository;
        }

        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------Products----------------------------------------------
        public AlzaAdminDTO GetAllProducts()
        {
            try
            {
                var result = _productRepo.GetAllProducts();
                
                return AlzaAdminDTO.Data(result);
                
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
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

       // public List<Prod_si> GetId_size(int Id)
        //{
       //     var result = _iProd_siRepository.GetId_size(Id);
         //   return (result);

        //}
        
    }

    
}
