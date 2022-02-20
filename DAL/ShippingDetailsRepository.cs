using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModel;

namespace DAL
{
    interface IShippingDetailsRepository
    {
        bool Add(ShippingDetailsViewModel shop);
        IEnumerable<ShippingDetailsViewModel> ship(int id);
    }
    public class ShippingDetailsRepository : IShippingDetailsRepository
    {
        dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();
        public bool Add(ShippingDetailsViewModel shop)
        {
            try
            {
                Tbl_ShippingDetails obj = new Tbl_ShippingDetails
                {
                    MemberId = shop.MemberId,
                    OrderId = shop.OrderId,
                    phone = shop.Phone,
                    Adress = shop.Adress,
                    City = shop.City,

                };
                db.Tbl_ShippingDetails.Add(obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        //customer Detail
        public IEnumerable<ShippingDetailsViewModel> ship(int id)
        {
            List<ShippingDetailsViewModel> detail = new List<ShippingDetailsViewModel>();
            ShippingDetailsViewModel obj = new ShippingDetailsViewModel();
            var customerDetail = db.Tbl_ShippingDetails.Where(x => x.OrderId == id).FirstOrDefault();
            obj.Phone = customerDetail.phone;
            obj.Adress = customerDetail.Adress;
            obj.City = customerDetail.City;
            var Member = db.Tbl_Members.Where(x => x.MemberId == customerDetail.MemberId).FirstOrDefault();
            string name = Member.FristName + Member.LastName;
            obj.Customername = name;
            detail.Add(obj);
            return detail;
        }
    }
}
