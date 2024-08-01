using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LogateShopWebApi
{
    public class ProductPicController : ApiController
    {
        // GET: api/ProductPic
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProductPic/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProductPic
        public string Post([FromBody] JObject value)// נקודת הקצה מקבלת תמונה,שומרת אותה ומחזירה את השם שנתנה לתמונה
        {
            // לא בשימוש שמירת התמונה מתבצעת דרך המוצר
            // הוספ תמונה ראשית
            // החזרת שם התמונה לצורך שמירה ב בסיס הנתנים
            JToken ProductPicValue;
            if (value.TryGetValue("ProductPic", out ProductPicValue))
            {
                string ProductPic = ProductPicValue.ToString();

                if (ProductPic.Contains("http") && GlobFunc.IsImageUrlValid(ProductPic))// שמירת קובץ התמונה ועדכון אובייקט המוצר
                {

                    return GlobFunc.SaveProductImage(ProductPic);
                }

            }
            return "";

        }



        // PUT: api/ProductPic/5
        public string Put(int id, [FromBody] JObject value)
        {
            // בדיקה שהמוצר קיים, במידה וכן מעבר על העמרך ושמירת כל התמונות
            //החזרת קודי התמונות במאגר התמונות
            string ProductGalleryNames = "";
            string Token = "abcdegf";
            int ProdId = id;
            JToken ProdIdValue;

            ProdId = id;
            object Pr = Product.GetById(ProdId, -1, Token);
            if (Pr != null)
            {
                JToken ArrProductPicsValue;
                if (value.TryGetValue("ArrProductPics", out ArrProductPicsValue) && ArrProductPicsValue is JArray)
                {
                    string[] ArrProductPics = ArrProductPicsValue.Select(p => p.ToString()).ToArray();
                    for (int i = 0; i < ArrProductPics.Length; i++)
                    {
                        string ProductPic = ArrProductPics[i];
                        if (ProductPic.Contains("http") && GlobFunc.IsImageUrlValid(ProductPic))// שמירת קובץ התמונה ועדכון אובייקט המוצר
                        {
                            var ProdGallery = new
                            {
                                ProdId = ProdId,
                                ProductPic = ProductPic
                            };
                            ProductGalleryNames += ProductGallery.Save(ProdGallery, ProdId, Token) + ",";
                        }
                    }
                }

            }




            return ProductGalleryNames;
        }

        // DELETE: api/ProductPic/5
        public void Delete(int id)
        {
        }
    }
}

