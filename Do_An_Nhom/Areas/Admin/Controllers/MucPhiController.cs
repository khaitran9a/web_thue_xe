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
    public class MucPhiController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Admin/MucPhi
        public ActionResult Index()
        {
            return View(db.tblMucPhis.ToList());
        }

        // GET: Admin/MucPhi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMucPhi tblMucPhi = db.tblMucPhis.Find(id);
            if (tblMucPhi == null)
            {
                return HttpNotFound();
            }
            return View(tblMucPhi);
        }

        // GET: Admin/MucPhi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MucPhi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMP,DonGia,MoTa")] tblMucPhi tblMucPhi)
        {
            if (ModelState.IsValid)
            {
                db.tblMucPhis.Add(tblMucPhi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblMucPhi);
        }

        // GET: Admin/MucPhi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMucPhi tblMucPhi = db.tblMucPhis.Find(id);
            if (tblMucPhi == null)
            {
                return HttpNotFound();
            }
            return View(tblMucPhi);
        }

        // POST: Admin/MucPhi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMP,DonGia,MoTa")] tblMucPhi tblMucPhi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMucPhi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblMucPhi);
        }

        // GET: Admin/MucPhi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMucPhi tblMucPhi = db.tblMucPhis.Find(id);
            if (tblMucPhi == null)
            {
                return HttpNotFound();
            }
            return View(tblMucPhi);
        }

        // POST: Admin/MucPhi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMucPhi tblMucPhi = db.tblMucPhis.Find(id);
            db.tblMucPhis.Remove(tblMucPhi);
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
