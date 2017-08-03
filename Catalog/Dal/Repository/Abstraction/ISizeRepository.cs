using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Dal.Entities;
using System.Linq;

namespace Catalog.Dal.Repository
{
    public interface ISizeRepository
    {
        List<Size> GetAllSizes(); 
        Size AddSize(Size entity);
        void RemoveSize(Size entity);
        Size UpdateSize(Size entity);

    }
}
