using LibraryOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryOnline.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private LibraryOnlineEntities db = new LibraryOnlineEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            //thống kê số lượng tài liệu
            var library = from doc in db.Documents
                       join cate in db.Categories on doc.Category_ID equals cate.ID
                       where doc.Status == true
                       select new
                       {
                           doc.Category_ID,
                           doc.ID
                       };
            //Thống kê giáo án
            ViewBag.Count_GiaoAn = library.Where(x => x.Category_ID == 1).Count();

            //Thống kê bài giảng
            ViewBag.Count_BaiGiang = library.Where(x => x.Category_ID == 2).Count();

            //Thống kê đề thi và kiểm tra
            ViewBag.Count_DeThi = library.Where(x => x.Category_ID == 3).Count();


            //Thống kê tài liệu được điểm đánh giá cao
            ViewBag.point_document = db.Documents.Where(x => x.Point > 0).OrderByDescending(x => x.Point).Take(10).ToList();

            //Thống kê tài liệu có lượt xem cao
            ViewBag.view_document = db.Documents.Where(x => x.ViewRate > 0).OrderByDescending(x => x.ViewRate).Take(10).ToList();

            //Thống kê đánh giá hôm nay
            ViewBag.Review_today = db.Feedbacks.Where(x => DbFunctions.TruncateTime(x.CreatedDate) == DbFunctions.TruncateTime(DateTime.Now)).Count();

            //Thống kê upload file hôm nay
            ViewBag.upload_today = db.Documents.Where(x => DbFunctions.TruncateTime(x.DatePost) == DbFunctions.TruncateTime(DateTime.Now)).Count();

            //Thống kê user đã đăng ký
            ViewBag.user = db.Users.Count();

            //Thống kê tài liệu chưa được duyệt
            ViewBag.document_nottest = db.Documents.Where(x => x.Status == false).Count();

            return View();
        }
    }
}