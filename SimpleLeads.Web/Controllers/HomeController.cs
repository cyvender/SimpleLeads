using System.Diagnostics;
using System.Text.Json;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleLeads.Data;
using SimpleLeads.Web.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleLeads.Web.Controllers 
{
    
    public class HomeController : Controller
    {
        private string _connectionString = "Data Source=.\\sqlexpress;Initial Catalog=SimpleLeads;Integrated Security=True";

        public IActionResult AddListing()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddListing(Listing listing)
        {


            ListingViewModel userIds = HttpContext.Session.Get<ListingViewModel>("userIds");

            var slDB = new SimpleLeadsDB(_connectionString);
            int id = slDB.AddListing(listing);

            if(userIds == null)
            {
                userIds = new ListingViewModel
                {
                    UserIds = new List<int>()
                };
            }

            userIds.UserIds.Add(id);

            HttpContext.Session.Set("userIds", userIds);

            return Redirect("/home/listings");
        }

        public IActionResult Listings()
        {
            ListingViewModel userIds = HttpContext.Session.Get<ListingViewModel>("userIds");
            HttpContext.Session.Set("userIds", userIds);

            var slDB = new SimpleLeadsDB(_connectionString);
            userIds.Listings = slDB.GetListings();
            return View(userIds);
        }

        //[HttpPost]
        public IActionResult DeleteListing(int id)
        {
            //ListingViewModel userIds = HttpContext.Session.Get<ListingViewModel>("userIds");
            //HttpContext.Session.Set("userIds", userIds);

            var slDB = new SimpleLeadsDB(_connectionString);
            slDB.DeletLising(id);
            return Redirect("/home/listings");
        }


    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}
