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
    public class SignUpController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: SignUp
        public ActionResult Index()
        {
            var tblUsers = db.tblUsers.Include(t => t.LoaiNguoiDung);
            return View(tblUsers.ToList());
        }

        // GET: SignUp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: SignUp/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiNguoiDungs, "MaLoai", "TenLoai");
            return View();
        }

        // POST: SignUp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_id,Name,Name_login,Email,Password,Phone,Address,Birthday,MaLoai,Date_signup,IsAdmin")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.tblUsers.Add(tblUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiNguoiDungs, "MaLoai", "TenLoai", tblUser.MaLoai);
            return View(tblUser);
        }

        // GET: SignUp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiNguoiDungs, "MaLoai", "TenLoai", tblUser.MaLoai);
            return View(tblUser);
        }

        // POST: SignUp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_id,Name,Name_login,Email,Password,Phone,Address,Birthday,MaLoai,Date_signup,IsAdmin")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiNguoiDungs, "MaLoai", "TenLoai", tblUser.MaLoai);
            return View(tblUser);
        }

        // GET: SignUp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: SignUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            db.tblUsers.Remove(tblUser);
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
