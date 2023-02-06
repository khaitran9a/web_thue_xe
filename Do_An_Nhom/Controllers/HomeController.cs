using Do_An_Nhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Nhom.Controllers
{
    public class HomeController : Controller
    {
        QL_THUEXEEntities db = new QL_THUEXEEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MenuTop()
        {
            var items = db.tblMenus.Where(i => (bool)i.IsActive).OrderBy(i => i.Positions).ToList();
            return PartialView("_MenuHome", items);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Banner()
        {
            var items = db.tblBanners.Where(i => (bool)i.isActive).Take(3).ToList();
            return PartialView("_HomeBanner", items);
        }

        public ActionResult Offer()
        {
            var items = db.tblXes.Where(i => (bool)i.isActive).Take(3).ToList();
            return PartialView("_HomeOffer", items);
        }

        public ActionResult NewProduct()
        {
            var items = db.tblXes.Where(i => (bool)i.isNew && (bool)i.isActive).Take(3).ToList();
            return PartialView("_NewProduct", items);
        }

        public ActionResult Review()
        {
            var items = db.tblContacts.Where(i => (bool)i.isActive).ToList();
            return PartialView("_HomeReview", items);
        }

        public ActionResult HomeBlog()
        {
            var items = db.tblBlogs.Take(3).ToList();
            return PartialView("_HomeBlog", items);
        }
    }
}