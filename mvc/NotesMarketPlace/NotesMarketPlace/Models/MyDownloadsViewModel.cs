using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class MyDownloadsViewModel
    {
        public Downloads mydownloadtbl { get; set; }
        public Users userstbl { get; set; }
        public Pager Pager { get; set; }
    }
}