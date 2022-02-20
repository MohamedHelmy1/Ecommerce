using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class ProdectViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public HttpPostedFileBase ProdectImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public string IsFeature { get; set; }
        public Nullable<int> Quantity { get; set; }
        public decimal price { get; set; }
    }
}
