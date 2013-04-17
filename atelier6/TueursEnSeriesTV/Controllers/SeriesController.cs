using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TueursEnSeries.Models;

namespace TueursEnSeries.Controllers
{
    public class SeriesController : Controller
    {
        //
        // GET: /Series/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Series/Search/SerieTitle
        public ActionResult Search(string q)
        {
            SerieList list = new SerieList();

            list.AppendSearch(HttpUtility.UrlEncode(q), true);
            ViewData["series"] = list.All;

            return View();
        }
    }
}
