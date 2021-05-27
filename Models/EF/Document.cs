//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryOnline.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            this.Feedbacks = new HashSet<Feedback>();
        }
    
        public long ID { get; set; }
        public string Name { get; set; }
        public string Metatitle { get; set; }
        public Nullable<System.DateTime> DatePost { get; set; }
        public string PostBy { get; set; }
        public Nullable<long> User_ID { get; set; }
        public Nullable<long> Category_ID { get; set; }
        public Nullable<double> Point { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<double> DownRate { get; set; }
        public Nullable<double> ShareRate { get; set; }
        public Nullable<double> ViewRate { get; set; }
        public Nullable<long> Subject_ID { get; set; }
        public Nullable<bool> Status { get; set; }
        public string ExtensionFile { get; set; }
        public Nullable<long> Class_ID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual Class Class { get; set; }
    }
}
