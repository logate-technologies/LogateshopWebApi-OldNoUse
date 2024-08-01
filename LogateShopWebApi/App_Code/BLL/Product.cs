using DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using LogateShopWebApi;

namespace BLL
{
    public class Product
    {

        // SELECT T_products.ShipmentPrice, T_products.SupplyDays, T_products.PaymentNo, T_products.InHomePage, T_products.InSale, T_products.InMailingList, T_products.InSite, T_products.productPlace, T_products.ProductPic, T_products.ProductFiles, T_products.catid, T_products.AdvProducts, T_products.AdditionProp, T_products.AdditionPropPrices, T_products.prodDesc, T_products.ProductNotes, T_products.ProductAdminNotes, T_products.DateAdded, T_products.InYellowPages, T_products.InZap, T_products.ShowPrice, T_products.warranty, T_products.supplierId, T_products.TotalView, T_products.ZapId, T_products.ZapPrice, T_products.ZapAgent, T_products.ZapDelta, T_products.MinPrice, T_products.InWaitingList, T_products.MetaTitle, T_products.MetaKeyWords, T_products.MetaDescription, T_products.GetPriceMinPrice, T_products.ModelID, T_products.LastScan, T_products.ShopName, T_products.ZapPrice2, T_products.ShopName2, T_products.PreScanPrice, T_products.ZapLink, T_products.ArticleIDs, T_products.redir301, T_products.metacanonical, T_products.custombutton, T_products.custombuttonlink, T_products.friendlyURL, T_products.prodcolor, T_products.prodweight, T_products.prodheight, T_products.prodwidth, T_products.proddepth, T_products.QtyStart, T_products.QtySteps, T_products.QtyCount, T_products.QtyStart2, T_products.QtySteps2, T_products.QtyCount2, T_products.SearchWords, T_products.youtubelink, T_products.bundleproducts, T_products.bundleproductsDiscount, T_products.VipPrice, T_products.allowupgradesonly, T_products.ProductType, T_products.ParentId, T_products.freeshipment
        //  FROM T_products;
       
        public int ProdId { get; set; }
        public string Pname { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string CtlNo { get; set; }
        public string ShortDesc { get; set; }
        public int Stock { get; set; }
        public int SaleType { get; set; }
        public int GroupSaleQty { get; set; }
        public float MarketPrice { get; set; }
        public float CostPrice { get; set; }
        public float SitePrice { get; set; }
        public int CurType { get; set; }
        public int AdditionToPrice { get; set; }
        public int Vat { get; set; }

        public int PricingType { get; set; }
      
        public float SalePrice { get; set; }
        public int ShipmentPrice { get; set; }
        public int SupplierId { get; set; }
        public int ManufacturerId { get; set; }
        
        public string Warranty { get; set; }
        public string SmallDesc { get; set; }
        public string Pdesc { get; set; }
        public int DlvTime { get; set; }
        public float ZapMinValue { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; }
        public DateTime Added { get; set; }
        public string CatId { get; set; }
        public int InZap { get; set; }
        public int ManagedByBot { get; set; }
        public int RemoveFromSite { get; set; }
        public static List<object> Get(string Query, int ClientId, string Token)
        {
            return ProductDAL.Get(Query, ClientId, Token);
        }
        public static object GetById(int Id, int ClientId, string Token)
        {

            return ProductDAL.GetById(Id, ClientId, Token); ;
        }
      
        public static int Save(JObject Tmp, int Id, string Token)
        {
            JToken ProductPicValue;
            if (Tmp.TryGetValue("ProductPic", out ProductPicValue))// במידה ונשלחה תמונת מוצר, קישור לתמונה
            {
                string ProductPic = ProductPicValue.ToString();
                if(ProductPic.Contains("http") && GlobFunc.IsImageUrlValid(ProductPic))// שמירת קובץ התמונה ועדכון אובייקט המוצר
                {
                    
                    GlobFunc.SetPropertyValue(Tmp, "ProductPic", GlobFunc.SaveProductImage(ProductPic));
                }
                    
            }
            return ProductDAL.Save(Tmp, Id, Token);

        }
        public static int RemoveById(int Id, int ClientId, string Token)
        {

            return ProductDAL.RemoveById(Id, ClientId, Token);

        }

    }
}