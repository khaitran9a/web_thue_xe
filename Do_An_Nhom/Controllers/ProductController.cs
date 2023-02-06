using Do_An_Nhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Nhom.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        QL_THUEXEEntities db = new QL_THUEXEEntities();
        public ActionResult Index(string alias, int? id)
        {
            if (id != null)
            {
                var items = db.tblXes.Find(id);
                //var itemsReview = db.tb_Review.Where(i => i.ProductId == id).ToList();
                //ViewBag.NumReview = itemsReview.Count();
                return View(items);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult idCategories(int?id)
        {
            var items = db.tblXes.Take(4).ToList();
            if (id != null)
            {
                 items = db.tblXes.Where(i => i.MaDongXe == id).ToList();
                //var itemsReview = db.tb_Review.Where(i => i.ProductId == id).ToList();
                //ViewBag.NumReview = itemsReview.Count();
                return PartialView("_ProductByCate", items);
            }
            else
            {
                return PartialView("_ProductByCate", items);
            }
            
        }

    }
}