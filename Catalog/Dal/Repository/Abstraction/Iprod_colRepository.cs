﻿using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Dal.Entities;


namespace Catalog.Dal.Repository.Abstraction
{
    public interface Iprod_colRepository
    {
        Prod_col GetRGB(int id);
    }
}