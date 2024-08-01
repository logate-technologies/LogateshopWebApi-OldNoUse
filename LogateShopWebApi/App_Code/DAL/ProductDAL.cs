using BLL;
using DATA;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Xml;

namespace DAL
{
    public class ProductDAL
    {
        public static List<object> Get(string Query, int ClientId, string Token)
        {
            List<object> lst = new List<object>();
            var parameters = DbContext.CreateParameters(new { Query = "%" + Query + "%", ClientId = ClientId });
            DbContext Db = new DbContext();
            string Sql = "SELECT * FROM t_Products Where 1=1";
            if (ClientId > 0)
                Sql += " And ClientId=@ClientId";
            if (Query != null && Query !="")
            {
                Sql += $" And  ProductName like @Query ";
            }

            DataTable Dt = Db.ExecuteWithParams(Sql, parameters);
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                lst.Add(new 
                {

                    ProdId = int.Parse(Dt.Rows[i]["ProdId"] + ""),
                    Pname = Dt.Rows[i]["Pname"] + "",
                    Model = Dt.Rows[i]["model"] + "",
                    Manufacturer = (Dt.Rows[i]["Manufacturer"] + ""),
                    CtlNo = (Dt.Rows[i]["CtlNo"] + ""),
                    ShortDesc = (Dt.Rows[i]["ShortDesc"] + ""),
                    Stock = int.Parse(Dt.Rows[i]["Stock"] + ""),
                    SaleType = int.Parse(Dt.Rows[i]["AdditionToPrice"] + "" != "" ? Dt.Rows[i]["AdditionToPrice"] + "" : "0"),
                    GroupSaleQty = int.Parse(Dt.Rows[i]["GroupSaleQty"] + "" != "" ? Dt.Rows[i]["GroupSaleQty"] + "" : "0"),
                    MarketPrice = float.Parse(Dt.Rows[i]["MarketPrice"] + "" != "" ? Dt.Rows[i]["MarketPrice"] + "" : "0"),
                    CostPrice = float.Parse(Dt.Rows[i]["CostPrice"] + "" != "" ? Dt.Rows[i]["CostPrice"] + "" : "0"),
                    SitePrice = float.Parse(Dt.Rows[i]["SitePrice"] + "" != "" ? Dt.Rows[i]["SitePrice"] + "" : "0"),
                    CurType = int.Parse(Dt.Rows[i]["CurType"] + "" != "" ? Dt.Rows[i]["CurType"] + "" : "0"),
                    //AdditionToPrice = int.Parse(Dt.Rows[i]["AdditionToPrice"] + "" != "" ? Dt.Rows[i]["AdditionToPrice"] + "" : "0"),
                    //Vat = int.Parse(Dt.Rows[i]["Vat"] + ""),
                    //ShipmentPrice = int.Parse(Dt.Rows[i]["ShipmentPrice"] + "" != "" ? Dt.Rows[i]["ShipmentPrice"] + "" : "0"),
                    //SupplyDays = int.Parse(Dt.Rows[i]["SupplyDays"] + ""),
                    //PaymentNo = int.Parse(Dt.Rows[i]["PaymentNo"] + ""),
                    //InHomePage = int.Parse(Dt.Rows[i]["InHomePage"] + ""),
                    //InSale = int.Parse(Dt.Rows[i]["InSale"] + ""),
                    //InMailingList = int.Parse(Dt.Rows[i]["InMailingList"] + ""),
                    //InSite = int.Parse(Dt.Rows[i]["InSite"] + ""),
                    //ProductPlace = (Dt.Rows[i]["ProductPlace"] + ""),
                    //ProductPic = (Dt.Rows[i]["ProductPic"] + ""),
                    //ProductFiles = (Dt.Rows[i]["ProductFiles"] + ""),
                    //CatId = int.Parse(Dt.Rows[i]["CatId"] + ""),
                    //AdvProducts = (Dt.Rows[i]["AdvProducts"] + ""),
                    //AdditionProp = (Dt.Rows[i]["AdditionProp"] + ""),
                    //AdditionPropPrices = (Dt.Rows[i]["AdditionPropPrices"] + ""),
                    //ProdDesc = (Dt.Rows[i]["ProdDesc"] + ""),
                    //ProductNotes = (Dt.Rows[i]["ProductNotes"] + ""),
                    //ProductAdminNotes = (Dt.Rows[i]["ProductAdminNotes"] + ""),
                    //DateAdded = DateTime.Parse(Dt.Rows[i]["DateAdded"] + ""),


                    //InYellowPages = int.Parse(Dt.Rows[i]["InYellowPages"] + "" != "" ? Dt.Rows[i]["InYellowPages"] + "" : "0"),
                    //InZap = int.Parse(Dt.Rows[i]["InZap"] + "" != "" ? Dt.Rows[i]["InZap"] + "" : "0"),
                    //ShowPrice = int.Parse(Dt.Rows[i]["ShowPrice"] + "" != "" ? Dt.Rows[i]["ShowPrice"] + "" : "0"),
                    //Warranty = (Dt.Rows[i]["warranty"] + ""),
                    //SupplierId = int.Parse(Dt.Rows[i]["SupplierId"] + "" != "" ? Dt.Rows[i]["SupplierId"] + "" : "0"),
                    //TotalView = int.Parse(Dt.Rows[i]["TotalView"] + "" != "" ? Dt.Rows[i]["TotalView"] + "" : "0"),
                    //ZapId = (Dt.Rows[i]["ZapId"] + ""),
                    //ZapPrice = float.Parse(Dt.Rows[i]["ZapPrice"] + "" != "" ? Dt.Rows[i]["ZapPrice"] + "" : "0"),
                    //ZapAgent = int.Parse(Dt.Rows[i]["ZapAgent"] + "" != "" ? Dt.Rows[i]["ZapAgent"] + "" : "0"),
                    //ZapDelta = int.Parse(Dt.Rows[i]["ZapDelta"] + "" != "" ? Dt.Rows[i]["ZapDelta"] + "" : "0"),
                    //MinPrice = float.Parse(Dt.Rows[i]["MinPrice"] + "" != "" ? Dt.Rows[i]["MinPrice"] + "" : "0"),

                    //InWaitingList = int.Parse(Dt.Rows[i]["InWaitingList"] + "" != "" ? Dt.Rows[i]["InWaitingList"] + "" : "0"),
                    //MetaTitle = (Dt.Rows[i]["MetaTitle"] + ""),
                    //MetaKeyWords = (Dt.Rows[i]["MetaKeyWords"] + ""),
                    //MetaDescription = (Dt.Rows[i]["MetaDescription"] + ""),
                    //GetPriceMinPrice = int.Parse(Dt.Rows[i]["GetPriceMinPrice"] + "" != "" ? Dt.Rows[i]["GetPriceMinPrice"] + "" : "0"),


                    //ModelId = int.Parse(Dt.Rows[i]["ModelId"] + "" != "" ? Dt.Rows[i]["ModelId"] + "" : "0"),
                    //LastScan = DateTime.Parse(Dt.Rows[i]["LastScan"] + "" != "" ? Dt.Rows[i]["LastScan"] + "" : DateTime.Now.ToShortDateString()),
                    //ShopName = (Dt.Rows[i]["ShopName"] + ""),
                    //ZapPrice2 = float.Parse(Dt.Rows[i]["ZapPrice2"] + "" != "" ? Dt.Rows[i]["ZapPrice2"] + "" : "0"),
                    //ShopName2 = (Dt.Rows[i]["ShopName2"] + ""),
                    //PreScanPrice = float.Parse(Dt.Rows[i]["PreScanPrice"] + "" != "" ? Dt.Rows[i]["PreScanPrice"] + "" : "0"),
                    //ZapLink = (Dt.Rows[i]["ZapLink"] + ""),
                    //ArticleIds = (Dt.Rows[i]["ArticleIDs"] + ""),
                    //Redir301 = (Dt.Rows[i]["redir301"] + ""),
                    //MetaCanonical = (Dt.Rows[i]["MetaCanonical"] + ""),
                    //CustomButton = (Dt.Rows[i]["CustomButton"] + ""),
                    //CustomButtonLink = (Dt.Rows[i]["CustomButtonLink"] + ""),
                    //FriendlyURL = (Dt.Rows[i]["FriendlyURL"] + ""),
                    //ProdColor = (Dt.Rows[i]["ProdColor"] + ""),
                    //ProdWeight = int.Parse(Dt.Rows[i]["ProdWeight"] + "" != "" ? Dt.Rows[i]["ProdWeight"] + "" : "0"),
                    //ProdHeight = int.Parse(Dt.Rows[i]["ProdHeight"] + "" != "" ? Dt.Rows[i]["ProdHeight"] + "" : "0"),
                    //ProdWidth = int.Parse(Dt.Rows[i]["ProdWidth"] + "" != "" ? Dt.Rows[i]["ProdWidth"] + "" : "0"),
                    //ProdDepth = int.Parse(Dt.Rows[i]["ProdDepth"] + "" != "" ? Dt.Rows[i]["ProdDepth"] + "" : "0"),
                    //QtyStart = int.Parse(Dt.Rows[i]["QtyStart"] + "" != "" ? Dt.Rows[i]["QtyStart"] + "" : "0"),
                    //QtySteps = int.Parse(Dt.Rows[i]["QtySteps"] + "" != "" ? Dt.Rows[i]["QtySteps"] + "" : "0"),
                    //QtyCount = int.Parse(Dt.Rows[i]["QtyCount"] + "" != "" ? Dt.Rows[i]["QtyCount"] + "" : "0"),
                    //QtyStart2 = int.Parse(Dt.Rows[i]["QtyStart2"] + "" != "" ? Dt.Rows[i]["QtyStart2"] + "" : "0"),
                    //QtySteps2 = int.Parse(Dt.Rows[i]["QtySteps2"] + "" != "" ? Dt.Rows[i]["QtySteps2"] + "" : "0"),
                    //QtyCount2 = int.Parse(Dt.Rows[i]["QtyCount2"] + "" != "" ? Dt.Rows[i]["QtyCount2"] + "" : "0"),
                    //SearchWords = (Dt.Rows[i]["SearchWords"] + ""),
                    //Youtubelink = (Dt.Rows[i]["Youtubelink"] + ""),
                    //BundleProducts = (Dt.Rows[i]["BundleProducts"] + ""),
                    //BundleProductsDiscount = int.Parse(Dt.Rows[i]["BundleProductsDiscount"] + "" != "" ? Dt.Rows[i]["BundleProductsDiscount"] + "" : "0"),
                    //VipPrice = float.Parse(Dt.Rows[i]["VipPrice"] + "" != "" ? Dt.Rows[i]["VipPrice"] + "" : "0"),
                    //AllowUpgradesOnly = int.Parse(Dt.Rows[i]["AllowUpgradesOnly"] + "" != "" ? Dt.Rows[i]["AllowUpgradesOnly"] + "" : "0"),
                    //ProductType = int.Parse(Dt.Rows[i]["ProductType"] + "" != "" ? Dt.Rows[i]["ProductType"] + "" : "0"),
                    //ParentId = long.Parse(Dt.Rows[i]["ParentId"] + "" != "" ? Dt.Rows[i]["ParentId"] + "" : "0"),
                    //FreeShipment = (Dt.Rows[i]["FreeShipment"] + "")
                });

            }
            Db.Close();
            return lst;
        }
        public static object GetById(int Id, int ClientId, string Token)
        {
           // var tmp ="";
            var parameters = DbContext.CreateParameters(new { Id = Id, ClientId = ClientId });
            DbContext Db = new DbContext();
            string Sql = $"SELECT * FROM t_Products Where ProdId=@Id ";
            if (ClientId > 0)
                Sql += " And ClientId=@ClientId";

