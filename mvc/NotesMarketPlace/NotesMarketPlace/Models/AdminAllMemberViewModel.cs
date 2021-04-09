using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AdminAllMemberViewModel
    {
        public IEnumerable<allmember> Members { get; set; }
        public Pager Pager { get; set; }
    }

    public class allmember
    {
        public Users user { get; set; }
        public int underReviewNote { get; set; }
        public int publichedNote { get; set; }
        public int downloadedNote { get; set; }
        public decimal? totalExpenses { get; set; }
        public decimal? totalEarning { get; set; }
    }
}