using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models.DTO
{
    public class SubjectDTO
    {
        public long Subject_ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CountLesson { get; set; }
        public Nullable<int> CountExam { get; set; }
        public Nullable<int> SumPlan { get; set; }
        public Nullable<long> Object_ID { get; set; }
        public Nullable<bool> Status { get; set; }

        public long Category_ID { get; set; }
        public Nullable<int> Real_Lesson { get; set; }
        public Nullable<int> Real_Exam { get; set; }
        public Nullable<int> Real_Plan { get; set; }
    }
}