using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AdminNotesUnderReviewViewModel
    {
        public SellerNotes sellNotes { get; set; }
        public Users user { get; set; }       
        public Pager Pager { get; set; }
    }
}