using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AdminAllDownloadedViewModel
    {
        public Downloads download { get; set; }
        public Users seller { get; set; }
        public Users buyer { get; set;}
        public Pager Page { get; set; }
    }
}