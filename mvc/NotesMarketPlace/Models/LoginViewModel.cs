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
        [Required(ErrorMessage ="Invalid email")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        public string Email { get; set; }

        [Required(ErrorMessage ="invalid password")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}