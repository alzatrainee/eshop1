using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Dal.Entities;


namespace Catalog.Dal.Repository.Abstraction
{
    public interface Iprod_colRepository
    {
        List<Prod_col> GetRGB(int id);
        Prod_col GetProductByRGB(string id, int id_prod);
    }
}
