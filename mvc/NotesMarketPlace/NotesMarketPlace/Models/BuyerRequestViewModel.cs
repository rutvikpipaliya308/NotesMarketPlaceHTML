using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class BuyerRequestViewModel
    {
        public Downloads Downloadstbl { get; set; }
        public Users Userstbl { get; set; }
        public UserProfile UserProfiletbl { get; set; }
        public Pager Pager { get; set; }
    }

}