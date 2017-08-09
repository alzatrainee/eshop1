using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IImageRepository
    {
        List<Image> GetAllImages(int id_pr);
        Image GetImage(int id_pr);
    }
}
