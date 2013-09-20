using System;
using System.Web.Mvc;

using BookReviews.ThirdParty.Twitter;

namespace BookReviews.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}