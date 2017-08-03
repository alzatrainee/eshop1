using Alza.Core.Module.Abstraction;
using Alza.Core.Module.Http;
using Catalog.Dal.Entities;
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
        private Iprod_colRepository _iprod_colRepository;

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
                var result = _productRepo.QueryGetProducts().ToList();
                
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

        public Prod_col GetRgb (int id)
        {
            var result = _iprod_colRepository.GetRGB(id);
            return (result);

        }

        public Colour GetColour(string id)
        {
            var result = _colourRepo.GetColour(id);
            return (result);

        }






    }

    
}
