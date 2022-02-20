using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class orderStattasView_Model
    {
        public int CartStatusId { get; set; }
        public string CartStatus { get; set; }
        public Nullable<bool> see_from_Admin { get; set; }
        public List<Card> Orders { get; set; }
    }
}
