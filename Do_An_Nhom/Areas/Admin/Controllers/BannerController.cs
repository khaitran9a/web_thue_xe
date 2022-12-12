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
    public class BannerController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Admin/Banner
        public ActionResult Index()
        {
            var tblBanners = db.tblBanners.Include(t => t.tblUser);
            return View(tblBanners.ToList());
        }

        // GET: Admin/Banner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBanner tblBanner = db.tblBanners.Find(id);
            if (tblBanner == null)
            {
                return HttpNotFound();
            }
            return View(tblBanner);
        }

        // GET: Admin/Banner/Create
        public ActionResult Create()
        {
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name");
            return View();
        }

        // POST: Admin/Banner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblBanner tblBanner, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                db.tblBanners.Add(tblBanner);
                db.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = tblBanner.BannerId;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "Banner" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/banners"), _FileName);
                    uploadhinh.SaveAs(_path);
                    tblBanner.Image = _FileName;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblBanner.User_id);
            return View(tblBanner);
        }

        // GET: Admin/Banner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBanner tblBanner = db.tblBanners.Find(id);
            if (tblBanner == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblBanner.User_id);
            return View(tblBanner);
        }

        // POST: Admin/Banner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblBanner tblBanner, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBanner).State = EntityState.Modified;
                db.SaveChanges();


                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = tblBanner.BannerId;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "Banner" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/banners"), _FileName);
                    uploadhinh.SaveAs(_path);
                    tblBanner.Image = _FileName;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblBanner.User_id);
            return View(tblBanner);
        }

        // GET: Admin/Banner/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBanner tblBanner = db.tblBanners.Find(id);
            if (tblBanner == null)
            {
                return HttpNotFound();
            }
            return View(tblBanner);
        }

        // POST: Admin/Banner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBanner tblBanner = db.tblBanners.Find(id);
            db.tblBanners.Remove(tblBanner);
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
