using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Do_An_Nhom.Models;

namespace Do_An_Nhom.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Admin/Blog
        public ActionResult Index(string searchString)
        {
            var items = db.tblBlogs.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(items);
        }

        // GET: Admin/Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBlog tblBlog = db.tblBlogs.Find(id);
            if (tblBlog == null)
            {
                return HttpNotFound();
            }
            return View(tblBlog);
        }

        // GET: Admin/Blog/Create
        public ActionResult Create()
        {
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name");
            return View();
        }

        // POST: Admin/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogId,Title,CreatDate,SubTitle,Text1,Image1,User_id")] tblBlog tblBlog, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                tblBlog.CreatDate = DateTime.UtcNow;
                db.tblBlogs.Add(tblBlog);
                db.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = tblBlog.BlogId;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "BlogImage" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/Blogs"), _FileName);
                    uploadhinh.SaveAs(_path);
                    tblBlog.Image1 = _FileName;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblBlog.User_id);
            return View(tblBlog);
        }

        // GET: Admin/Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBlog tblBlog = db.tblBlogs.Find(id);
            if (tblBlog == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblBlog.User_id);
            return View(tblBlog);
        }

        // POST: Admin/Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogId,Title,CreatDate,SubTitle,Text1,Image1,User_id")] tblBlog tblBlog, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBlog).State = EntityState.Modified;
                db.SaveChanges();
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = tblBlog.BlogId;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "BlogImage" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/Blogs"), _FileName);
                    uploadhinh.SaveAs(_path);
                    tblBlog.Image1 = _FileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblBlog.User_id);
            return View(tblBlog);
        }

        // GET: Admin/Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBlog tblBlog = db.tblBlogs.Find(id);
            if (tblBlog == null)
            {
                return HttpNotFound();
            }
            return View(tblBlog);
        }

        // POST: Admin/Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBlog tblBlog = db.tblBlogs.Find(id);
            db.tblBlogs.Remove(tblBlog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
