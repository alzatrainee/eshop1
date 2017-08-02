using Alza.Core.Module.Abstraction;
using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IColourRepository //: IRepository<Colour>
    {
        IQueryable<Colour> getAllColours();
        Colour FindByName(string name);
        //Colour Add(Colour entity);
        //Colour Remove(Colour entity);

    }
}
