using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Models
{
    public class SearchNoteViewModel
    {
       public SellerNotes note { get; set; }
        public int averageRating { get; set; }
        public int totalRating { get; set; }
        public int totalSpam { get; set; }
        public Pager Pager { get; set; }
    } 

    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize)
        {
            // calculate total, start and end pages
            int paginationSize = 7;
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 3;
            var endPage = currentPage + 3;
            if (startPage <= 0)
            {
                endPage = totalPages >= paginationSize ? paginationSize : totalPages;
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                startPage = totalPages >= paginationSize ? endPage-(paginationSize-1) : 1;                
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}