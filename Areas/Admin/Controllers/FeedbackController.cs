using LibraryOnline.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryOnline.Areas.Admin.Controllers
{
    public class FeedbackController : Controller
    {
        private LibraryOnlineEntities db = new LibraryOnlineEntities();
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = db.Feedbacks.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
        }

        //Xóa đánh giá
        public JsonResult Delete(long ID)
        {
            var fed = db.Feedbacks.Find(ID);
            db.Feedbacks.Remove(fed);
            db.SaveChanges();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult changeStatus(long ID)
        {
            var fed = db.Feedbacks.Find(ID);
            fed.Status = false;
            db.SaveChanges();
            return Json(new
            {
                status = true
            });
        }
    }
}