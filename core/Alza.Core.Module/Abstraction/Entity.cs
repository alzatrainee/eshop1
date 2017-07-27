﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public abstract class Entity : IEntityWithTypedId<int>
    {
        [Key]
        public int id_user
        {
            get; set;
        }

        //vhodne :
        //CreatedDate
        //LastChangeDate
        
    }
}
