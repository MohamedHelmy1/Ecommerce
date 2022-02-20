using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;

namespace E_Commerce_shop.Controllers
{
    public class SalesController : Controller
    {
        ShippingDetailsRepository memberDetail = new ShippingDetailsRepository();
        OrderRepossitory order = new OrderRepossitory();
        // GET: Sales
        public ActionResult ShopDetail()
        {
            if (Session["Cart"] == null)
            {
                return Redirect("/Home/Home");
            }
            return View();
        }

        public JsonResult SaveOrder(string id, int Quantity)
        {
            if (Session["Memmberid"] == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }


            List<Card_Item> cart = (List<Card_Item>)Session["Cart"];
            foreach (var item in cart)
            {

                if (item.prodect.ProductId == int.Parse(id))
                {
                    item.Quantity = Quantity;

                }






            }


            return Json("1", JsonRequestBehavior.AllowGet);
        }
        public JsonResult MemberDetail(ShippingDetailsViewModel Detail)
        {
            int Memmberid = int.Parse(Session["Memmberid"].ToString());
            List<Card_Item> cart = (List<Card_Item>)Session["Cart"];
            int m = order.Add(Memmberid, cart);
            Detail.MemberId = int.Parse(Session["Memmberid"].ToString());
            Detail.OrderId = m;
            bool A = memberDetail.Add(Detail);
            List<Card_Item> s = new List<Card_Item>();
            Session["Cart"] = s;
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}