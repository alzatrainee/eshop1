using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IFirmRepository
    {
        Firm GetFirm(int id_fir);
        List<Firm> GetAllFirms();
    }
}
