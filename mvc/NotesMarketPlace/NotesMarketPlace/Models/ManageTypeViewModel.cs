using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NotesMarketPlace.Models
{
    public class ManageTypeViewModel
    {
        public IEnumerable<ManageType> NoteTypes { get; set; }

        [Required(ErrorMessage = "Typename  is required")]
        [MaxLength(100, ErrorMessage = "Length should be <100")]
        public string typeName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string description { get; set; }

        public int ID { get; set; }
    }

    public class ManageType
    {
        public NoteTypes Type { get; set; }
        public Users users { get; set; }
        public Pager Pager { get; set; }
    }
}