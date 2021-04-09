using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NotesMarketPlace.Models
{
    public class ManageCountryViewModel
    {
        public IEnumerable<ManageCountry> Country { get; set; }

        [Required(ErrorMessage = "Country name is required")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        public string countryName { get; set; }

        [Required(ErrorMessage = "Country code is required")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only digits allowed")]
        public string countryCode { get; set; }

        public int ID { get; set; }
    }
    public class ManageCountry
    {
        public Countries countries { get; set; }
        public Users users { get; set; }
        public Pager Pager { get; set; }
    }

}