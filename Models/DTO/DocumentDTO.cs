using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models.DTO
{
    public class DocumentDTO
    {

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
        public string Category_Name { get; set; }
        public string Subject_Name { get; set; }
        public Nullable<long> Object_ID { get; set; }

        public Nullable<long> Class_ID { get; set; }
        public string ClassName { get; set; }

    }
}