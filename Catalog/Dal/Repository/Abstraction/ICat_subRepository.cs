using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ICat_subRepository
    {
        Cat_sub GetCat_Sub(int id_cs);
        Category GetCategory(int id_cat);
    }
}
