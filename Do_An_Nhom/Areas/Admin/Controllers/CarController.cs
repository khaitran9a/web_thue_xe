using Do_An_Nhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Nhom.Areas.Admin.Controllers
{
    public class CarController : Controller
    {
        // GET: Admin/Car
        QL_THUEXEEntities dbObj = new QL_THUEXEEntities();
        public ActionResult Index()
        {
            var listCar = dbObj.tblXes.ToList();
            return View(listCar);
        }
    }
}