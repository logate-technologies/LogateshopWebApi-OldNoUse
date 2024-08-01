using BLL;
using DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LogateShopWebApi.controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        public object Get()
        {
           
            return BLL.Category.Get("", -1, "abcdefg");
        }

        // GET: api/Category/5
        public object Get(int id)
        {
            return Category.GetById(id, -1, "abcdefg");
        }

        // POST: api/Category
        public int Post([FromBody] JObject value)
        {
            return Category.Save(value, -1, "abcdefg");
        }

        // PUT: api/Category/5
        public int Put(int id, [FromBody] JObject value)
        {

            return Category.Save(value, id, "abcdefg");
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
        }
    }
}
