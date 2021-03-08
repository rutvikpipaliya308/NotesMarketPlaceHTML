using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class DashboardViewModel
    {

        public IEnumerable<SellerNotes> InProgressNote { get; set; }
        public IEnumerable<SellerNotes> PublishedNote { get; set; }

        public int MyDownload { get; set; }
        public int NumberOfSoldNote { get; set; }
        public int MoneyEarned { get; set; }
        public int MyRejectedNote { get; set; }
        public int BuyerRequest { get; set; }
    }
}