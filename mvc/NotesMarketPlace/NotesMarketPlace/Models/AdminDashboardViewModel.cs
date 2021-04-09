using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AdminDashboardViewModel
    {
        public publishednotes Notes { get; set; }
        public int totalDownload { get; set; }
        public long totalSize { get; set; }        
        public Pager Pager { get; set; }
    }  
    
    public class publishednotes
    {
        public SellerNotes notes { get; set; }
        public Users users { get; set; }
    }
    
}