using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class MySoldNotesViewModel
    {
        public Downloads downloadtbl { get; set; }
        public Users usertbl { get; set; }
        public Pager Pager { get; set; }
    }
}