using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLeads.Data
{
    public class Listing
    {
        public DateTime DateCreated { get; set; }
        public string Text { get; set; }
        public string PhoneNumber { get; set; }
        public int Id { get; set; }
    }
}
