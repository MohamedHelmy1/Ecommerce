using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class SliderViewModel
    {
        public int SlideId { get; set; }
        public string SlideTitle { get; set; }
        public string SlideImage { get; set; }
        public HttpPostedFileBase SlideImag { get; set; }
    }
}
