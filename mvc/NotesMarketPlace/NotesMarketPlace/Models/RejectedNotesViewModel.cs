using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class RejectedNotesViewModel
    {
        public SellerNotes rejectednotestbl { get; set; }
        public NoteCategories notecategorytbl { get; set; }
        public Pager Pager { get; set; }
    }
}