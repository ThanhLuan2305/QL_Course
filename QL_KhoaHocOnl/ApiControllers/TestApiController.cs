using QL_KhoaHocOnl.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Http;
using QL_KhoaHocOnl.Models;

namespace QL_KhoaHocOnl.ApiControllers
{
    public class TestApiController : ApiController
    {

        QL_COURSEEntities db = new QL_COURSEEntities();
        public List<COURSE> Get()
        {
            return db.COURSEs.ToList();
        }
        public COURSE Get(string id)
        {
            return db.COURSEs.Where(x => x.ID_COURSE == id).FirstOrDefault();
        }
    }
}
