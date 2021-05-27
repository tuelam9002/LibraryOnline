using LibraryOnline.Models.Business;
using LibraryOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Presentation;
using System.Web.Script.Serialization;
using LibraryOnline.Models.DTO;
using PagedList;

namespace LibraryOnline.Controllers
{
    public class DocumentController : Controller
    {
        private LibraryOnlineEntities db = new LibraryOnlineEntities();
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(string Metatitle, long ID)
        {
            var doc = db.Documents.Find(ID);
            //Tăng lượt xem
            new DocumentBusiness().IncreaseView(ID);
            ViewBag.Document = doc;
            ViewBag.Category = db.Categories.FirstOrDefault(x => x.ID == doc.Category_ID);

            //Lấy đánh giá
            ViewBag.Feedback = new DocumentBusiness().getFeedback(ID);
            return View();
        }

        public ActionResult GetPDF(long ID)
        {
            var doc = db.Documents.Find(ID);
            string filePath = "~/Assets/File/" + doc.Name;
            Response.AddHeader("Content-Disposition", "inline; filename=DataBindEvalFormat.pdf");

            return File(filePath, "application/pdf");
        }

        public ActionResult GetDoc(long ID)
        {
            var doc = db.Documents.Find(ID);
            //Create word document
            Spire.Doc.Document document = new Spire.Doc.Document();
            document.LoadFromFile(Server.MapPath("~/Assets/File/" + doc.Name));
            document.SaveToFile(Server.MapPath("~/Views/Document/GetDoc.pdf"), Spire.Doc.FileFormat.PDF);
            return File(Server.MapPath("~/Views/Document/GetDoc.pdf"), "application/pdf");
        }


        public ActionResult GetPowerPoint(long ID)
        {
            var doc = db.Documents.Find(ID);
            //Initialize an object of Presentation class
            Presentation ppt = new Presentation();
            ppt.LoadFromFile(Server.MapPath("~/Assets/File/" + doc.Name));
            //ppt.SaveToFile(Server.MapPath("~/Views/Document/GetPowerPoint.html"), Spire.Presentation.FileFormat.Html);
            ppt.SaveToFile(Server.MapPath("~/Views/Document/GetPowerPoint.pdf"), Spire.Presentation.FileFormat.PDF);

            //return new FilePathResult(Server.MapPath("~/Views/Document/GetPowerPoint.html"), "text/html");
            return File(Server.MapPath("~/Views/Document/GetPowerPoint.pdf"), "application/pdf");

        }


        public ActionResult Upload()
        {
            ViewBag.ListCategory = new DocumentBusiness().listCategory();
            ViewBag.ListObject = new DocumentBusiness().listObject();
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(Models.EF.Document entity, HttpPostedFileBase file)
        {
            try
            {
                var doc = new Models.EF.Document();
                doc.DatePost = DateTime.Now;
                doc.PostBy = entity.PostBy;
                doc.User_ID = entity.User_ID;
                doc.Category_ID = entity.Category_ID;
                doc.Class_ID = entity.Class_ID;
                doc.Point = 0;
                doc.Subject_ID = entity.Subject_ID;
                doc.DownRate = 0;
                doc.ShareRate = 0;
                doc.ViewRate = 0;
                doc.Status = false;

                
                var path = Path.Combine(Server.MapPath("~/Assets/File"), file.FileName);
                if (System.IO.File.Exists(path))
                {
                    string extensionName = Path.GetExtension(file.FileName);
                    string filename = file.FileName + DateTime.Now.ToString("ddMMyyyy") + extensionName;
                    path = Path.Combine(Server.MapPath("~/Assets/File"), filename);
                    file.SaveAs(path);
                    doc.Name = filename;
                    doc.Metatitle = Str_Metatitle(filename);
                    doc.ExtensionFile = extensionName.Trim();
                    doc.Size = file.ContentLength;
                }
                else
                {
                    file.SaveAs(path);
                    doc.Name = file.FileName;
                    doc.Metatitle = Str_Metatitle(file.FileName);
                    doc.ExtensionFile = Path.GetExtension(file.FileName);
                    doc.Size = file.ContentLength;
                }

                db.Documents.Add(doc);
                db.SaveChanges();
                TempData["add_success"] = "Đăng tài liệu thành công. Bạn vui lòng chờ quản trị viên xét duyệt tài liệu.";
                return RedirectToAction("Upload");
            }
            catch
            {
                TempData["add_success"] = "Đăng tài liệu KHÔNG thành công, tài liệu quá lớn.";
                return RedirectToAction("Upload");
            }
        }

        public JsonResult addFeedback(string json_feedback)
        {
            var JsonReview = new JavaScriptSerializer().Deserialize<List<FeedbackDTO>>(json_feedback);
            var review = new Feedback();
            foreach (var item in JsonReview)
            {
                review.Content = item.Content;
                review.Point = item.Point;
                review.CreatedDate = DateTime.Now;
                review.User_ID = item.User_ID;
                review.Document_ID = item.Document_ID;
                review.Status = true;
            }

            var res = new DocumentBusiness().addFeedback(review);
            if (res)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public ActionResult DownloadFile(long ID)
        {
            //tăng số lượt tải về
            new DocumentBusiness().IncreaseDown(ID);
            var doc = db.Documents.Find(ID);
            string fullName = Server.MapPath("~/Assets/File/" + doc.Name);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, doc.Name);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }


        public JsonResult ListName(string q)
        {
            var query = db.Documents.Where(x => x.Name.Contains(q)).Select(x => x.Name);
            //var query = from pro in db.Products
            //            where pro.Product_Name.Contains(q)
            //            select new
            //            {
            //                pro.Product_Name,
            //                pro.Image
            //            };
            return Json(new
            {
                data = query,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, int pagesize = 12)
        {
            string[] key = keyword.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var document_name = new List<LibraryOnline.Models.EF.Document>();//Tìm theo tên tài liệu
            foreach (var item in key)
            {
                document_name = (from b in db.Documents
                                where b.Name.Contains(item)
                                select b).ToList();
            }
            ViewBag.KeyWord = keyword;
            return View(document_name.ToPagedList(page, pagesize));
        }

        public JsonResult listSubjectByObject(long ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.Subjects.Where(x => x.Object_ID == ID).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listClassByObject(long ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.Classes.Where(x => x.Object_ID == ID).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        //Chuyển tên file thành metatitle
        public string Str_Metatitle(string str)
        {
            string[] VietNamChar = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ:/"
            };
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]).Replace("“", string.Empty).Replace("”", string.Empty);
                    str = str.Replace("\"", string.Empty).Replace("'", string.Empty).Replace("`", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty);
                    str = str.Replace(".", string.Empty).Replace(",", string.Empty).Replace(";", string.Empty).Replace(":", string.Empty);
                    str = str.Replace("?", string.Empty);
                }
            }
            string str1 = str.ToLower();
            string[] name = str1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string meta = null;
            //Thêm dấu '-'
            foreach (var item in name)
            {
                meta = meta + item + "-";
            }
            return meta;
        }
    }
}