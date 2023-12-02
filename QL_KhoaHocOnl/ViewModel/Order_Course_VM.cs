using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_KhoaHocOnl.ViewModel
{
    public class Order_Course_VM
    {
        public string ID_ORDER { get; set; }
        public string ID_COURSE { get; set; }
        public int ID_USER { get; set; }
        public string STATUS_ORDER { get; set; }
        public System.DateTime TIME_AT_ORDER { get; set; }

        public Order_Course_VM(string idCourse, int iduser, string idorder)
        {
            ID_COURSE = idCourse;
            ID_USER = iduser;
            ID_ORDER = idorder;
        }
    }
}