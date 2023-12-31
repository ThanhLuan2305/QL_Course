﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QL_COURSEEntities : DbContext
    {
        public QL_COURSEEntities()
            : base("name=QL_COURSEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<ARTICLE> ARTICLEs { get; set; }
        public virtual DbSet<CART_OF_USER> CART_OF_USER { get; set; }
        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<COMMENT_LESSON> COMMENT_LESSON { get; set; }
        public virtual DbSet<COURSE> COURSEs { get; set; }
        public virtual DbSet<FEEDBACK> FEEDBACKs { get; set; }
        public virtual DbSet<GRADE> GRADES { get; set; }
        public virtual DbSet<LESSON> LESSONs { get; set; }
        public virtual DbSet<ORDER_COURSE> ORDER_COURSE { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizs { get; set; }
        public virtual DbSet<QUIZZE> QUIZZES { get; set; }
        public virtual DbSet<TEACHER> TEACHERs { get; set; }
        public virtual DbSet<TYPE_COURSE> TYPE_COURSE { get; set; }
        public virtual DbSet<USER_COURSE> USER_COURSE { get; set; }
        public virtual DbSet<vw_ARTICLE_WITH_USER> vw_ARTICLE_WITH_USER { get; set; }
    
        public virtual int PRO_UPDATE_COUNTLESSONS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PRO_UPDATE_COUNTLESSONS");
        }
    
        public virtual int PRO_UPDATE_USER_COUNT_COURSE()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PRO_UPDATE_USER_COUNT_COURSE");
        }
    }
}
