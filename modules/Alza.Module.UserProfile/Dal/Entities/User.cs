﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alza.Core.Module.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace Alza.Module.UserProfile.Dal.Entities
{
    public class User// : Entity
    {
        [Key]
        public int id_user { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

        [Phone]
        public string mobile { get; set; }

    }
}