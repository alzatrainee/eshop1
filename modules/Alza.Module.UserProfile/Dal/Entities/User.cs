using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alza.Core.Module.Abstraction;
using System.ComponentModel.DataAnnotations;
using Alza.Module.UserProfile.Business;

namespace Alza.Module.UserProfile.Dal.Entities
{
    public class User// : Entity
    {
        public User()
        {}
        public User(int id, string name, string surname, string mobile)
        {
            id_user = id;
            this.name = name;
            this.surname = surname;
            this.mobile = mobile;
           
        }

        [Key]
        public int id_user { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

       
        [Phone]
        public string mobile { get; set; }
    }
}