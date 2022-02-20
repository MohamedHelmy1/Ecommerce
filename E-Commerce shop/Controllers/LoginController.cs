using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using BLL;

namespace E_Commerce_shop.Controllers
{
    public class LoginController : Controller
    {
        //Repository
        MembreRepository member = new MembreRepository();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        //Register

        public JsonResult Register(MemberViewModel Member)
        {

            Member.Password = helper.MD5Password.GetMd5Hash(Member.Password);
            IEnumerable<MemberViewModel> result = member.Register(Member);
            if (result == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {

                foreach (var item in result)
                {
                    Session["Memmberid"] = item.MemberId;


                }
                return Json("success", JsonRequestBehavior.AllowGet);

            }


        }
        public JsonResult LoginMember(MemberViewModel Member)
        {
            Member.Password = helper.MD5Password.GetMd5Hash(Member.Password);
            IEnumerable<MemberViewModel> result = member.login(Member);

            if (result == null)
            {
                var role = "";
                return Json(role, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var role = "";
                foreach (var item in result)
                {
                    Session["Memmberid"] = item.MemberId;

                    role = item.Roleid.ToString();
                }
                return Json(role, JsonRequestBehavior.AllowGet);

            }

        }
    }
}