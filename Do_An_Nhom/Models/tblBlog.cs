//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Do_An_Nhom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblBlog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBlog()
        {
            this.tblBlogComments = new HashSet<tblBlogComment>();
        }
    
        public int BlogId { get; set; }
        public Nullable<int> User_id { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> CreatDate { get; set; }
        public string SubTitle { get; set; }
        public string Text1 { get; set; }
        public string Image1 { get; set; }
        public string Alias { get; set; }
    
        public virtual tblUser tblUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBlogComment> tblBlogComments { get; set; }
    }
}
