using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class NoteDetailViewModel
    {
        public SellerNotes sellnote { get; set; }
        public SellerNotesReviews notereview { get; set; }
        public int reportedIssue { get; set; }

        public int AvgRating { get; set; }

        public string notePreview { get; set; }
        public int TotalReview { get; set; }
        public IEnumerable<ReviewsModel> notesreview { get; set; }
    }

    public class ReviewsModel
    {
        public Users users { get; set; }
        public UserProfile userProfiles { get; set; }
        public SellerNotesReviews sellerNotesReviews { get; set; }
    }


}