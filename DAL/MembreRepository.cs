using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using BLL;

namespace DAL
{
    interface IMembreRepository
    {

        IEnumerable<MemberViewModel> login(MemberViewModel member);
        IEnumerable<MemberViewModel> Register(MemberViewModel member);



    }
    public class MembreRepository : IMembreRepository
    {
        dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();

        public IEnumerable<MemberViewModel> login(MemberViewModel member)
        {
            MemberViewModel log = new MemberViewModel();
            var Login = db.Tbl_Members.Where(x => x.EmailId == member.EmailId && x.Password == member.Password).FirstOrDefault();
            if (Login != null)
            {

                log.Roleid = Login.Roleid;
                log.MemberId = Login.MemberId;
                List<MemberViewModel> mem = new List<MemberViewModel>();
                mem.Add(log);

                return mem;

            }
            else
            {
                return null;

            }
        }

        public IEnumerable<MemberViewModel> Register(MemberViewModel Register)
        {
            var register = db.Tbl_Members.Where(x => x.EmailId == Register.EmailId).FirstOrDefault();
            if (register != null)
            {
                return null;
            }
            else
            {
                try
                {
                    Tbl_Members Member = new Tbl_Members
                    {
                        FristName = Register.FristName,
                        LastName = Register.LastName,
                        EmailId = Register.EmailId,
                        CreatedOn = DateTime.Now,
                        IsActive = true,
                        IsDelete = false,
                        Password = Register.Password,
                        Roleid = 2,

                    };



                    db.Tbl_Members.Add(Member);

                    db.SaveChanges();
                    Register.Roleid = 2;
                    Register.CreatedOn = DateTime.Now;
                    Register.IsActive = true;
                    Register.IsDelete = false;
                    List<MemberViewModel> mem = new List<MemberViewModel>
                    {
                        Register
                    };
                    return mem;
                }
                catch (Exception)
                {

                    return null;
                }




            }

        }
        public string[] CountCustomer()
        {


            int[] myNum = { 10, 20, 30, 40 };


            string order = db.Orders.Count().ToString();
            string AllMoney = db.Orders.Select(x => x.AmountPayed).Sum().ToString();
            string prpduct = db.Tbl_Product.Count().ToString();
            string xx = db.Tbl_Members.Count().ToString();
            string[] arr = { order, AllMoney, prpduct, xx };
            return arr;
        }
    }
}
