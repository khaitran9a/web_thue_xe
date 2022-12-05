using Do_An_Nhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;

namespace Do_An_Nhom.Controllers
{
    public class FleetController : Controller
    {
        QL_THUEXEEntities db = new QL_THUEXEEntities();
        // GET: Fleet
        public ActionResult FleetView(string dongXe, int? id)
        {
            var items = db.tblXes.Find(id);
            if (id == null)
            {
                ViewBag.CateTitle = "All";
            }
            else
            {
                ViewBag.CateId = items.MaDongXe;
                ViewBag.CateTitle = items.Title;
            }
            return View();
        }


        public ActionResult Index(string searchString, int? page)
        {
            
            if (page == null) page = 1;


            var items = db.tblXes.OrderBy(x => x.MaXe).ToList();

            int pageSize = 6;

            int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.tblDongXe.DongXe.ToLower().Contains(searchString.ToLower())).ToList();

            }

            return View(items.ToList().ToPagedList(pageNumber, pageSize));
        }


        public ActionResult FleetCar(string searchString, int? id, int? page)
        {
            var items = db.tblXes.Where(i => (bool)i.isActive).ToList();
            if (id != null)
            {
                items = items.Where(i => i.MaDongXe == id).ToList();
            }
            int pageSize = 6;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.tblDongXe.DongXe.ToLower().Contains(searchString.ToLower())).ToList();

            }

            int pageNumber = (page ?? 1);
            return PartialView("_FleetCar", items.ToPagedList(pageNumber, pageSize));
        }
    }
}