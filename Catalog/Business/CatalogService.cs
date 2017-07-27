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


        public AlzaAdminDTO getAllProducts()
        {
            try
            {
                var result = _productRepo.getAllProducts().ToList();
                return AlzaAdminDTO.Data(result);
                
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
       
        

     
        
    }

    
}
