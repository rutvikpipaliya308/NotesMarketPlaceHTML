using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class MemberDetailViewModel
    {
        public IEnumerable<detailmodel> memDetails { get; set; }
        public Users member { get; set; }
        public UserProfile memberProfile { get; set; }
        public Pager Pager { get; set; }
    }

    public class detailmodel
    {
        public SellerNotes sellnote { get; set; }
        public int totalDownload { get; set; }
        public decimal? totalEarning { get; set; }
    }
}