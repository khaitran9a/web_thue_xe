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
    public class tblContactsController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Admin/tblContacts
        public ActionResult Index()
        {
            return View(db.tblContacts.ToList());
        }

        // GET: Admin/tblContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblContact tblContact = db.tblContacts.Find(id);
            if (tblContact == null)
            {
                return HttpNotFound();
            }
            return View(tblContact);
        }

        // GET: Admin/tblContacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/tblContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactId,Name,Email,Message,isRead,CreatDate,Phone,isActive")] tblContact tblContact)
        {
            if (ModelState.IsValid)
            {
                db.tblContacts.Add(tblContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblContact);
        }

        // GET: Admin/tblContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblContact tblContact = db.tblContacts.Find(id);
            if (tblContact == null)
            {
                return HttpNotFound();
            }
            return View(tblContact);
        }

        // POST: Admin/tblContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactId,Name,Email,Message,isRead,CreatDate,Phone,isActive")] tblContact tblContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblContact);
        }

        // GET: Admin/tblContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblContact tblContact = db.tblContacts.Find(id);
            if (tblContact == null)
            {
                return HttpNotFound();
            }
            return View(tblContact);
        }

        // POST: Admin/tblContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblContact tblContact = db.tblContacts.Find(id);
            db.tblContacts.Remove(tblContact);
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
