using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alza.Core.Module.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace Alza.Module.UserProfile.Dal.Entities
{
    public class User : Entity
    {

        public string name { get; set; }
        public string surname { get; set; }

    }
}