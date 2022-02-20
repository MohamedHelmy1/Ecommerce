using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModel;

namespace DAL
{
    interface IOrderRepossitory
    {
        int Add(int Memmberid, IEnumerable<Card_Item> orders);
        IEnumerable<GetOrderVviewModel> order();
        IEnumerable<Card_Item> OrderDetail(int id);
    }
    public class OrderRepossitory : IOrderRepossitory
    {
        dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();


        public int Add(int Memmberid, IEnumerable<Card_Item> orders)
        {
            try
            {
                //Number Of Order
                Order order = new Order();
                order.Memberid = Memmberid;
                order.time = DateTime.Now.ToString();
                db.Orders.Add(order);
                db.SaveChanges();
                decimal x = 0;
                //Add product in DataBase
                Tbl_Cart obj = new Tbl_Cart();
                foreach (var item in orders)
                {
                    obj.MemberId = Memmberid;
                    obj.ProductId = item.prodect.ProductId;
                    obj.Quanity = item.Quantity;
                    x = x + (item.prodect.price * item.Quantity);
                    obj.CartStatusId = 2;
                    obj.Order_id = order.id;


                }

                //Tbl_CartStatus A = new Tbl_CartStatus();
                //A.CartStatus = DateTime.Now.ToString();

                //obj.Tbl_CartStatus.CartStatus = DateTime.Now.ToString();
                order.AmountPayed = x;
                db.Tbl_Cart.Add(obj);
                db.SaveChanges();
                return order.id;
            }
            catch (Exception)
            {

                return 0;
            }



        }

        public IEnumerable<GetOrderVviewModel> order()
        {
            List<GetOrderVviewModel> order = new List<GetOrderVviewModel>();

            foreach (var item in db.Orders)
            {
                GetOrderVviewModel obj = new GetOrderVviewModel();
                obj.id = item.id;
                var a = db.Tbl_Members.Where(x => x.MemberId == item.Memberid).FirstOrDefault();
                obj.Name = a.FristName + a.LastName;
                obj.Memberid = item.Memberid;
                obj.time = item.time;

                obj.AmountPayed = item.AmountPayed;

                order.Add(obj);
            }
            return order;

        }

        public IEnumerable<Card_Item> OrderDetail(int id)
        {
            List<Card_Item> order = new List<Card_Item>();
            foreach (var item in db.Tbl_Cart.Where(x => x.Order_id == id))
            {
                Card_Item obj = new Card_Item();

                var item1 = db.Tbl_Product.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                obj.image = item1.ProductImage;
                obj.name = item1.ProductName;

                obj.Amountpayed = item.Order.AmountPayed.ToString();
                string Quantity = item.Quanity.ToString();
                obj.Quantity = int.Parse(Quantity);

                order.Add(obj);
            }
            return order;
        }
        public IEnumerable<GetOrderVviewModel> payedformonth()
        {
            List<GetOrderVviewModel> Amount = new List<GetOrderVviewModel>();

            foreach (var item in db.Orders)
            {
                GetOrderVviewModel obj = new GetOrderVviewModel();
                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(item.time, culture);
                if (DateTime.Now.Year == tempDate.Year)
                {
                    string month = tempDate.ToString("MMMM");
                    obj.time = month;
                    obj.AmountPayed = item.AmountPayed;
                    Amount.Add(obj);
                }


            }
            return Amount;
        }
    }
}
