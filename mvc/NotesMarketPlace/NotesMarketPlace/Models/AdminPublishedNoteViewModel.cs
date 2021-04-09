using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AdminPublishedNoteViewModel
    {
        public APublishedNote pNote { get; set; }
        public int TotalDownload { get; set; }
        public Pager Pager { get; set; }
    }

    public class APublishedNote
    {
        public SellerNotes psnotes { get; set; }
        public Users user { get; set; }
        public Users auser { get; set; }
    }
}