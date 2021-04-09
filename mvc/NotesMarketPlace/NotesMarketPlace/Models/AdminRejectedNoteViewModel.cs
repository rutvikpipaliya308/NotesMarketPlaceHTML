using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AdminRejectedNoteViewModel
    {
        public SellerNotes reject { get; set; }
        public Users seller { get; set; }
        public Users rejector { get; set; }
        public Pager Pager { get; set; }
    }
}