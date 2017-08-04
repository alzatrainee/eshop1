using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
   public interface IProd_siRepository
    {
        List<Prod_si> GetId_size( int Id ); // predavame ID prodktu s ucelem zjisteni id_si;
    }
}
