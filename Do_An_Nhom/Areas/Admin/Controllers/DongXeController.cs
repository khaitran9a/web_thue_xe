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
    public class DongXeController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Admin/DongXe
        public ActionResult Index()
        {
            return View(db.tblDongXes.ToList());
        }

        // GET: Admin/DongXe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDongXe tblDongXe = db.tblDongXes.Find(id);
            if (tblDongXe == null)
            {
                return HttpNotFound();
            }
            return View(tblDongXe);
        }

        // GET: Admin/DongXe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DongXe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DongXe,HangXe,SoChoNgoi,MaDongXe")] tblDongXe tblDongXe)
        {
            if (ModelState.IsValid)
            {
                db.tblDongXes.Add(tblDongXe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDongXe);
        }

        // GET: Admin/DongXe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDongXe tblDongXe = db.tblDongXes.Find(id);
            if (tblDongXe == null)
            {
                return HttpNotFound();
            }
            return View(tblDongXe);
        }

        // POST: Admin/DongXe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DongXe,HangXe,SoChoNgoi,MaDongXe")] tblDongXe tblDongXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDongXe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDongXe);
        }

        // GET: Admin/DongXe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDongXe tblDongXe = db.tblDongXes.Find(id);
            if (tblDongXe == null)
            {
                return HttpNotFound();
            }
            return View(tblDongXe);
        }

        // POST: Admin/DongXe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDongXe tblDongXe = db.tblDongXes.Find(id);
            db.tblDongXes.Remove(tblDongXe);
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
