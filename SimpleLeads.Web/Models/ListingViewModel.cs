using System;
using SimpleLeads.Data;

namespace SimpleLeads.Web.Models
{
    public class ListingViewModel
    {
        public List<Listing> Listings { get; set; }
        public List<int> UserIds { get; set; }
    }
}
