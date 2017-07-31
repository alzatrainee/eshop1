using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Pernicek.Models.ManageViewModels
{
    public class IndexViewModel_1
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        [Display(Name = "First name")]
        public string name { get; set; }

        [Display(Name = "Last name")]
        public string sec_name { get; set; }
    }
}
