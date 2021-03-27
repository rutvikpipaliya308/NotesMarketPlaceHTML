using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NotesMarketPlace.Models
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage = "FirstName is Required")]
        [MaxLength(50, ErrorMessage = "Length should be <50")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DisplayName("Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is Required")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s]*$", ErrorMessage = "Use letters only")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Comment is Required")]
        [DisplayName("Comments/Questions")]
        public string Comments { get; set; }
    }
}