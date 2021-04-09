using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NotesMarketPlace.Models
{
    public class ManageCategoryViewModel
    {
        public IEnumerable<ManageCat> Category { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        public string categoryName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string description { get; set; }

        public int ID { get; set; }

    }
    public class ManageCat
    {
        public NoteCategories categories { get; set; }
        public Users users { get; set; }
        public Pager Pager { get; set; }
    }
}