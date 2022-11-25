using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Do_An_Nhom.Models;

namespace Do_An_Nhom.Areas.Admin.Controllers
{
    public class LoaiDichVuController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Admin/LoaiDichVu
        public ActionResult Index()
        {
            return View(db.tblLoaiDichVus.ToList());
        }

        // GET: Admin/LoaiDichVu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLoaiDichVu tblLoaiDichVu = db.tblLoaiDichVus.Find(id);
            if (tblLoaiDichVu == null)
            {
                return HttpNotFound();
            }
            return View(tblLoaiDichVu);
        }

        // GET: Admin/LoaiDichVu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiDichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiDV,TenLoaiDV")] tblLoaiDichVu tblLoaiDichVu)
        {
            if (ModelState.IsValid)
            {
                db.tblLoaiDichVus.Add(tblLoaiDichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblLoaiDichVu);
        }

        // GET: Admin/LoaiDichVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLoaiDichVu tblLoaiDichVu = db.tblLoaiDichVus.Find(id);
            if (tblLoaiDichVu == null)
            {
                return HttpNotFound();
            }
            return View(tblLoaiDichVu);
        }

        // POST: Admin/LoaiDichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiDV,TenLoaiDV")] tblLoaiDichVu tblLoaiDichVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLoaiDichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblLoaiDichVu);
        }

        // GET: Admin/LoaiDichVu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLoaiDichVu tblLoaiDichVu = db.tblLoaiDichVus.Find(id);
            if (tblLoaiDichVu == null)
            {
                return HttpNotFound();
            }
            return View(tblLoaiDichVu);
        }

        // POST: Admin/LoaiDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLoaiDichVu tblLoaiDichVu = db.tblLoaiDichVus.Find(id);
            db.tblLoaiDichVus.Remove(tblLoaiDichVu);
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
