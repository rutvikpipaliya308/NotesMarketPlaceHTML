using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NotesMarketPlace.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [MaxLength(24, ErrorMessage = "Length should be <24")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}