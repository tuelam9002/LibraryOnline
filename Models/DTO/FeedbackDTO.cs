using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models.DTO
{
    public class FeedbackDTO
    {
        public long ID { get; set; }
        public string Content { get; set; }
        public Nullable<int> Point { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> User_ID { get; set; }
        public Nullable<long> Document_ID { get; set; }
        public Nullable<bool> Status { get; set; }

        public string User_Name { get; set; }

    }
}