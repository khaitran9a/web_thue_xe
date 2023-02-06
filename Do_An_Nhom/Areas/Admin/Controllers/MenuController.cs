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
    public class MenuController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Admin/Menu
        public ActionResult Index(string searchString)
        {
            var items = db.tblMenus.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(items);
        }

        // GET: Admin/tblMenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMenu tblMenus = db.tblMenus.Find(id);
            if (tblMenus == null)
            {
                return HttpNotFound();
            }
            return View(tblMenus);
        }

        // GET: Admin/tblMenus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/tblMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuId,Title,Alias,Description,Position,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] tblMenu tblMenus)
        {
            if (ModelState.IsValid)
            {
                db.tblMenus.Add(tblMenus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblMenus);
        }

        // GET: Admin/tblMenus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMenu tblMenus = db.tblMenus.Find(id);
            if (tblMenus == null)
            {
                return HttpNotFound();
            }
            return View(tblMenus);
        }

        // POST: Admin/tblMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuId,Title,Alias,Description,Position,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] tblMenu tblMenus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMenus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblMenus);
        }

        // GET: Admin/tblMenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMenu tblMenus = db.tblMenus.Find(id);
            if (tblMenus == null)
            {
                return HttpNotFound();
            }
            return View(tblMenus);
        }

        // POST: Admin/tblMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMenu tblMenus = db.tblMenus.Find(id);
            db.tblMenus.Remove(tblMenus);
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
