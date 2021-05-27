using LibraryOnline.Models.Business;
using LibraryOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult frmLogin(User model)
        {
            var res = new UserBusiness().checkLogin(model);
            if (res)
            {
                Session["user"] = new UserBusiness().findUser(model);
                return Redirect("/");
            }
            else
            {
                TempData["error"] = "Tài khoản hoặc mật khẩu không chính xác.";
                return RedirectToAction("Login");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult frmRegister(User entity)
        {
            var res = new UserBusiness().register(entity);
            if (res)
            {
                TempData["error"] = "Bạn vui lòng đăng nhập để sử dụng dịch vụ của Enzuca.";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["error"] = "Đã có lỗi xảy ra, vui lòng thử lại sau.";
                return RedirectToAction("Register");
            }
        }


        public ActionResult logout()
        {
            Session["user"] = null;
            return Redirect("/");
        }
    }
}