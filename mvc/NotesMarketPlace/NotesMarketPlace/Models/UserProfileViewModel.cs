using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class UserProfileViewModel
    {
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

        public Nullable<System.DateTime> DOB { get; set; }

        public Nullable<int> Gender { get; set; }
        
        [Required(ErrorMessage = "Code is required")]
        [MaxLength(5, ErrorMessage = "Countrycode length should be <5")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public string PhoneNumber_CountryCode { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(10, ErrorMessage = "Length should be <10")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digits allowed")]
        public string PhoneNumber { get; set; }

        public HttpPostedFileBase ProfilePicture { get; set; }

        [Required(ErrorMessage = "Address1 is required")]
        [MaxLength(100, ErrorMessage = "Address1 length should be <100")]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Address2 is required")]
        [MaxLength(100, ErrorMessage = "Address2 length should be <100")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City name is required")]
        [MaxLength(50, ErrorMessage = "City name length should be <50")]        
        public string City { get; set; }

        [Required(ErrorMessage = "State name is required")]
        [MaxLength(50, ErrorMessage = "State name length should be <50")]        
        public string State { get; set; }

        [Required(ErrorMessage = "Zipcode is required")]
        [MaxLength(50, ErrorMessage = "Zipcode length should be <50")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Country name is required")]
        [MaxLength(50, ErrorMessage = "Country name should be <50")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        public string Country { get; set; }

        [MaxLength(100, ErrorMessage = "Universityname should be <100")]        
        public string University { get; set; }

        [MaxLength(100, ErrorMessage = "Collegename should be <100")]        
        public string College { get; set; }

        public IEnumerable<ReferenceData> GenderList { get; set; }

    }
}