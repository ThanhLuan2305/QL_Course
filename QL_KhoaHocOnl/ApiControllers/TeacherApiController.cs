using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.ApiControllers
{
    public class TeacherApiController : ApiController
    {
        QL_COURSEEntities db = new QL_COURSEEntities();
        public List<TEACHER> Get()
        {
            return db.TEACHERs.ToList();
        }
        public TEACHER GetById(string id)
        {
            return db.TEACHERs.Where(x => x.ID_TEACHER == id).FirstOrDefault();
        }
        public void Update(string id)
        {

        }
        public void Insert()
        {

        }
        public void Delete(string id)
        {

        }
    }
}
