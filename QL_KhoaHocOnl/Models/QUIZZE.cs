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
    
    public partial class QUIZZE
    {
        public string QUIZID { get; set; }
        public string ID_COURSE { get; set; }
        public string QUESTION { get; set; }
        public Nullable<System.DateTime> DUEDATE_QUIZ { get; set; }
        public string ANSWER_TRUE { get; set; }
        public string ANSWER_FALSE { get; set; }
        public Nullable<double> POINTS_QUIZ { get; set; }
    
        public virtual COURSE COURSE { get; set; }
    }
}