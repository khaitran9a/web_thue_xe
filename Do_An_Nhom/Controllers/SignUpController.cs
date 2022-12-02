using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Do_An_Nhom.Models;
using static Do_An_Nhom.Models.tblUser;

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


        [HttpGet]
        public ActionResult ViewUser()
        {
            var userList = db.tblUsers.ToList();
            return View(userList);
        }

        public ActionResult Profile()
        {
            if (Session["User"] != null)
            {
                var userid = Int32.Parse(Session["UserID"].ToString());

                var x = db.tblUsers.SingleOrDefault(c => c.User_id == userid);
                return View(x);
            }
            return RedirectToAction("Login");

        }


        public ActionResult Change()
        {
            if (Session["UserID"] != null)
            {
                var userid = Int32.Parse(Session["UserID"].ToString());
                var x = db.tblUsers.SingleOrDefault(c => c.User_id == userid);

                return View(x);
            }
            return RedirectToAction("Login");
        }

        public ActionResult Password()
        {
            if (Session["UserID"] != null)
            {

                return View();
            }
            return RedirectToAction("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Password(EditPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var userid = Int32.Parse(Session["UserID"].ToString());
                var _User = db.tblUsers.SingleOrDefault(c => c.User_id == userid);
                if (_User != null)
                {
                    var pass = new LoginModel
                    {
                        Name_login = _User.Name_login,
                        Password = model.PasswordOld,

                    };

                    if (_User.Password != PasswordEncryption(pass))
                    {
                        ViewBag.error = "Mật khẩu không chính xác";
                        return View();
                    }
                    if (model.PasswordNew != model.RePassword)
                    {
                        ViewBag.error = "Mật khẩu nhập lại không chính xác";
                        return View();
                    }
                    var pass2 = new LoginModel
                    {
                        Name_login = _User.Name_login,
                        Password = model.PasswordNew,

                    };
                    _User.Password = PasswordEncryption(pass2);
                    db.tblUsers.AddOrUpdate(_User);
                    db.SaveChanges();
                    return RedirectToAction("Profile");
                }
                return RedirectToAction("login");

            }
            else
            {
                ViewBag.error = "Thay đổi thất bại";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(tblUser model, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                var userid = Int32.Parse(Session["UserID"].ToString());
                var _User = db.tblUsers.SingleOrDefault(c => c.User_id == userid);
                if (_User != null)
                {
                    var check = db.tblUsers.FirstOrDefault(c => c.Email == model.Email);
                    if (check != null && check.User_id != userid)
                    {
                        ViewBag.error = "Email đã có người sử dụng";
                        return View();
                    }

                    if (IsEmail(model.Email) == false)
                    {
                        ViewBag.error = "Email không đúng định dạng vd: user@gmail.com";
                        return View();
                    }

                 
                    _User.Name = model.Name;
                    _User.Address = model.Address;

                    if (model.Email != null)
                    {
                        _User.Email = model.Email;
                    }

                    _User.Phone = model.Phone;
                    //_User.Slug = convertToUnSign2(user.FirstName + model.LastName);
                    db.tblUsers.AddOrUpdate(_User);
                    db.SaveChanges();
                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        int id = _User.User_id;

                        string _FileName = "";

                        int index = uploadhinh.FileName.IndexOf('.');

                        _FileName = "avatar" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/avatars"), _FileName);
                        uploadhinh.SaveAs(_path);
                        _User.avatar = _FileName;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Profile");
                }


            }
            ViewBag.error = "Thay đổi thất bại";
            return View();
        }








        [HttpGet]
        public ActionResult signUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult signUp(tblUser user, HttpPostedFileBase uploadhinh)
        {
            // kiem tra va luu vao db
            if (ModelState.IsValid)
            {
                var _User = db.tblUsers.SingleOrDefault(c => c.Name_login == user.Name_login);
                if (_User == null)
                {
                    var check = db.tblUsers.FirstOrDefault(c => c.Email == user.Email);
                    if (check != null)
                    {
                        ViewBag.error = "Email đã có người sử dụng";
                        return View();
                    }

                    if (IsEmail(user.Email) == false)
                    {
                        ViewBag.error = "Email không đúng định dạng vd: user@gmail.com";
                        return View();
                    }

                    var pass = new LoginModel
                    {
                        Name_login = user.Name_login,
                        Password = user.Password
                    };


                    var _user = new tblUser
                    {
                        Name_login = user.Name_login,
                        Password = PasswordEncryption(pass),
                       
                        Name = user.Name,
                        Email = user.Email,
                        Phone = user.Phone,
                        Date_signup = DateTime.UtcNow,
                        IsAdmin = false,
                        Address = user.Address,

                    };

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tblUsers.Add(_user);
                    db.SaveChanges();
                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        int id = _user.User_id;

                        string _FileName = "";

                        int index = uploadhinh.FileName.IndexOf('.');

                        _FileName = "avatar" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/avatars/"), _FileName);
                        uploadhinh.SaveAs(_path);
                        _user.avatar = _FileName;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Login");
                }



                else
                {
                    ViewBag.error = "Username đã có người sử dụng";
                    return View();
                }
            }

            ViewBag.error = "Đăng kí thất bại";
            return View();

        }



        [HttpGet]
        public ActionResult Login()
        {
            if(Session["UserID"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var pass = PasswordEncryption(user);
                var data = db.tblUsers.SingleOrDefault(s => s.Name_login.Equals(user.Name_login) && s.Password.Equals(pass));

                if (data != null)
                {
                    //add session

                    Session["FullName"] = data.Name;
                    Session["Email"] = data.Email;
                    Session["UserID"] = data.User_id;
                    if (data.IsAdmin == true)
                    {
                        Session["Admin"] = "Admin";
                    }
                    Session["Avatar"] = data.avatar;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu không chính xác";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "Đăng nhập thất bại, thử lại sau";
                return View();
            }

        }


        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store,must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Session.Abandon();

            Session.Clear();
            Response.Cookies.Clear();
            Session.RemoveAll();

            return RedirectToAction("Index", "Home");
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




        // ham`
        public static bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string strRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(strRegex);

            return regex.IsMatch(email);
        }
        public string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace(' ', '-');
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        //cài đăt time
        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1979, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }


        //mã hóa pass

        private string PasswordEncryption(LoginModel user)
        {
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] _user = System.Text.Encoding.ASCII.GetBytes(user.Password + user.Name_login);
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(user.Password + Encoding.UTF8.GetBytes("daylaconghoaxahoichunghiavietnam"));

            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            byte[] hash2 = mh.ComputeHash(_user);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
                sb.Append(hash2[i].ToString("X2"));
            }
            return (sb.ToString());
        }




    }

}
