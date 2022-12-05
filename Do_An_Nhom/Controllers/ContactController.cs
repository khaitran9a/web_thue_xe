using Do_An_Nhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Nhom.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        QL_THUEXEEntities db = new QL_THUEXEEntities();
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(string name, string phone, string email, string message)
        {
            try
            {
                tblContact myItem = new tblContact();
                myItem.Name = name;
                myItem.Phone = phone;
                myItem.Email = email;
                myItem.Message = message;
                myItem.CreatDate = DateTime.Now;
                db.tblContacts.Add(myItem);
                db.SaveChanges();
                return Json(new { message = true });
            }
            catch
            {
                return Json(new { message = false });
            }
        }
    }
}