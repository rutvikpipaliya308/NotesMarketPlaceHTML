using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AdminSpamNotesIssuesViewModel
    {
        public IEnumerable<SellerNotesReportedIssues> issue { get; set; }
        public Pager Pager { get; set; }
    }
}