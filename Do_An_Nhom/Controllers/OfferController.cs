using Do_An_Nhom.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Nhom.Controllers
{
    public class OfferController : Controller
    {
        QL_THUEXEEntities db = new QL_THUEXEEntities();
        // GET: Offer


        public ActionResult OfferView(int? page)
        {

            if (page == null) page = 1;


            var items = db.tblXes.OrderBy(x => x.MaXe).ToList();

            int pageSize = 6;

            int pageNumber = (page ?? 1);

            return View(items.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult OfferCar(string dongXe, int? id, int? page)
        {
            var items = db.tblXes.Where(i => (bool)i.isActive).ToList();
            if (id != null)
            {
                items = items.Where(i => i.MaDongXe == id).ToList();
            }
            int pageSize = 6;

            int pageNumber = (page ?? 1);
            return PartialView("_OfferCar", items.ToPagedList(pageNumber, pageSize));
        }
    }
}