            DataTable Dt = Db.ExecuteWithParams(Sql, parameters);
            if (Dt.Rows.Count > 0)
            {
              var  tmp = new 
                { // רשימת שדות סטנדרטית לכל מוצר בלוגייט שופ לצורך קריאה בלבד
                  
                  ProdId = int.Parse(Dt.Rows[0]["ProdId"] + ""),
                    Pname = Dt.Rows[0]["Pname"] + "",
                    Model = Dt.Rows[0]["model"] + "",
                    Manufacturer = (Dt.Rows[0]["Manufacturer"] + ""),
                    CtlNo = (Dt.Rows[0]["CtlNo"] + ""),
                    ShortDesc = (Dt.Rows[0]["ShortDesc"] + ""),
                    Stock = int.Parse(Dt.Rows[0]["Stock"] + ""),
                    SaleType = int.Parse(Dt.Rows[0]["SaleType"] + ""),
                    GroupSaleQty = int.Parse(Dt.Rows[0]["GroupSaleQty"] + ""),
                    MarketPrice = float.Parse(Dt.Rows[0]["MarketPrice"] + ""),
                    CostPrice = float.Parse(Dt.Rows[0]["CostPrice"] + ""),
                    SitePrice = float.Parse(Dt.Rows[0]["SitePrice"] + ""),
                    CurType = int.Parse(Dt.Rows[0]["CurType"] + ""),
                  AdditionToPrice = int.Parse(Dt.Rows[0]["AdditionToPrice"] + "" != "" ? Dt.Rows[0]["AdditionToPrice"] + "" : "0"),
                  Vat = int.Parse(Dt.Rows[0]["Vat"] + ""),
                  ShipmentPrice = int.Parse(Dt.Rows[0]["ShipmentPrice"] + "" != "" ? Dt.Rows[0]["ShipmentPrice"] + "" : "0"),
                  SupplyDays = int.Parse(Dt.Rows[0]["SupplyDays"] + ""),
                  PaymentNo = int.Parse(Dt.Rows[0]["PaymentNo"] + ""),
                  InHomePage = int.Parse(Dt.Rows[0]["InHomePage"] + ""),
                  InSale = int.Parse(Dt.Rows[0]["InSale"] + ""),
                  InMailingList = int.Parse(Dt.Rows[0]["InMailingList"] + ""),
                  InSite = int.Parse(Dt.Rows[0]["InSite"] + ""),
                  ProductPlace = (Dt.Rows[0]["ProductPlace"] + ""),
                  ProductPic = (Dt.Rows[0]["ProductPic"] + ""),
                  ProductFiles = (Dt.Rows[0]["ProductFiles"] + ""),
                  CatId = int.Parse(Dt.Rows[0]["CatId"] + ""),
                  AdvProducts = (Dt.Rows[0]["AdvProducts"] + ""),
                  AdditionProp = (Dt.Rows[0]["AdditionProp"] + ""),
                  AdditionPropPrices = (Dt.Rows[0]["AdditionPropPrices"] + ""),
                  ProdDesc = (Dt.Rows[0]["ProdDesc"] + ""),
                  ProductNotes = (Dt.Rows[0]["ProductNotes"] + ""),
                  ProductAdminNotes = (Dt.Rows[0]["ProductAdminNotes"] + ""),
                  DateAdded = DateTime.Parse(Dt.Rows[0]["DateAdded"] + ""),

                  InYellowPages = int.Parse(Dt.Rows[0]["InYellowPages"] + "" != "" ? Dt.Rows[0]["InYellowPages"] + "" : "0"),
                  InZap = int.Parse(Dt.Rows[0]["InZap"] + "" != "" ? Dt.Rows[0]["InZap"] + "" : "0"),
                  ShowPrice = int.Parse(Dt.Rows[0]["ShowPrice"] + "" != "" ? Dt.Rows[0]["ShowPrice"] + "" : "0"),
                  Warranty = (Dt.Rows[0]["warranty"] + ""),
                  SupplierId = int.Parse(Dt.Rows[0]["SupplierId"] + "" != "" ? Dt.Rows[0]["SupplierId"] + "" : "0"),
                  TotalView = int.Parse(Dt.Rows[0]["TotalView"] + "" != "" ? Dt.Rows[0]["TotalView"] + "" : "0"),
                  ZapId = (Dt.Rows[0]["ZapId"] + ""),
                  ZapPrice = float.Parse(Dt.Rows[0]["ZapPrice"] + "" != "" ? Dt.Rows[0]["ZapPrice"] + "" : "0"),
                  ZapAgent = int.Parse(Dt.Rows[0]["ZapAgent"] + "" != "" ? Dt.Rows[0]["ZapAgent"] + "" : "0"),
                  ZapDelta = int.Parse(Dt.Rows[0]["ZapDelta"] + "" != "" ? Dt.Rows[0]["ZapDelta"] + "" : "0"),
                  MinPrice = float.Parse(Dt.Rows[0]["MinPrice"] + "" != "" ? Dt.Rows[0]["MinPrice"] + "" : "0"),
                  //InWaitingList = int.Parse(Dt.Rows[0]["InWaitingList"] + "" != "" ? Dt.Rows[0]["InWaitingList"] + "" : "0"),
                  //MetaTitle = (Dt.Rows[0]["MetaTitle"] + ""),
                  //MetaKeyWords = (Dt.Rows[0]["MetaKeyWords"] + ""),
                  //MetaDescription = (Dt.Rows[0]["MetaDescription"] + ""),
                  //GetPriceMinPrice = int.Parse(Dt.Rows[0]["GetPriceMinPrice"] + "" != "" ? Dt.Rows[0]["GetPriceMinPrice"] + "" : "0"),


                  ModelId = int.Parse(Dt.Rows[0]["ModelId"] + "" != "" ? Dt.Rows[0]["ModelId"] + "" : "0"),
                  LastScan = DateTime.Parse(Dt.Rows[0]["LastScan"] + "" != "" ? Dt.Rows[0]["LastScan"] + "" : DateTime.Now.ToShortDateString()),
                  ShopName = (Dt.Rows[0]["ShopName"] + ""),
                  ZapPrice2 = float.Parse(Dt.Rows[0]["ZapPrice2"] + "" != "" ? Dt.Rows[0]["ZapPrice2"] + "" : "0"),
                  ShopName2 = (Dt.Rows[0]["ShopName2"] + ""),
                  PreScanPrice = float.Parse(Dt.Rows[0]["PreScanPrice"] + "" != "" ? Dt.Rows[0]["PreScanPrice"] + "" : "0"),
                  ZapLink = (Dt.Rows[0]["ZapLink"] + ""),
                  ArticleIds = (Dt.Rows[0]["ArticleIDs"] + ""),
                  Redir301 = (Dt.Rows[0]["redir301"] + ""),
                  MetaCanonical = (Dt.Rows[0]["MetaCanonical"] + ""),
                  CustomButton = (Dt.Rows[0]["CustomButton"] + ""),
                  CustomButtonLink = (Dt.Rows[0]["CustomButtonLink"] + ""),
                  FriendlyURL = (Dt.Rows[0]["FriendlyURL"] + ""),
                  ProdColor = (Dt.Rows[0]["ProdColor"] + ""),
                  ProdWeight = int.Parse(Dt.Rows[0]["ProdWeight"] + "" != "" ? Dt.Rows[0]["ProdWeight"] + "" : "0"),
                  ProdHeight = int.Parse(Dt.Rows[0]["ProdHeight"] + "" != "" ? Dt.Rows[0]["ProdHeight"] + "" : "0"),
                  ProdWidth = int.Parse(Dt.Rows[0]["ProdWidth"] + "" != "" ? Dt.Rows[0]["ProdWidth"] + "" : "0"),
                  ProdDepth = int.Parse(Dt.Rows[0]["ProdDepth"] + "" != "" ? Dt.Rows[0]["ProdDepth"] + "" : "0"),
                  QtyStart = int.Parse(Dt.Rows[0]["QtyStart"] + "" != "" ? Dt.Rows[0]["QtyStart"] + "" : "0"),
                  QtySteps = int.Parse(Dt.Rows[0]["QtySteps"] + "" != "" ? Dt.Rows[0]["QtySteps"] + "" : "0"),
                  QtyCount = int.Parse(Dt.Rows[0]["QtyCount"] + "" != "" ? Dt.Rows[0]["QtyCount"] + "" : "0"),
                  QtyStart2 = int.Parse(Dt.Rows[0]["QtyStart2"] + "" != "" ? Dt.Rows[0]["QtyStart2"] + "" : "0"),
                  QtySteps2 = int.Parse(Dt.Rows[0]["QtySteps2"] + "" != "" ? Dt.Rows[0]["QtySteps2"] + "" : "0"),
                  QtyCount2 = int.Parse(Dt.Rows[0]["QtyCount2"] + "" != "" ? Dt.Rows[0]["QtyCount2"] + "" : "0"),
                  SearchWords = (Dt.Rows[0]["SearchWords"] + ""),
                  Youtubelink = (Dt.Rows[0]["Youtubelink"] + ""),
                  BundleProducts = (Dt.Rows[0]["BundleProducts"] + ""),
                  //BundleProductsDiscount = int.Parse(Dt.Rows[0]["BundleProductsDiscount"] + "" != "" ? Dt.Rows[0]["BundleProductsDiscount"] + "" : "0"),
                  VipPrice = float.Parse(Dt.Rows[0]["VipPrice"] + "" != "" ? Dt.Rows[0]["VipPrice"] + "" : "0"),
                  AllowUpgradesOnly = int.Parse(Dt.Rows[0]["AllowUpgradesOnly"] + "" != "" ? Dt.Rows[0]["AllowUpgradesOnly"] + "" : "0"),
                  ProductType = int.Parse(Dt.Rows[0]["ProductType"] + "" != "" ? Dt.Rows[0]["ProductType"] + "" : "0"),
                  ParentId = long.Parse(Dt.Rows[0]["ParentId"] + "" != "" ? Dt.Rows[0]["ParentId"] + "" : "0"),
                  FreeShipment = (Dt.Rows[0]["FreeShipment"] + "" )

              };
                return tmp;
            }
            return null;
        }
        public static int Save(JObject Tmp,int Id, string Token)
        {

            var parameters = DbContext.CreateDynamicParameters(Tmp);
            string Sql = null;
            DbContext Db = new DbContext();            
           
            
            if (Id == -1)
            {
                Sql = "INSERT INTO T_Products ";
                Id = (int)Db.GetMaxId("T_Products", "ProdId");
                Id++;
                parameters.Add(new OleDbParameter("ProdId",Id));
                var Fields = new
                { // רשימת שדות מלאה עם ערכי ברירת מחדל

                    //ProdId = -1,
                    Pname = "ללא שם",
                    Model = "",
                    Manufacturer = (""),
                    CtlNo = (""),
                    ShortDesc = (""),
                    Stock = 0,
                    SaleType = 1,
                    GroupSaleQty = 0,
                    MarketPrice = 0,
                    CostPrice = 0,
                    SitePrice = 0,
                    CurType = 0,// ש"ח
                    AdditionToPrice = 0,
                    Vat = 1,// כולל מע"מ
                    ShipmentPrice = 0,
                    SupplyDays = 7,
                    PaymentNo = 3,
                    InHomePage = 0,
                    InSale = 0,
                    InMailingList = 0,
                    InSite = 1,
                    ProductPlace = 99,
                    ProductPic = "",
                    ProductFiles = "",
                    CatId = 0,
                    //AdvProducts ="",
                    //AdditionProp = "",
                    //AdditionPropPrices = "",
                    ProdDesc = "",
                    ProductNotes = "",
                    ProductAdminNotes = "",
                    DateAdded = DateTime.Now.ToString("dd/MM/yyyy"),

                    InYellowPages = 1,
                    InZap = 1,
                    ShowPrice = 1,
                    Warranty = "",
                    SupplierId = 0,
                    TotalView = 0,
                    //ZapId = "",

                    //ZapPrice = 0,
                    //ZapAgent = "",
                    //ZapDelta = 0,
                    //MinPrice = 0,
                    //InWaitingList =0,
                    //MetaTitle = "",
                    //MetaKeyWords = "",
                    //MetaDescription = "",
                    GetPriceMinPrice = 0,


                    //ModelId = 0,
                    //LastScan = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy"),
                    //ShopName =  "",
                    //ZapPrice2 =0,
                    //ShopName2 ="",
                    //PreScanPrice =0,
                    //ZapLink ="",
                    //ArticleIds ="",
                    //Redir301 = "",
                    //MetaCanonical = "",
                    //CustomButton = "",
                    //CustomButtonLink = "",
                    //FriendlyURL = "",
                    //ProdColor = "",
                    //ProdWeight = 0,
                    //ProdHeight = 0,
                    //ProdWidth = 0,
                    //ProdDepth = 0,
                    QtyStart = 1,
                    QtySteps = 1,
                    QtyCount = 99,
                    QtyStart2 = 0,
                    QtySteps2 = 0,
                    QtyCount2 = 0,
                    //SearchWords = "",
                    //Youtubelink = "",
                    //BundleProducts = "",
                    BundleProductsDiscount = 0,
                    VipPrice = 0,
                    //AllowUpgradesOnly =0,
                    ProductType = 1,
                    ParentId = 0,
                    FreeShipment = "N"

                };
                DbContext.AddDefaultMissingFields(parameters, Fields);
                Sql += DbContext.CreateSqlStatementWithParameters(parameters, 1);
               
            }
            else
            {
                Sql = $"UPDATE T_Products set ";
                Sql += DbContext.CreateSqlStatementWithParameters(parameters, 2);  
                Sql += $" Where ProdId={Id}";
            }
           
            int TotalRec = Db.ExecuteNonQueryWithParams(Sql, parameters);            
            Db.Close();
            return Id;// החזרת קוד המוצר
        }

       
        public static int RemoveById(int Id, int ClientId, string Token)
        {
            var parameters = DbContext.CreateParameters(new { Id = Id, ClientId = ClientId });
            string Sql = $" Delete FROM T_Product Where ProdId=@Id ";
            if (ClientId > 0)
                Sql += " And ClientId=@ClientId";
            DbContext Db = new DbContext();
            int TotalRec = Db.ExecuteNonQueryWithParams(Sql, parameters);
            Db.Close();
            return TotalRec;
        }

