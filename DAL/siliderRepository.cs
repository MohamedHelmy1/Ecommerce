using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using BLL;

namespace DAL
{
    public class siliderRepository
    {
        dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();

        public bool Add(string name, string text)
        {
            try
            {
                Tbl_SlideImage slider = new Tbl_SlideImage();
                slider.SlideImage = name;
                slider.SlideTitle = text;
                db.Tbl_SlideImage.Add(slider);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public IEnumerable<SliderViewModel> GetAllGata()
        {
            List<SliderViewModel> slider = new List<SliderViewModel>();
            foreach (var item in db.Tbl_SlideImage)
            {
                SliderViewModel obj = new SliderViewModel();
                obj.SlideId = item.SlideId;
                obj.SlideImage = item.SlideImage;
                obj.SlideTitle = item.SlideTitle;
                slider.Add(obj);
            }
            return slider;


        }
        public string Delete(int id)
        {
            var Delete = db.Tbl_SlideImage.Where(x => x.SlideId == id).FirstOrDefault();
            if (Delete != null)
            {
                string x = Delete.SlideImage;
                db.Tbl_SlideImage.Remove(Delete);
                db.SaveChanges();
                return x;
            }
            else
            {
                return "";
            }

        }
        public IEnumerable<SliderViewModel> Update(int id)
        {
            List<SliderViewModel> slider = new List<SliderViewModel>();
            foreach (var item in db.Tbl_SlideImage.Where(x => x.SlideId == id))
            {
                SliderViewModel obj = new SliderViewModel();
                obj.SlideImage = item.SlideImage;
                obj.SlideTitle = item.SlideTitle;

                slider.Add(obj);
            }
            return slider;
        }

        public string Update(SliderViewModel slider)
        {
            var Update = db.Tbl_SlideImage.Where(x => x.SlideId == slider.SlideId).FirstOrDefault();
            if (Update != null)
            {
                string x = "0";
                if (slider.SlideImage != null)
                {
                    x = Update.SlideImage;
                    Update.SlideImage = slider.SlideImage;
                }

                Update.SlideTitle = slider.SlideTitle;

                db.SaveChanges();
                return x;
            }
            else
            {
                return "";
            }
        }

    }
}
