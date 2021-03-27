using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NotesMarketPlace.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,24}$", ErrorMessage = "Password must be between 8 and 24 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string oldPassword { get; set; }

        [Required(ErrorMessage = "Newpassword is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,24}$", ErrorMessage = "Password must be between 8 and 24 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string newPassword { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Password and Confirm password is not match")]
        public string confirmPassword { get; set; }
    }
}