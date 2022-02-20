using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;

namespace E_Commerce_shop.Controllers
{
    public class AdminController : Controller
    {
        OrderRepossitory Order = new OrderRepossitory();
      
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Memmberid"] == null)
            {
                return RedirectToRoute("MyRoute");
            }
            else
            {


                return View();
            }
        }
        public ActionResult OrdelDetail(int id)
        {
            if (Session["Memmberid"] == null)
            {
                return RedirectToRoute("MyRoute");
            }
            else
            {
                ViewBag.OrderId = id;
                return View();
            }

        }
        public JsonResult Getorder(GetOrderVviewModel order)
        {
            IEnumerable<GetOrderVviewModel> orders = Order.order();
            return Json(orders, JsonRequestBehavior.AllowGet);

        }
        public JsonResult OrderDetails(int id)
        {
            IEnumerable<Card_Item> OrderDetail = Order.OrderDetail(id);
            return Json(OrderDetail, JsonRequestBehavior.AllowGet);
        }
        ShippingDetailsRepository ship = new ShippingDetailsRepository();
        public JsonResult customerDetail(int id)
        {
            var customerDetail = ship.ship(id);
            return Json(customerDetail, JsonRequestBehavior.AllowGet);
        }

        public JsonResult payedformonth()
        {
            IEnumerable<GetOrderVviewModel> payedformonth = Order.payedformonth();
            return Json(payedformonth, JsonRequestBehavior.AllowGet);
        }
        //customer page
        public ActionResult customerpage()
        {
            if (Session["Memmberid"] == null)
            {
                return RedirectToRoute("MyRoute");
            }
            else
            {
                return View();
            }

        }
        MembreRepository Member = new MembreRepository();
        public JsonResult Dacboord()
        {
            var array = Member.CountCustomer();
            return Json(array, JsonRequestBehavior.AllowGet);
        }
        //slider page
        siliderRepository slider = new siliderRepository();
        public JsonResult sliders(HttpPostedFileBase image, string text)
        {
            string name = "";
            if (image != null && image.ContentLength > 0)
            {
                string a = DateTime.Now.ToString("MMddyyHHmmss");
                name = a + Path.GetFileName(image.FileName);

                //string folder = Server.MapPath("~/App_Data/project image");
                string folder = Server.MapPath("~/image/slider image");
                string path = Path.Combine(folder, name);

                image.SaveAs(path);

                //System.IO.File.Delete(path);



            }
            bool x = slider.Add(name, text);

            return Json(x, JsonRequestBehavior.AllowGet);
        }
        //Get All SliderData
        public JsonResult GetAllSliderData()
        {
            IEnumerable<SliderViewModel> sliders = slider.GetAllGata();
            return Json(sliders.ToList(), JsonRequestBehavior.AllowGet);
        }
        //delet slider
        public JsonResult Deleteslider(int id)
        {
            string Delete = slider.Delete(id);
            if (Delete != "")
            {
                string folder = Server.MapPath("~/image/slider image");
                string path = Path.Combine(folder, Delete);
                System.IO.File.Delete(path);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }


        }
        //Update slider
        public JsonResult Updateslider(int id)
        {
            IEnumerable<SliderViewModel> slide = slider.Update(id);
            return Json(slide.ToList(), JsonRequestBehavior.AllowGet);


        }
        public JsonResult Updatesliders(SliderViewModel sliders)
        {
            string name = "";
            if (sliders.SlideImag != null && sliders.SlideImag.ContentLength > 0)
            {
                string a = DateTime.Now.ToString("MMddyyHHmmss");
                name = a + Path.GetFileName(sliders.SlideImag.FileName);

                //string folder = Server.MapPath("~/App_Data/project image");
                string folder = Server.MapPath("~/image/slider image");
                string path = Path.Combine(folder, name);

                sliders.SlideImag.SaveAs(path);

                //System.IO.File.Delete(path);

                sliders.SlideImage = name;

            }
            string Update = slider.Update(sliders);
            if (Update != "")
            {
                if (Update != "0")
                {
                    string folder = Server.MapPath("~/image/slider image");
                    string path = Path.Combine(folder, Update);
                    System.IO.File.Delete(path);
                }

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
    }
}