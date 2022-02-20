using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModel;

namespace DAL
{
    interface ICatodorey_Repository
    {
        bool Add(CatogoreyViewModel catogorey);
        bool Update(CatogoreyViewModel catogorey);
        bool Delete(int id);
        IEnumerable<CatogoreyViewModel> GetAll();



    }
    public class Catodorey_Repository : ICatodorey_Repository
    {
        dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();
        public bool Add(CatogoreyViewModel catogorey)
        {

            Tbl_Category cat = new Tbl_Category
            {
                CategoryName = catogorey.CategoryName,
                IsActive = true,
                IsDelete = false
            };
            db.Tbl_Category.Add(cat);
            db.SaveChanges();
            return true;


        }

        public bool Delete(int id)
        {
            var Delete = db.Tbl_Category.Where(x => x.CategoryId == id).FirstOrDefault();
            if (Delete != null)
            {
                db.Tbl_Category.Remove(Delete);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public IEnumerable<CatogoreyViewModel> GetAll()
        {
            List<CatogoreyViewModel> catogoreys = new List<CatogoreyViewModel>();
            foreach (var item in db.Tbl_Category.Where(x => x.IsDelete == false))
            {
                CatogoreyViewModel obj = new CatogoreyViewModel();
                obj.CategoryId = item.CategoryId;
                obj.CategoryName = item.CategoryName;
                obj.IsActive = item.IsActive;
                obj.IsDelete = item.IsDelete;
                catogoreys.Add(obj);

            }
            return catogoreys;
        }
        public IEnumerable<CatogoreyViewModel> Update(int id)
        {
            List<CatogoreyViewModel> catogoreys = new List<CatogoreyViewModel>();
            foreach (var item in db.Tbl_Category.Where(x => x.CategoryId == id))
            {
                CatogoreyViewModel obj = new CatogoreyViewModel();
                obj.CategoryId = item.CategoryId;
                obj.CategoryName = item.CategoryName;
                obj.IsActive = item.IsActive;
                obj.IsDelete = item.IsDelete;
                catogoreys.Add(obj);

            }
            return catogoreys;
        }

        public bool Update(CatogoreyViewModel catogorey)
        {
            var Delete = db.Tbl_Category.Where(x => x.CategoryId == catogorey.CategoryId).FirstOrDefault();
            if (Delete != null)
            {

                Delete.CategoryName = catogorey.CategoryName;
                Delete.IsActive = catogorey.IsActive;

                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
