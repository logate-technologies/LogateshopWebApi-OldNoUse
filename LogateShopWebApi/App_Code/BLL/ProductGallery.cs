using DAL;
using LogateShopWebApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class ProductGallery
    {

        public static List<object> Get(string Query, int ClientId, string Token)
        {
            return ProductGalleryDAL.Get(Query, ClientId, Token);
        }
        public static object GetById(int Id, int ClientId, string Token)
        {

            return ProductGalleryDAL.GetById(Id, ClientId, Token); ;
        }
        //public int InsertUpdate(object Tmp,string Token)
        //{

        //    return ProductDAL.InsertUpdate(this, Token);

        //}
        public static int Save(dynamic Tmp, int Id, string Token)
        {

            string ProductPic = Tmp.ProductPic;
            if (ProductPic.Contains("http") && GlobFunc.IsImageUrlValid(ProductPic))// שמירת קובץ התמונה ועדכון אובייקט המוצר
            {

                ProductPic = GlobFunc.SaveProductImage(ProductPic);
            }

            var ProdGallery = new
            {
                ProdId = Id,
                ProductPic = ProductPic
            };
            return ProductGalleryDAL.Save(ProdGallery, -1, Token);

        }
    }
}