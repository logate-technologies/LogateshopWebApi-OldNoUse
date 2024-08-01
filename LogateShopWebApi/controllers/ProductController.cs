using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LogateShopWebApi.controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        // GET: api/Product
        public object Get()
        {
            //var authorizationHeader = Request.Headers.Authorization;

            //if (authorizationHeader == null || authorizationHeader.Scheme != "Bearer")
            //{
            //    return Unauthorized();
            //}
            
            //var token = authorizationHeader.Parameter;

            //// Validate the token
            //if (token!="abcdefg")
            //{
            //    return Unauthorized();
            //}
            return BLL.Product.Get("", -1, "abcdefg");
        }

        // GET: api/Product/5
        public object Get(int id)
        {
            return Product.GetById(id,-1,"abcdefg");
        }

        // POST: api/Product
        public int Post([FromBody] JObject value)
        {
            int ProdId = 0;
            try
            {
                ProdId= Product.Save(value, -1, "abcdefg");
            }
            catch (Exception ex)
            {
                GlobFunc.Logger(HttpContext.Current.Server.MapPath("~/App_Data/WebApi-Log.txt"), DateTime.Now.ToString() + ex.Message + ex.StackTrace);
                ProdId = -1;
            }
            return ProdId;
        }

        // PUT: api/Product/5
        public int Put(int id, [FromBody] JObject value)
        {

            int ProdId = id;
            //try
            //{
                ProdId=Product.Save(value, id, "abcdefg");
            //}
            //catch (Exception ex)
            //{
            //    GlobFunc.Logger(HttpContext.Current.Server.MapPath("~/App_Data/WebApi-Log.txt"), DateTime.Now.ToString() + ex.Message + ex.StackTrace);
            //    ProdId = -1;
            //}
            return ProdId;
            
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
        }
    }
}
