using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NotesMarketPlace.Models
{
    public class NotesReviewViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public decimal Rating { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
    }
}