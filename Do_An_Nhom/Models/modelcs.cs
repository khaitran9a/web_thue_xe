using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_An_Nhom.Models
{
    public class EditPasswordModel
    {
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string RePassword { get; set; }
    }
}