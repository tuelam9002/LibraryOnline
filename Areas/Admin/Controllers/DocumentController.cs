using LibraryOnline.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryOnline.Areas.Admin.Controllers
{
    public class DocumentController : Controller
    {
        private LibraryOnlineEntities db = new LibraryOnlineEntities();
        // GET: Admin/Document
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            return View(db.Documents.Where(x => x.Status == true).OrderByDescending(x => x.DatePost).ToPagedList(page, pageSize));
        }

        public ActionResult DocumentNotTest(int page = 1, int pageSize = 10)
        {
            return View(db.Documents.Where(x => x.Status == false).OrderByDescending(x => x.DatePost).ToPagedList(page, pageSize));
        }

        public JsonResult Delete(long ID)
        {
            try
            {
                var doc = db.Documents.Find(ID);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Assets/File"), doc.Name));

                db.Documents.Remove(doc);
                db.SaveChanges();
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }

        }

        //Duyệt tài liệu
        public ActionResult TestDocument(long ID)
        {
            try
            {
                var doc = db.Documents.Find(ID);
                doc.Status = true;
                db.SaveChanges();
                TempData["add_success"] = "Duyệt tài liệu thành công.";
                return RedirectToAction("DocumentNotTest");
            }
            catch
            {
                TempData["add_success"] = "Duyệt tài liệu KHÔNG thành công.";
                return RedirectToAction("DocumentNotTest");
            }
        }
    }
}