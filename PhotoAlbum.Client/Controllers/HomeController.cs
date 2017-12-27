using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Error(string message)
        {
            var model = new HandleErrorInfo(new Exception(message), "NaN", "NaN");
            return View(model);
        }
    }
}