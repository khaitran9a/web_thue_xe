using Do_An_Nhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Nhom.Controllers
{
    public class BlogDetailController : Controller
    {
        // GET: BlogDetail
        QL_THUEXEEntities db = new QL_THUEXEEntities();
        public ActionResult Index(string alias,int? id)
        {
            if (id != null)
            {
                var items = db.tblBlogs.Find(id);
                //var itemsReview = db.tb_Review.Where(i => i.ProductId == id).ToList();
                //ViewBag.NumReview = itemsReview.Count();
                return View(items);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult Comment(string name, string message)
        {
            try
            {
                tblBlogComment myItem = new tblBlogComment();
                myItem.Name = name;
               
                myItem.Cmt = message;
                myItem.CreatDate = DateTime.Now;
                db.tblBlogComments.Add(myItem);

                db.SaveChanges();
                return Json(new { message = true });
            }
            catch
            {
                return Json(new { message = false });
            }
        }
    }
}