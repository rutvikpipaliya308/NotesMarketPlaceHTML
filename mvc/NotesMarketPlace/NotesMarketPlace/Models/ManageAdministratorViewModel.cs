using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NotesMarketPlace.Models
{
    public class ManageAdministratorViewModel
    {
        public IEnumerable<ManageAdmini> allAdmins { get; set; }

        public int ID { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [MaxLength(50, ErrorMessage = "Firstname length should be <50")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(50, ErrorMessage = "Lastname length should be <50")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Use valid email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [MaxLength(5, ErrorMessage = "Countrycode length should be <5")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digits allowed")]
        public string PhoneNumber_CountryCode { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(10, ErrorMessage = "Length should be <10")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public string PhoneNumber { get; set; }
    }

    public class ManageAdmini
    {
        public Users admins { get; set; }
        public UserProfile adminProfiles { get; set; }
        public Pager Pager { get; set; }
    }
}