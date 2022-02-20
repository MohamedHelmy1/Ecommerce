using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GetOrderVviewModel
    {

        public int id { get; set; }
        public string time { get; set; }
        public string Name { get; set; }
        public Nullable<int> Memberid { get; set; }
        public Nullable<decimal> AmountPayed { get; set; }
    }
}
