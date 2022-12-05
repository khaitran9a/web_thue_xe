using Do_An_Nhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Nhom.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        QL_THUEXEEntities db = new QL_THUEXEEntities();
        public ActionResult Index()
        {
            if (Session["Admin"] == "Admin")
            {       
                return View();
            }
            return  HttpNotFound();
        }
    }
}