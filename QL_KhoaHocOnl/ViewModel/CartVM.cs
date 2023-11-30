using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QL_KhoaHocOnl.ViewModel;

namespace QL_KhoaHocOnl.ViewModel
{
    public class CartVM
    {
        public int ID_USER { get; set; }
        public string ID_COURSE { get; set; }
        public string NAME_TYPECOURSE { get; set; }
        public string NAME_TEACHER { get; set; }
        public string NAME_COURSE { get; set; }
        public string DESCRIPTION_COURSE { get; set; }
        public double PRICE_COURSE { get; set; }
        public string THUMBNAIL { get; set; }

        QL_COURSEEntities db = new QL_COURSEEntities();
        public CartVM(string idCourse, int iduser)
        {
            ID_USER = iduser;
            ID_COURSE = idCourse;

            COURSE kh = db.COURSE.Single(item => item.ID_COURSE == ID_COURSE);
            NAME_TYPECOURSE = db.TYPE_COURSE.Where(na => na.ID_TYPECOURSE == kh.ID_TYPECOURSE).Select(st => st.NAME_TYPECOURSE).FirstOrDefault();
            NAME_TEACHER = db.TEACHER.Where(na => na.ID_TEACHER == kh.ID_TEACHER).Select(st => st.NAME_TEACHER).FirstOrDefault();
            NAME_COURSE = kh.NAME_COURSE;
            DESCRIPTION_COURSE = kh.DESCRIPTION_COURSE;
            PRICE_COURSE = kh.PRICE_COURSE;
            THUMBNAIL = kh.THUMBNAIL;
        }
    }
}