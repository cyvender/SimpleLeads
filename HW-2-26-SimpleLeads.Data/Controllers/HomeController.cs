using System.Diagnostics;
using HW_2_26_SimpleLeads.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_2_26_SimpleLeads.Data.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
