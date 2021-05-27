using LibraryOnline.Models.DTO;
using LibraryOnline.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryOnline.Controllers
{
    public class HomeController : Controller
    {
        private LibraryOnlineEntities db = new LibraryOnlineEntities();

        public ActionResult Index()
        {
            ViewBag.ListAllDocument = db.Documents.Where(x => x.Status == true).OrderByDescending(x => x.DatePost).ToList();
            var query = from obj in db.Objects
                                  join sub in db.Subjects on obj.ID equals sub.Object_ID
                                  join doc in db.Documents on sub.ID equals doc.Subject_ID
                                  where doc.Status == true
                                  select new DocumentDTO()
                                  {
                                      ID = doc.ID,
                                      Name = doc.Name,
                                      Metatitle = doc.Metatitle,
                                      DatePost = doc.DatePost,
                                      PostBy = doc.PostBy,
                                      Point = doc.Point,
                                      ExtensionFile = doc.ExtensionFile,
                                      Object_ID = sub.Object_ID,
                                      ClassName = doc.Class.Name,
                                      Class_ID = doc.Class_ID
                                  };
            ViewBag.ListDocument = query.OrderByDescending(x => x.DatePost).ToList();
            ViewBag.ListObject = db.Objects.ToList();
            ViewBag.ListClass = db.Classes.ToList();
            
            
            //ViewBag.ListTieuHoc = query.Where(x => x.Object_ID == 1).OrderByDescending(x => x.DatePost).ToList();
            //ViewBag.ListTHCS = query.Where(x => x.Object_ID == 2).OrderByDescending(x => x.DatePost).ToList();
            //ViewBag.ListTHPT = query.Where(x => x.Object_ID == 3).OrderByDescending(x => x.DatePost).ToList();

            //ViewBag.ListClass_TH = db.Classes.Where(x => x.Object_ID == 1).ToList();
            //ViewBag.ListClass_THCS = db.Classes.Where(x => x.Object_ID == 2).ToList();
            //ViewBag.ListClass_THPT = db.Classes.Where(x => x.Object_ID == 3).ToList();
            return View();
        }

        public ActionResult GetDocByCate(string link, long ID, int page = 1, int pagesize = 12)
        {
            var model = from obj in db.Objects
                        join sub in db.Subjects on obj.ID equals sub.Object_ID
                        join doc in db.Documents on sub.ID equals doc.Subject_ID
                        where doc.Status == true && obj.ID == ID
                        select new DocumentDTO()
                        {
                            ID = doc.ID,
                            Name = doc.Name,
                            Metatitle = doc.Metatitle,
                            DatePost = doc.DatePost,
                            PostBy = doc.PostBy,
                            Point = doc.Point,
                            ExtensionFile = doc.ExtensionFile,
                            Object_ID = sub.Object_ID,
                            ClassName = doc.Class.Name,
                            Class_ID = doc.Class_ID
                        };
                
            ViewBag.Object = db.Objects.Find(ID);
            return View(model.OrderByDescending(x => x.DatePost).ToPagedList(page, pagesize));
        }
    }
}