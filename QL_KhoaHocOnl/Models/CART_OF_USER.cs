//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_KhoaHocOnl.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CART_OF_USER
    {
        public int ID_USER { get; set; }
        public string ID_COURSE { get; set; }
    
        public virtual USER_COURSE USER_COURSE { get; set; }

        public CART_OF_USER() { }
        public CART_OF_USER(string Course, int id)
        {
            ID_USER = id;
            ID_COURSE = Course;
        }
    }
}
