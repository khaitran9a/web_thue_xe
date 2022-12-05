using Do_An_Nhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Do_An_Nhom.Controllers
{
    public class BlogController : Controller
    {
        QL_THUEXEEntities db = new QL_THUEXEEntities();
        // GET: Offer


        public ActionResult Index(int? page , string searchString)
        {

            if (page == null) page = 1;


            var items = db.tblBlogs.OrderBy(x => x.BlogId).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }
            int pageSize = 3;

            int pageNumber = (page ?? 1);

            return View(items.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BlogList(string searchString, int? page)
        {
            var items = db.tblBlogs.ToList();
            if (searchString != null)
            {
                items = items.Where(i => i.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }
            int pageSize = 3;

            int pageNumber = (page ?? 1);
            return PartialView("_BlogList", items.ToPagedList(pageNumber, pageSize));
        }
    }
}