using LibraryOnline.Models.DTO;
using LibraryOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryOnline.Areas.Admin.Controllers
{
    public class SubjectController : Controller
    {
        private LibraryOnlineEntities db = new LibraryOnlineEntities();
        // GET: Admin/Subject
        public ActionResult Index()
        {
            ViewBag.TieuHoc = db.Subjects.Where(x => x.Object_ID == 1).ToList();
            ViewBag.THCS = db.Subjects.Where(x => x.Object_ID == 2).ToList();
            ViewBag.THPT = db.Subjects.Where(x => x.Object_ID == 3).ToList();
            return View();
        }

        public ActionResult Primary()
        {
            var subject = db.Subjects.Where(x=> x.Object_ID == 1).ToList();
            var document = from doc in db.Documents
                           join cate in db.Categories on doc.Category_ID equals cate.ID
                           join sub in db.Subjects on doc.Subject_ID equals sub.ID
                           where sub.Object_ID == 1
                           select new SubjectDTO()
                           {
                               Subject_ID = sub.ID,
                               Category_ID = cate.ID
                           };
           
            var lstSubDTO = new List<SubjectDTO>();
            foreach(var item in subject)
            {
                var subDTO = new SubjectDTO();
                var real_Lesson = 0;
                var real_Exam = 0;
                var real_Plan = 0;
                foreach(var jtem in document)
                {
                    if(jtem.Subject_ID == item.ID)
                    {
                        if (jtem.Category_ID == 1)
                            real_Lesson += 1;
                        else if (jtem.Category_ID == 2)
                            real_Plan += 1;
                        else if (jtem.Category_ID == 3)
                            real_Exam += 1;
                    }
                }
                subDTO.Subject_ID = item.ID;
                subDTO.Name = item.Name;
                subDTO.CountLesson = item.CountLesson;
                subDTO.SumPlan = item.SumPlan;
                subDTO.CountExam = item.CountExam;
                subDTO.Real_Lesson = real_Lesson;
                subDTO.Real_Plan = real_Plan;
                subDTO.Real_Exam = real_Exam;
                lstSubDTO.Add(subDTO);
            }

            ViewBag.lstSubDTO = lstSubDTO;
            return View();
        }

        public ActionResult Secondary()
        {
            var subject = db.Subjects.Where(x => x.Object_ID == 2).ToList();
            var document = from doc in db.Documents
                           join cate in db.Categories on doc.Category_ID equals cate.ID
                           join sub in db.Subjects on doc.Subject_ID equals sub.ID
                           where sub.Object_ID == 2
                           select new SubjectDTO()
                           {
                               Subject_ID = sub.ID,
                               Category_ID = cate.ID
                           };

            var lstSubDTO = new List<SubjectDTO>();
            foreach (var item in subject)
            {
                var subDTO = new SubjectDTO();
                var real_Lesson = 0;
                var real_Exam = 0;
                var real_Plan = 0;
                foreach (var jtem in document)
                {
                    if (jtem.Subject_ID == item.ID)
                    {
                        if (jtem.Category_ID == 1)
                            real_Lesson += 1;
                        else if (jtem.Category_ID == 2)
                            real_Plan += 1;
                        else if (jtem.Category_ID == 3)
                            real_Exam += 1;
                    }
                }
                subDTO.Subject_ID = item.ID;
                subDTO.Name = item.Name;
                subDTO.CountLesson = item.CountLesson;
                subDTO.SumPlan = item.SumPlan;
                subDTO.CountExam = item.CountExam;
                subDTO.Real_Lesson = real_Lesson;
                subDTO.Real_Plan = real_Plan;
                subDTO.Real_Exam = real_Exam;
                lstSubDTO.Add(subDTO);
            }

            ViewBag.lstSubDTO = lstSubDTO;
            return View();
        }


        public ActionResult HighSchool()
        {
            var subject = db.Subjects.Where(x => x.Object_ID == 3).ToList();
            var document = from doc in db.Documents
                           join cate in db.Categories on doc.Category_ID equals cate.ID
                           join sub in db.Subjects on doc.Subject_ID equals sub.ID
                           where sub.Object_ID == 3
                           select new SubjectDTO()
                           {
                               Subject_ID = sub.ID,
                               Category_ID = cate.ID
                           };

            var lstSubDTO = new List<SubjectDTO>();
            foreach (var item in subject)
            {
                var subDTO = new SubjectDTO();
                var real_Lesson = 0;
                var real_Exam = 0;
                var real_Plan = 0;
                foreach (var jtem in document)
                {
                    if (jtem.Subject_ID == item.ID)
                    {
                        if (jtem.Category_ID == 1)
                            real_Lesson += 1;
                        else if (jtem.Category_ID == 2)
                            real_Plan += 1;
                        else if (jtem.Category_ID == 3)
                            real_Exam += 1;
                    }
                }
                subDTO.Subject_ID = item.ID;
                subDTO.Name = item.Name;
                subDTO.CountLesson = item.CountLesson;
                subDTO.SumPlan = item.SumPlan;
                subDTO.CountExam = item.CountExam;
                subDTO.Real_Lesson = real_Lesson;
                subDTO.Real_Plan = real_Plan;
                subDTO.Real_Exam = real_Exam;
                lstSubDTO.Add(subDTO);
            }

            ViewBag.lstSubDTO = lstSubDTO;
            return View();
        }

        public ActionResult SubjectDetail(long ID)
        {
            var query = db.Documents.Where(x => x.Subject_ID == ID).ToList();
            ViewBag.BaiGiang = query.Where(x => x.Category.ID == 1).OrderByDescending(x => x.DatePost).ToList();
            ViewBag.GiaoAn = query.Where(x => x.Category.ID == 2).OrderByDescending(x => x.DatePost).ToList();
            ViewBag.DeThi = query.Where(x => x.Category.ID == 3).OrderByDescending(x => x.DatePost).ToList();

            ViewBag.Subject = db.Subjects.Find(ID);
            return View();
        }

        public JsonResult GetSubjectByID(long ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var sub = db.Subjects.Find(ID);
            return Json(sub, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult editSubject(Subject entity)
        {
            try
            {
                var sub = db.Subjects.Find(entity.ID);
                sub.CountLesson = entity.CountLesson;
                sub.SumPlan = entity.SumPlan;
                sub.CountExam = entity.CountExam;
                db.SaveChanges();
                TempData["add_success"] = "Sửa yêu cầu tiến độ môn học thành công";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["add_success"] = "Sửa yêu cầu tiến độ môn học KHÔNG thành công";
                return RedirectToAction("Index");
            }
        }
    }
}