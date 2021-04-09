using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NotesMarketPlace.Models
{
    public class SystemConfigViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Use valid email address")]
        [EmailAddress]
        public string SupportEmail { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [MaxLength(10, ErrorMessage = "Length should be <10")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digits allowed")]
        public string SupportContact { get; set; }                

        [Required(ErrorMessage = "Email addresses is required")]
        public string EmailAddresses { get; set; }
        
        public HttpPostedFileBase NotePicture { get; set; }
        
        public HttpPostedFileBase UserPicture { get; set; }

        [Required(ErrorMessage = "Facebook url is required")]
        public string FacebookURL { get; set; }

        [Required(ErrorMessage = "Twitter url is required")]
        public string TwitterURL { get; set; }

        [Required(ErrorMessage = "LInkedin url is required")]
        public string LinkedinURL { get; set; }
    }
}