using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Do_An_Nhom.Models;

namespace Do_An_Nhom.Controllers
{
    public class BillController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Bill
        public ActionResult Index()
        {
            var tblHopDongs = db.tblHopDongs.Include(t => t.tblUser).Include(t => t.tblXe);
            return View(tblHopDongs.ToList());
        }

        // GET: Bill/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHopDong tblHopDong = db.tblHopDongs.Find(id);
            if (tblHopDong == null)
            {
                return HttpNotFound();
            }
            return View(tblHopDong);
        }

        // GET: Bill/Create
        public ActionResult Create()
        {
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name");
            ViewBag.MaXe = new SelectList(db.tblXes, "MaXe", "Image");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string name, string phone, string email, int songay, string noithue, string noiitra, DateTime ngaythue /*,int userId*/)
        {
            try
            {
                tblHopDong myItem = new tblHopDong();
                //myItem.User_id = userId;
                myItem.TenDayDu = name;
                myItem.SoDienThoai = phone;
                myItem.Email = email;
                myItem.NoiThue = noithue;
                myItem.NoiTra = noiitra;
                myItem.SoNgayThue = songay;

                int tien = 200000;

                myItem.NgayNhanXe = ngaythue;
                myItem.TienDatCoc = songay * tien;
                db.tblHopDongs.Add(myItem);
                db.SaveChanges();
                return Json(new { message = true });
            }
            catch
            {
                return Json(new { message = false });
            }

        }

        // GET: Bill/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHopDong tblHopDong = db.tblHopDongs.Find(id);
            if (tblHopDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblHopDong.User_id);
            ViewBag.MaXe = new SelectList(db.tblXes, "MaXe", "Image", tblHopDong.MaXe);
            return View(tblHopDong);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHopDong,MaXe,NgayNhanXe,NgayTraXe,GhiChu,TienDatCoc,User_id")] tblHopDong tblHopDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblHopDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblHopDong.User_id);
            ViewBag.MaXe = new SelectList(db.tblXes, "MaXe", "Image", tblHopDong.MaXe);
            return View(tblHopDong);
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHopDong tblHopDong = db.tblHopDongs.Find(id);
            if (tblHopDong == null)
            {
                return HttpNotFound();
            }
            return View(tblHopDong);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblHopDong tblHopDong = db.tblHopDongs.Find(id);
            db.tblHopDongs.Remove(tblHopDong);
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
