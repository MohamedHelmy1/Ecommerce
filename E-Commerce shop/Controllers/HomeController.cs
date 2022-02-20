using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using E_Commerce_shop.helper;

namespace E_Commerce_shop.Controllers
{
    public class HomeController : Controller
    {
        //-----------------Home sale Prodect-------------------------

        public ActionResult Home()
        {
            return View();
        }
        //AllProdect Home page
        public JsonResult AllProdect(string search, int pageNumber = 1, int pageSize = 4)
        {
            if (search == null || search == "")
            {

                IEnumerable<ProdectViewModel> prodect = prodet.GetAll();
                var pagedData = Pagination.PagedResult(prodect, pageNumber, pageSize);
                return Json(pagedData, JsonRequestBehavior.AllowGet);
                //return Json(prodect.ToList()/*.ToPagedList(page ?? 1, 3)*/, JsonRequestBehavior.AllowGet);
            }
            else
            {
                IEnumerable<ProdectViewModel> prodect = prodet.Searchproduct(search);
                var pagedData = Pagination.PagedResult(prodect, pageNumber, pageSize);
                return Json(pagedData, JsonRequestBehavior.AllowGet);
                //return Json(prodect.ToList(), JsonRequestBehavior.AllowGet);
            }



        }
        public JsonResult AllProdects(int catogoryid, string search, int pageNumber = 1, int pageSize = 15)
        {
            if (search == null || search == "")
            {

                IEnumerable<ProdectViewModel> prodect = prodet.GetAll(catogoryid);
                var pagedData = Pagination.PagedResult(prodect, pageNumber, pageSize);
                return Json(pagedData, JsonRequestBehavior.AllowGet);
                //return Json(prodect.ToList()/*.ToPagedList(page ?? 1, 3)*/, JsonRequestBehavior.AllowGet);
            }
            else
            {
                IEnumerable<ProdectViewModel> prodect = prodet.Searchproduct(catogoryid, search);
                var pagedData = Pagination.PagedResult(prodect, pageNumber, pageSize);
                return Json(pagedData, JsonRequestBehavior.AllowGet);
                //return Json(prodect.ToList(), JsonRequestBehavior.AllowGet);
            }



        }

        //Add product in card

        public JsonResult AddCard(int productid)
        {
            ProdectViewModel prodec = prodet.GetById(productid);
            if (Session["Cart"] == null)
            {
                List<Card_Item> cart = new List<Card_Item>();
                cart.Add(new Card_Item()
                {
                    prodect = prodec,
                    Quantity = 1

                });
                Session["Cart"] = cart;

                return Json(cart.ToList(), JsonRequestBehavior.AllowGet);

            }
            else
            {
                List<Card_Item> cart = (List<Card_Item>)Session["Cart"];
                foreach (var item in cart)
                {
                    if (item.prodect.ProductId == productid)
                    {
                        return Json(1, JsonRequestBehavior.AllowGet);

                    }



                }

                cart.Add(new Card_Item()
                {
                    prodect = prodec,
                    Quantity = 1

                });
                Session["Cart"] = cart;

                return Json(cart.ToList(), JsonRequestBehavior.AllowGet);


            }





        }
        //Add in the card page
        public ActionResult productcardpage()
        {
            return View();
        }
        public JsonResult cardpage()
        {
            if (Session["Cart"] != null)
            {
                List<Card_Item> cart = (List<Card_Item>)Session["Cart"];
                return Json(cart.ToList(), JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(1, JsonRequestBehavior.AllowGet);

            }


        }
        //remove from card
        public JsonResult removeproduct(int productid)
        {
            List<Card_Item> cart = (List<Card_Item>)Session["Cart"];

            foreach (var item in cart)
            {
                if (item.prodect.ProductId == productid)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["Cart"] = cart;


            return Json(cart.ToList(), JsonRequestBehavior.AllowGet);
        }

        //----------------- End Home sale Prodect-------------------------
        //-----------------Admin page-------------------------
        public ActionResult Admin()
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
        //------------------Catogerey Page-----------------------------
        public ActionResult Catogrey()
        {
            return View();
        }
        // -------------Catogrey Repository---------
        Catodorey_Repository category = new Catodorey_Repository();
        //--------------Get All Catogrey------------
        public JsonResult AllCatogrey()
        {
            IEnumerable<CatogoreyViewModel> Cat = category.GetAll();
            return Json(Cat.ToList(), JsonRequestBehavior.AllowGet);
        }
        //Add Catogorey
        public JsonResult AddCatogrey(CatogoreyViewModel catogoery)
        {
            bool Add = category.Add(catogoery);
            if (Add == true)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
        //Update Catogery
        public JsonResult UpdateCatogreys(int id)
        {
            IEnumerable<CatogoreyViewModel> Cat = category.Update(id);
            return Json(Cat.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCatogrey(CatogoreyViewModel catogoery)
        {
            bool Update = category.Update(catogoery);
            if (Update == true)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }

        //Deleat Catogrey
        public JsonResult DeleteCatogrey(int id)
        {
            bool Delete = category.Delete(id);
            if (Delete == true)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }

        //------------------End Catogerey Page-----------------------------
        //------------------------Start Product----------------------------
        //prodect Repository

        ProdectRepository prodet = new ProdectRepository();
        //Prodect page

        public ActionResult Prodects()
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
        //AddProject
        public JsonResult AddProdect(ProdectViewModel prodect)
        {
            var file = prodect.ProdectImage;
            if (file != null && file.ContentLength > 0)
            {
                string name = Path.GetFileName(file.FileName);
                prodect.ProductImage = name;
                //string folder = Server.MapPath("~/App_Data/project image");
                string folder = Server.MapPath("~/image/product image");
                string path = Path.Combine(folder, name);
                file.SaveAs(path);

            }
            bool result = prodet.Add(prodect);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //Start Uplood Prodect image

        //End  Uplood Prodect image
        //update project
        public ActionResult updateproject(int id)
        {
            if (Session["Memmberid"] == null)
            {
                return RedirectToRoute("MyRoute");
            }
            else
            {
                ViewBag.prodect = id;
                return View();
            }

        }
        public JsonResult Updateprodects(int id)
        {
            ProdectViewModel prodect = prodet.GetById(id);
            return Json(prodect, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateaProdect(ProdectViewModel prodect)
        {
            var file = prodect.ProdectImage;
            if (file != null && file.ContentLength > 0)
            {
                string name = Path.GetFileName(file.FileName);
                prodect.ProductImage = name;
                //string folder = Server.MapPath("~/App_Data/project image");
                string folder = Server.MapPath("~/image/product image");

                string path = Path.Combine(folder, name);
                file.SaveAs(path);

            }
            bool result = prodet.Update(prodect);

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //Delete Product
        public JsonResult DeleteProduct(int id)
        {
            var result = prodet.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //***********************************************//
        //product page
        public ActionResult product()
        {
            return View();
        }
    }
}