using LibraryOnline.Models.DTO;
using LibraryOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models.Business
{
    public class DocumentBusiness
    {
        private LibraryOnlineEntities db = new LibraryOnlineEntities();

        //lấy danh mục tài liệu
        public List<Category> listCategory()
        {
            return db.Categories.ToList();
        }

        //lấy danh mục đối tượng
        public List<Models.EF.Object> listObject()
        {
            return db.Objects.ToList();
        }

        //Tăng lượt xem tài liệu
        public void IncreaseView(long ID)
        {
            var doc = db.Documents.Find(ID);
            doc.ViewRate += 1;
            db.SaveChanges();
        }

        //Tăng lượt download tài liệu
        public void IncreaseDown(long ID)
        {
            var doc = db.Documents.Find(ID);
            doc.DownRate += 1;
            db.SaveChanges();
        }

        //Cộng điểm tài liệu
        public void AddPoint(long ID, float point)
        {
            var doc = db.Documents.Find(ID);
            doc.Point += point;
            db.SaveChanges();
        }


        //thêm đánh giá tài liệu
        public bool addFeedback(Feedback entity)
        {
            try
            {
                db.Feedbacks.Add(entity);
                AddPoint((long)entity.Document_ID, (float)entity.Point);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //lấy đánh giá tài liệu
        public List<FeedbackDTO> getFeedback(long document_id)
        {
            var query = from fed in db.Feedbacks
                        join doc in db.Documents on fed.Document_ID equals doc.ID
                        join user in db.Users on fed.User_ID equals user.ID
                        where fed.Document_ID == document_id && fed.Status == true
                        select new FeedbackDTO()
                        {
                            ID = fed.ID,
                            Content = fed.Content,
                            Point = fed.Point,
                            User_Name = user.Fullname,
                            CreatedDate = fed.CreatedDate
                        };
            return query.OrderByDescending(x => x.CreatedDate).ToList();
        }

    }
}