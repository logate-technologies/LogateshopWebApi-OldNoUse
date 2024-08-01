using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Mime;
using System.Reflection;
using System.Runtime;
using Newtonsoft.Json.Schema;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
using ImageMagick;
using Newtonsoft.Json.Linq;

//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens;
//using System.IdentityModel.Tokens;


namespace LogateShopWebApi
{
    public class GlobFunc
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("yaronlapidot2023");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("arielsachar20231");
        public static string GenerateToken(string userId)
        {
            return "";
            //Settings settings = (Settings)HttpContext.Current.Application["Settings"];
            //var mySecret = settings.TokenSecret;
            //var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            //var myIssuer = "http://localhost:65049/";
            //var myAudience = "http://localhost:65049/";

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //new Claim(ClaimTypes.NameIdentifier, userId),
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    Issuer = myIssuer,
            //    Audience = myAudience,
            //    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
        }

        //public static string GetClaim(string token, string claimType)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        //    var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
        //    return stringClaimValue;
        //}
        public static void ReplaceNulls<T>(T obj)
        {
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    // Check if the property is of type string
                    string propertyValue = (string)propertyInfo.GetValue(obj);
                    if (propertyValue == null)
                    {
                        // Replace null value with an empty string
                        propertyInfo.SetValue(obj, string.Empty);
                    }
                }
                //else
                //{
                //    // If the property is nullable, check for null and replace with default value
                //    if (propertyInfo.PropertyType.IsGenericType &&
                //        propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                //    {
                //        object propertyValue = propertyInfo.GetValue(obj);
                //        if (propertyValue == null)
                //        {
                //            // Set the default value for the nullable type (e.g., null for int?, double?, etc.)
                //            propertyInfo.SetValue(obj, Activator.CreateInstance(propertyInfo.PropertyType));
                //        }
                //    }
                //}
            }

        }
       
        public static Object GetToken()
        {
          return null;
        }
        // פונקציה ליצירת מחרוזת רנדומלית באורך מבוקש
        public static string GetRandStr(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            //string st = "";
            Random rnd = new Random();
            StringBuilder Builder = new StringBuilder();
            for (int i=0;i< length;i++)
            {
                Builder.Append(chars[rnd.Next(chars.Length)]);
            }
            return Builder.ToString();
        }
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static List<string> UploadImages(FileUpload UploadObj, string ImageSavedPath)
        {
            List<string> images = new List<string>();
            FileUpload FU = UploadObj;
           // string FullPath = "";
            if (FU.HasFile)
            {
                // שמירת הנתיב המלא לתמונה
                string FilePath =HttpContext.Current.Server.MapPath(ImageSavedPath);
                Settings settings = (Settings)HttpContext.Current.Application["Settings"];
                string[] Extensions = settings.AllowedImageExtensions;
                for (int i=0;i<FU.PostedFiles.Count;i++)// מעבר על כל הקבצים שנשלחו
                {
                    string FileExtension = System.IO.Path.GetExtension(FU.FileName).ToLower();// חילוץ סיומת הקובץ
                    for (int j = 0; j < Extensions.Length; j++)
                    {
                        if (FileExtension == Extensions[j])
                        {
                            try
                            {
                                // יצירת שם רנדולמיל המורכב מהתאריך ותווים אקראיים
                                string NewFileName = DateTime.Now.Year.ToString() +
                                DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + GetRandStr(10)+ FileExtension;
                                // שמירת קובץ התמונה בדיסק, בנתיב שנשלח
                                FU.PostedFile.SaveAs(FilePath + NewFileName);                                
                                // הוספ התמונה לרשימת התמונות
                                images.Add(NewFileName);
                            }
                            catch (Exception ex)
                            {
                                
                            }

                        }

                    }
                }
                
                

            }
            return images;
        }
        public static string Encrypt(string plainText, string key)
        {
            byte[] encryptedBytes;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // Initialization Vector (IV) for AES (16 bytes)

                using (MemoryStream encryptStream = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(encryptStream, encryptor, CryptoStreamMode.Write))
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encryptedBytes = encryptStream.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // Initialization Vector (IV) for AES (16 bytes)

                using (MemoryStream decryptStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(decryptStream, decryptor, CryptoStreamMode.Read))
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string EncryptString(string plainText)
        {
            byte[] encryptedBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(plainText);
                        }
                        encryptedBytes = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }
        public static string DecryptString(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            string decryptedText;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            decryptedText = reader.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedText;
        }
        public string GetUrl(string Url)
        {
            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create(Url);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
           
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        public static bool SendEmail(string To, string ToName, string From, string Subject, string HtmlBody, List<LinkedResource> LstImages)
        {




            AlternateView avHtml = AlternateView.CreateAlternateViewFromString
           (HtmlBody, null, MediaTypeNames.Text.Html);
            if (LstImages != null)
            {
                for (int i = 0; i < LstImages.Count; i++)
                {
                    avHtml.LinkedResources.Add(LstImages[i]);
                }
            }
            MailMessage mailMsg = new MailMessage();
            mailMsg.AlternateViews.Add(avHtml);
            // To
            mailMsg.To.Add(new MailAddress(To, ToName));

            // From
            mailMsg.From = new MailAddress("info@shop.LogateBI.co.il", "לוגייט בינה עסקית");

            // Subject and multipart/alternative Body
            mailMsg.Subject = Subject;



            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("shop.LogateBI.co.il", Convert.ToInt32(25));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("info@shop.LogateBI.co.il", "Gal2023!");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);




            return true;
        }
        public static void Logger(string Path, string Msg)
        {
            using (StreamWriter sw = File.AppendText(Path))
            {
                sw.WriteLine(Msg);

            }
            //SendEmail("ylapidot@gmail.com", "yaron", "yaron@logate.co.il", "LogateBI Error", Msg, null);
        }
       
        public static bool CheckCapcha(string ClientResponse)
        {
            WebRequest request = WebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = $"secret={ConfigurationManager.AppSettings["GoogleCapcha"].ToString()}&response={ClientResponse}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                return responseFromServer.Contains("true");
            }

        }
        //public static void SendOrderEmail(Client Cl,Order Od ,HttpContext context)
        //{
        //    try
        //    {

        //        Settings settings = (Settings)context.Application["Settings"];
        //        List<LinkedResource> Lst = new List<LinkedResource>();

        //        LinkedResource inline = new LinkedResource(context.Server.MapPath("~/images/logo-600x150.png"), MediaTypeNames.Image.Jpeg);
        //        inline.ContentId = "logoemail";
        //        Lst.Add(inline);
        //        string HtmlBody = System.IO.File.ReadAllText(context.Server.MapPath("~/Content/Email/OrderTemplateEmail.html"));
        //        HtmlBody = HtmlBody.Replace("@@OrderId@@", Od.OrderId + "").Replace("@@ClientName@@", Cl.ClientName).Replace("@@OrderTotal@@", Od.OrderTotal + "").Replace("@@PublishLink@@", settings.PublishLink);
        //        string Subject = $"הזמנת פרסום / כתיבת תוכן באתר בייפוסט - LogateBI.co.il"; 
        //        string To = Cl.Email;
        //        string ToName = Cl.ClientName;               
        //        GlobFunc.SendEmail(To, ToName, "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //        GlobFunc.SendEmail("ylapidot@netvision.net.il", "Yaron", "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //        GlobFunc.SendEmail("ylapidot@gmail.com", "Yaron", "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //        GlobFunc.SendEmail("yaron@logate.co.il", "Yaron", "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //        //  GlobFunc.SendEmail("LogateBIil@gmail.com", "Logate BI", "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //    }
        //    catch (Exception ex)
        //    {
        //        GlobFunc.Logger(context.Server.MapPath("~/App_Data/LogateBI-Log.txt"), DateTime.Now.ToString() + ex.Message + ex.StackTrace);
        //    }
        //}
        //public static void SendPublishEmail(Client Cl, Publish Pu, HttpContext context)
        //{
        //    try
        //    {

        //        Settings settings = (Settings)context.Application["Settings"];
        //        List<LinkedResource> Lst = new List<LinkedResource>();

        //        LinkedResource inline = new LinkedResource(context.Server.MapPath("~/images/logo-600x150.png"), MediaTypeNames.Image.Jpeg);
        //        inline.ContentId = "logoemail";
        //        Lst.Add(inline);
        //        string HtmlBody = System.IO.File.ReadAllText(context.Server.MapPath("~/Content/Email/PublishstatusEmail.html"));
        //        PublishStatus Ps = PublishStatus.GetById(Pu.PublishStatusId);
        //        string PublishStatusDesc =$"<span style='background-color:{Ps.Color};color:#ffffff;'>{Ps.PublishStatusDesc}</span>";
        //        string WebSiteDesc = "";
        //        if (Pu.ItemTypeId == 3)
        //            WebSiteDesc = "כתיבת תוכן בלבד";
        //        else
        //            WebSiteDesc = WebSite.GetById(Pu.WebSiteId).WebSiteDesc;
        //        HtmlBody = HtmlBody.Replace("@@ItemTypeDesc@@", ItemType.GetById(Pu.ItemTypeId).ItemTypeName + "")
        //            .Replace("@@ClientName@@", Cl.ClientName).Replace("@@PublishId@@", Pu.PublishId + "")
        //            .Replace("@@PublishLink@@", settings.PublishLink)
        //            .Replace("@@WebSiteDesc@@",WebSiteDesc)
        //            .Replace("@@PublishStatus@@", PublishStatusDesc);
        //        string Subject = $"עדכון סטאטוס פרסום / כתיבת תוכן באתר בייפוסט - LogateBI.co.il";
        //        string To = Cl.Email;
        //        string ToName = Cl.ClientName;
        //        GlobFunc.SendEmail(To, ToName, "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //        GlobFunc.SendEmail("ylapidot@netvision.net.il", "Yaron", "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //        GlobFunc.SendEmail("ylapidot@gmail.com", "Yaron", "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //        GlobFunc.SendEmail("yaron@logate.co.il", "Yaron", "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //        //  GlobFunc.SendEmail("LogateBIil@gmail.com", "Logate BI", "info@shop.LogateBI.co.il", Subject, HtmlBody, Lst);
        //    }
        //    catch (Exception ex)
        //    {
        //        GlobFunc.Logger(context.Server.MapPath("~/App_Data/LogateBI-Log.txt"), DateTime.Now.ToString() + ex.Message + ex.StackTrace);
        //    }
        //}
        //public static void SendResetPassEmail(ClientUser tmp,HttpContext context)
        //{
        //    try
        //    {
        //        string ResetLink = Settings.GetGlobalResourceValue("ResetPassUrl") + "?UId=" + tmp.Token;



        //        Settings settings = (Settings)context.Application["Settings"];
        //        List<LinkedResource> Lst = new List<LinkedResource>();

        //        LinkedResource inline = new LinkedResource(context.Server.MapPath("~/images/logo-600x150.png"), MediaTypeNames.Image.Jpeg);
        //        inline.ContentId = "logoemail";
        //        Lst.Add(inline);
        //        string HtmlBody = System.IO.File.ReadAllText(context.Server.MapPath("~/Content/Email/ResetPassEmail.html"));
               
        //        HtmlBody = HtmlBody.Replace("@@ClientName@@", tmp.ClientUserFullName).Replace("@@ResetLink@@", ResetLink);
        //        string Subject = $"לוגייט בינה עסקית - איפוס סיסמת מערכת";
        //        string To = tmp.Email;
        //        string ToName = tmp.ClientUserFullName;

        //        GlobFunc.SendEmail(To, ToName, "info@shop.buy-post.co.il", Subject, HtmlBody, Lst);
        //        GlobFunc.SendEmail("ylapidot@gmail.com", "Yaron", "yaron@logate.co.il", Subject, HtmlBody, Lst);
        //    }
        //    catch (Exception ex)
        //    {
        //        GlobFunc.Logger(context.Server.MapPath("~/App_Data/LogateBI-Log.txt"), DateTime.Now.ToString() + ex.Message + ex.StackTrace);
               
        //    }
        //}
        public static string SaveProductImage(string ImageUrl)
        {
            string FullImageName = "";
            try
            {
                string BaseImagesPath = ConfigurationManager.AppSettings["baseimagespath"] + "";
               // string BaseImagesPath = ConfigurationManager.ConnectionStrings["baseimagespath"].ConnectionString + "";
                string ImageName = GetRandStr(8);
                string ImageExt=ImageUrl.Substring(ImageUrl.LastIndexOf(".") );
                FullImageName = ImageName + ImageExt;
                // Download the image
                using (WebClient client = new WebClient())
                using (Stream imageStream = client.OpenRead(ImageUrl))
                using (MagickImage image = new MagickImage(imageStream))
                {
                    // Resize and save the images
                    string[] sizes = {"0", "40", "75", "250" };
                    foreach (string size in sizes)
                    {
                        int width = int.Parse(size);
                        if(width > 0)
                        {
                            SaveImage(image, width, $"{BaseImagesPath}/s{size}/{FullImageName}");
                        }
                        else
                        {
                            SaveImage(image, width, $"{BaseImagesPath}/{FullImageName}");
                        }
                        
                    }
                }

               
            }
            catch 
            {
                FullImageName = "";
            }
            return FullImageName;
           
        }

      
        public static void SaveImage(MagickImage originalImage, int width, string ImageFullPath)
        {
            using (MagickImage resizedImage = (MagickImage)originalImage.Clone())
            {
                if(width>0)
                {
                    int height = (int)((double)width / originalImage.Width * originalImage.Height);
                    resizedImage.Resize(width, height);
                }
                
                resizedImage.Write(ImageFullPath);
            }
        }

        public static bool IsImageUrlValid(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return false;
            }

            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = System.IO.Path.GetExtension(imageUrl).ToLower();

            return Array.Exists(validExtensions, ext => ext == extension);
        }

        public static void SetPropertyValue(JObject jObject, string propertyName, object value)
        {
            // Check if the property exists
            if (jObject[propertyName] != null)
            {
                // Update the value of the existing property
                jObject[propertyName] = JToken.FromObject(value);
            }
            else
            {
                // Add a new property and set its value
                jObject.Add(new JProperty(propertyName, value));
            }
        }

    }
}