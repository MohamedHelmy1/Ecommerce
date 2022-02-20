using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModel;

namespace DAL
{
    interface IProdectRepository
    {
        bool Add(ProdectViewModel prodect);
        bool Update(ProdectViewModel prodect);
        bool Delete(int id);
        IEnumerable<ProdectViewModel> GetAll();
        ProdectViewModel GetById(int id);

    }
    public class ProdectRepository : IProdectRepository
    {
        dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();

        public bool Add(ProdectViewModel prodect)
        {
            try
            {
                Tbl_Product obj = new Tbl_Product();
                obj.CreatedDate = DateTime.Now;
                obj.Description = prodect.Description;
                obj.IsActive = true;
                obj.IsDelete = false;
                obj.IsFeatured = prodect.IsFeatured;
                //obj.ModifiedDate = null;
                obj.price = prodect.price;
                obj.ProductImage = prodect.ProductImage;
                obj.ProductName = prodect.ProductName;
                obj.Quantity = prodect.Quantity;
                obj.CategoryId = prodect.CategoryId;
                db.Tbl_Product.Add(obj);
                db.SaveChanges();
                return true;


            }
            catch (Exception)
            {

                return false;
            }


        }

        public bool Delete(int id)
        {
            try
            {
                var delete = db.Tbl_Product.Where(x => x.ProductId == id).FirstOrDefault();
                delete.IsDelete = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        public IEnumerable<ProdectViewModel> GetAll()

        {
            List<ProdectViewModel> prodect = new List<ProdectViewModel>();

            foreach (var item in db.Tbl_Product.Where(x => x.IsDelete == false))
            {
                ProdectViewModel obj = new ProdectViewModel();
                obj.ProductId = item.ProductId;
                obj.CreatedDate = item.CreatedDate;
                obj.Description = item.Description;
                obj.IsActive = item.IsActive;
                obj.IsDelete = item.IsDelete;
                var IsFeatured = item.IsFeatured;
                if (IsFeatured == true)
                {
                    obj.IsFeature = "New";
                }
                else
                {
                    obj.IsFeature = "Old";
                }
                obj.ModifiedDate = item.ModifiedDate;
                obj.price = item.price;
                obj.ProductImage = item.ProductImage;
                obj.ProductName = item.ProductName;
                obj.Quantity = item.Quantity;

                prodect.Add(obj);

            }




            return prodect;

        }
        public IEnumerable<ProdectViewModel> GetAll(int id)

        {
            List<ProdectViewModel> prodect = new List<ProdectViewModel>();

            foreach (var item in db.Tbl_Product.Where(x => x.IsDelete == false && x.CategoryId == id))
            {
                ProdectViewModel obj = new ProdectViewModel();
                obj.ProductId = item.ProductId;
                obj.CreatedDate = item.CreatedDate;
                obj.Description = item.Description;
                obj.IsActive = item.IsActive;
                obj.IsDelete = item.IsDelete;
                var IsFeatured = item.IsFeatured;
                if (IsFeatured == true)
                {
                    obj.IsFeature = "New";
                }
                else
                {
                    obj.IsFeature = "Old";
                }
                obj.ModifiedDate = item.ModifiedDate;
                obj.price = item.price;
                obj.ProductImage = item.ProductImage;
                obj.ProductName = item.ProductName;
                obj.Quantity = item.Quantity;

                prodect.Add(obj);

            }




            return prodect;

        }
        //Search
        public IEnumerable<ProdectViewModel> Searchproduct(int id, string search)

        {
            List<ProdectViewModel> prodect = new List<ProdectViewModel>();

            //Search
            foreach (var item in db.Tbl_Product.Where(x => x.IsDelete == false && x.CategoryId == id && x.ProductName.Contains(search)))
            {
                ProdectViewModel obj = new ProdectViewModel();
                obj.ProductId = item.ProductId;
                obj.CreatedDate = item.CreatedDate;
                obj.Description = item.Description;
                obj.IsActive = item.IsActive;
                obj.IsDelete = item.IsDelete;
                var IsFeatured = item.IsFeatured;
                if (IsFeatured == true)
                {
                    obj.IsFeature = "New";
                }
                else
                {
                    obj.IsFeature = "Old";
                }
                obj.ModifiedDate = item.ModifiedDate;
                obj.price = item.price;
                obj.ProductImage = item.ProductImage;
                obj.ProductName = item.ProductName;
                obj.Quantity = item.Quantity;

                prodect.Add(obj);

            }



            return prodect;

        }
        public IEnumerable<ProdectViewModel> Searchproduct(string search)

        {
            List<ProdectViewModel> prodect = new List<ProdectViewModel>();

            //Search
            foreach (var item in db.Tbl_Product.Where(x => x.IsDelete == false && x.ProductName.Contains(search)))
            {
                ProdectViewModel obj = new ProdectViewModel();
                obj.ProductId = item.ProductId;
                obj.CreatedDate = item.CreatedDate;
                obj.Description = item.Description;
                obj.IsActive = item.IsActive;
                obj.IsDelete = item.IsDelete;
                var IsFeatured = item.IsFeatured;
                if (IsFeatured == true)
                {
                    obj.IsFeature = "New";
                }
                else
                {
                    obj.IsFeature = "Old";
                }
                obj.ModifiedDate = item.ModifiedDate;
                obj.price = item.price;
                obj.ProductImage = item.ProductImage;
                obj.ProductName = item.ProductName;
                obj.Quantity = item.Quantity;

                prodect.Add(obj);

            }



            return prodect;

        }


        public ProdectViewModel GetById(int id)
        {
            List<ProdectViewModel> prodect = new List<ProdectViewModel>();
            var item = db.Tbl_Product.FirstOrDefault(x => x.ProductId == id);

            ProdectViewModel obj = new ProdectViewModel();
            obj.ProductId = item.ProductId;
            obj.CreatedDate = item.CreatedDate;
            obj.Description = item.Description;
            obj.IsActive = item.IsActive;
            obj.IsDelete = item.IsDelete;
            obj.IsFeatured = item.IsFeatured;
            obj.ModifiedDate = item.ModifiedDate;
            obj.price = item.price;
            obj.ProductImage = item.ProductImage;
            obj.ProductName = item.ProductName;
            obj.Quantity = item.Quantity;
            obj.CategoryId = item.CategoryId;



            return obj;
        }

        public bool Update(ProdectViewModel prodect)
        {
            var prop = db.Tbl_Product.Where(x => x.ProductId == prodect.ProductId).FirstOrDefault();
            if (prop != null)
            {
                try
                {


                    prop.Description = prodect.Description;
                    prop.IsActive = prodect.IsActive;

                    prop.IsFeatured = prodect.IsFeatured;
                    prop.ModifiedDate = DateTime.Now;
                    prop.price = prodect.price;
                    if (prodect.ProductImage != null)
                    {
                        prop.ProductImage = prodect.ProductImage;
                    }

                    prop.ProductName = prodect.ProductName;
                    prop.Quantity = prodect.Quantity;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            else
            {
                return false;
            }




        }
    }
}
