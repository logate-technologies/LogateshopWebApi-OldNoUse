using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Resources;
using System.Reflection;

namespace LogateShopWebApi
{
    public class PriceOverloading {
        public static string Addition = "+";
        public static string Percent = "%";
};
    public class Settings
    {
        // WebSites
        public string WebSiteLogoPath { get; set; }
        public string WebSiteLogoDefaultImageName { get; set; }
        public string[] AllowedImageExtensions { get; set; }
        public string TokenSecret { get; set; }
        public  float Vat { get; set; }
        public string MeshulamUserId { get; set; } // קוד משתמש משולם
        public  string MeshulamPageCode { get; set; }// קוד עסקת חיוב באשראי משולם
        public  string MeshulamPaymentUrl { get; set; }
        public  string MeshulamApproveUrl { get; set; }
        public  string MeshulamPaymentTemplate { get; set; }
        public  string SuccesUrl { get; set; }
        public  string CancelUrl { get; set; }     
        public  string PublishLink { get; set; }
        public string ExcelSuccesUrl { get; set; }
        public string ExcelCancelUrl { get; set; }
        public string PayNowSuccesUrl { get; set; }
        public string PayNowCancelUrl { get; set; }

        
        public static string GetGlobalResourceValue(string ResourceKey)
        {
           
            string resourceValue = HttpContext.GetGlobalResourceObject("Glob", ResourceKey)?.ToString();
            return resourceValue;
        }
        public  void LoadResources()
        {
           
        WebSiteLogoPath = GetGlobalResourceValue("WebSiteLogoPath"); 
        WebSiteLogoDefaultImageName = GetGlobalResourceValue("WebSiteLogoDefaultImageName");
        AllowedImageExtensions=GetGlobalResourceValue("WebSiteLogoDefaultImageName").Split(',');
        TokenSecret = GetGlobalResourceValue("TokenSecret"); 
        Vat = float.Parse(GetGlobalResourceValue("Vat"));
        MeshulamUserId = GetGlobalResourceValue("MeshulamUserId");  // קוד משתמש משולם
        MeshulamPageCode = GetGlobalResourceValue("MeshulamPageCode"); // קוד עסקת חיוב באשראי משולם
        MeshulamPaymentUrl = GetGlobalResourceValue("MeshulamPaymentUrl");
        MeshulamApproveUrl = GetGlobalResourceValue("MeshulamApproveUrl");
        MeshulamPaymentTemplate = GetGlobalResourceValue("MeshulamPaymentTemplate"); 
        SuccesUrl = GetGlobalResourceValue("SuccesUrl"); 
        CancelUrl = GetGlobalResourceValue("CancelUrl");
        PublishLink = GetGlobalResourceValue("PublishLink");       
        ExcelSuccesUrl = GetGlobalResourceValue("ExcelSuccesUrl");
        ExcelCancelUrl = GetGlobalResourceValue("ExcelCancelUrl");
        PayNowSuccesUrl = GetGlobalResourceValue("PayNowSuccesUrl");
        PayNowCancelUrl = GetGlobalResourceValue("PayNowCancelUrl");

        }
    }
}