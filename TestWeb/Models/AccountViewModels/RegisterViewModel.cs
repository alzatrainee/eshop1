﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pernicek.Models.AccountViewModels
{
	public class RegisterViewModel : LegoViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string sec_name { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string mobile { get; set; }

        public string UrlAddress { get; set; }
        public int CheckRegister { get; set; }

        public int CheckIfUserExist { get; set; }
    }
}
