using System;

using System.Collections.Generic;
using System.Web;
using DAL;
using LogateShopWebApi;
using Newtonsoft.Json.Linq;

namespace BLL
{
    // קטגוריות
    public class Category
    {

        public int CatId { get; set; }
        public string CatName { get; set; }
        public int CatParentId { get; set; }        
        
        public int CatStatus { get; set; }

        public static List<object> Get(string Query, int ClientId, string Token)
        {
            return CategoryDAL.Get(Query, ClientId, Token);
        }
        public static object GetById(int Id, int ClientId, string Token)
        {

            return CategoryDAL.GetById(Id, ClientId, Token); ;
        }

        public static int Save(JObject Tmp, int Id, string Token)
        {
           
            return CategoryDAL.Save(Tmp, Id, Token);

        }
        public static int RemoveById(int Id, int ClientId, string Token)
        {

            return CategoryDAL.RemoveById(Id, ClientId, Token);

        }


    }
}