        public static void AddDefaultMissingFields(List<OleDbParameter> LstParams)
        {
            
            var Tmp = new
            { // רשימת שדות מלאה עם ערכי ברירת מחדל

                //ProdId = -1,
                Pname = "ללא שם",
                Model =  "",
                Manufacturer = (""),
                CtlNo = (""),
                ShortDesc = (""),
                Stock = 0,
                SaleType = 1,
                GroupSaleQty = 0,
                MarketPrice = 0,
                CostPrice = 0,
                SitePrice = 0,
                CurType = 0,// ש"ח
                AdditionToPrice = 0,
                Vat = 1,// כולל מע"מ
                ShipmentPrice = 0,
                SupplyDays = 7,
                PaymentNo = 3,
                InHomePage = 0,
                InSale = 0,
                InMailingList = 0,
                InSite = 1,
                ProductPlace = 99,
                ProductPic = "",
                ProductFiles = "",
                CatId = 0,
                //AdvProducts ="",
                //AdditionProp = "",
                //AdditionPropPrices = "",
                ProdDesc = "",
                ProductNotes = "",
                ProductAdminNotes = "",
                DateAdded = DateTime.Now.ToString("dd/MM/yyyy"),

                InYellowPages = 1,
                InZap = 1,
                ShowPrice = 1,
                Warranty = "",
                SupplierId = 0,
                TotalView = 0,
                //ZapId = "",

                //ZapPrice = 0,
                //ZapAgent = "",
                //ZapDelta = 0,
                //MinPrice = 0,
                //InWaitingList =0,
                //MetaTitle = "",
                //MetaKeyWords = "",
                //MetaDescription = "",
                GetPriceMinPrice =0,


                //ModelId = 0,
                //LastScan = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy"),
                //ShopName =  "",
                //ZapPrice2 =0,
                //ShopName2 ="",
                //PreScanPrice =0,
                //ZapLink ="",
                //ArticleIds ="",
                //Redir301 = "",
                //MetaCanonical = "",
                //CustomButton = "",
                //CustomButtonLink = "",
                //FriendlyURL = "",
                //ProdColor = "",
                //ProdWeight = 0,
                //ProdHeight = 0,
                //ProdWidth = 0,
                //ProdDepth = 0,
                QtyStart = 1,
                QtySteps = 1,
                QtyCount = 99,
                QtyStart2 = 0,
                QtySteps2 = 0,
                QtyCount2 = 0,
                //SearchWords = "",
                //Youtubelink = "",
                //BundleProducts = "",
                BundleProductsDiscount = 0,
                VipPrice =0,
                //AllowUpgradesOnly =0,
                ProductType =1,
                ParentId =0,
                FreeShipment = "N"

            };
            // הגדרת רשימת פרמטרים ליישות עם ערכי ברירת מחדל
            List<OleDbParameter> LstDefault = DbContext.CreateParameters(Tmp);

            // מעבר על רשימת שדות הברירת מחדל, ובדיקה האם הם קיימים ברשימה שקיבלנו
            // במידה והשדה לא נמצא ברשימה, מעתיקים אותו לרשימה שקיבלנו
            
            var NameLstDefault = LstDefault.Select(p => p.ParameterName).ToList();
            var NamesLstParams = LstParams.Select(p => p.ParameterName).ToList();

            // Find parameters in list1 that do not exist in list2
            var parametersToAdd = LstDefault.Where(p => !NamesLstParams.Contains(p.ParameterName)).ToList();

            // Add missing parameters to list2
            LstParams.AddRange(parametersToAdd);

            


        }
    }
}