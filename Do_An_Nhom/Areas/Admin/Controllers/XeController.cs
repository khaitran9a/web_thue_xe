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
    public class XeController : Controller
    {
        private QL_THUEXEEntities db = new QL_THUEXEEntities();

        // GET: Admin/Xe
        public ActionResult Index(string searchString)
        {
            var items = db.tblXes.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.tblDongXe.DongXe.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(items);
        }

        // GET: Admin/Xe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblXe tblXe = db.tblXes.Find(id);
            if (tblXe == null)
            {
                return HttpNotFound();
            }
            return View(tblXe);
        }

        // GET: Admin/Xe/Create
        public ActionResult Create()
        {
            ViewBag.MaDongXe = new SelectList(db.tblDongXes, "MaDongXe", "DongXe");
            ViewBag.MaLoaiDV = new SelectList(db.tblLoaiDichVus, "MaLoaiDV", "TenLoaiDV");
            ViewBag.MaMP = new SelectList(db.tblMucPhis, "MaMP", "MoTa");
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name");
            return View();
        }

        // POST: Admin/Xe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tblXe tblXe, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                tblXe.NgayDK = DateTime.UtcNow;
                db.tblXes.Add(tblXe);
                db.SaveChanges();
              
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = tblXe.MaXe;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "Xe" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    tblXe.Image = _FileName;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MaDongXe = new SelectList(db.tblDongXes, "MaDongXe", "DongXe", tblXe.MaDongXe);
            ViewBag.MaLoaiDV = new SelectList(db.tblLoaiDichVus, "MaLoaiDV", "TenLoaiDV", tblXe.MaLoaiDV);
            ViewBag.MaMP = new SelectList(db.tblMucPhis, "MaMP", "MoTa", tblXe.MaMP);
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblXe.User_id);
            return View(tblXe);
        }

        // GET: Admin/Xe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblXe tblXe = db.tblXes.Find(id);
            if (tblXe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDongXe = new SelectList(db.tblDongXes, "MaDongXe", "DongXe", tblXe.MaDongXe);
            ViewBag.MaLoaiDV = new SelectList(db.tblLoaiDichVus, "MaLoaiDV", "TenLoaiDV", tblXe.MaLoaiDV);
            ViewBag.MaMP = new SelectList(db.tblMucPhis, "MaMP", "MoTa", tblXe.MaMP);
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblXe.User_id);
            return View(tblXe);
        }

        // POST: Admin/Xe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaXe,User_id,MaMP,MaDongXe,MaLoaiDV,NgayDK,LuotXem,Rate,LuotThue,Image,isNew,isActive,Title,Sub")] tblXe tblXe, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblXe).State = EntityState.Modified;
                db.SaveChanges();
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = tblXe.MaXe;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "Product" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    tblXe.Image = _FileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDongXe = new SelectList(db.tblDongXes, "MaDongXe", "DongXe", tblXe.MaDongXe);
            ViewBag.MaLoaiDV = new SelectList(db.tblLoaiDichVus, "MaLoaiDV", "TenLoaiDV", tblXe.MaLoaiDV);
            ViewBag.MaMP = new SelectList(db.tblMucPhis, "MaMP", "MoTa", tblXe.MaMP);
            ViewBag.User_id = new SelectList(db.tblUsers, "User_id", "Name", tblXe.User_id);
            return View(tblXe);
        }

        // GET: Admin/Xe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblXe tblXe = db.tblXes.Find(id);
            if (tblXe == null)
            {
                return HttpNotFound();
            }
            return View(tblXe);
        }

        // POST: Admin/Xe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblXe tblXe = db.tblXes.Find(id);
            db.tblXes.Remove(tblXe);
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
