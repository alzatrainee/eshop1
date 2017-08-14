using Alza.Core.Module.Abstraction;
using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ICategoryRepository 
    {
        Category AddCategory(Category entity);
        void RemoveCategory(Category entity);
        Category UpdateCategory(Category entity);
        List<Category> GetAllCategory();
        List<Category> GetCategoryWithName(string name);
    }
}
