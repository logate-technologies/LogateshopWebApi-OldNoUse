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

namespace DAL
{
    public class CategoryDAL
    {
        public static List<object> Get(string Query, int ClientId, string Token)
        {
            List<object> lst = new List<object>();
            var parameters = DbContext.CreateParameters(new { Query = "%" + Query + "%", ClientId = ClientId });
            DbContext Db = new DbContext();
            string Sql = "SELECT * FROM T_Categories Where 1=1";
            if (ClientId > 0)
                Sql += " And ClientId=@ClientId";
            if (Query != null)
            {
                Sql += $" And  CatName like @Query ";
            }
            DataTable Dt = Db.ExecuteWithParams(Sql, parameters);
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                lst.Add(new 
                {
                    
                    CatId = int.Parse(Dt.Rows[i]["CatId"] + ""),                    
                    CatName = Dt.Rows[i]["CatName"] + "",
                    CatParentId =int.Parse( Dt.Rows[i]["CatParentId"] + ""),                   
                    CatStatus = int.Parse(Dt.Rows[i]["CatStatus"] + "")                  

                });

            }
            Db.Close();
            return lst;
        }
        public static object GetById(int Id, int ClientId, string Token)
        {
            
           // Category tmp = null;
            var parameters = DbContext.CreateParameters(new { Id = Id, ClientId = ClientId });
           // string ConnStr = "vbngtvdfv4567g5ghvcftg54gv5hg5";
            DbContext Db = new DbContext();
            string Sql = $"SELECT * FROM T_Categories Where CatId=@Id ";            
            DataTable Dt = Db.ExecuteWithParams(Sql, parameters);
            if (Dt.Rows.Count > 0)
            {
                var tmp = new
                {
                    CatId = int.Parse(Dt.Rows[0]["CatId"] + ""),
                    CatName = Dt.Rows[0]["CatName"] + "",
                    CatParentId = int.Parse(Dt.Rows[0]["CatParentId"] + ""),
                    CatStatus = int.Parse(Dt.Rows[0]["CatStatus"] + "")

                };
                return tmp;
            }
            return null;
        }
       
        public static int Save(JObject Tmp, int Id, string Token)
        {

            var parameters = DbContext.CreateDynamicParameters(Tmp);
            string Sql = null;
            DbContext Db = new DbContext();


            if (Id == -1)
            {
                Sql = "INSERT INTO T_Categories ";
                Id = (int)Db.GetMaxId("T_Categories", "CatId");
                Id++;
                parameters.Add(new OleDbParameter("CatId", Id));
                var Fields = new
                { // רשימת שדות מלאה עם ערכי ברירת מחדל

                    //CatId = -1,
                    CatName = "ללא שם",
                    CatParentId = 0,
                    CatStatus = 1

                };
                DbContext.AddDefaultMissingFields(parameters, Fields);
                Sql += DbContext.CreateSqlStatementWithParameters(parameters, 1);

            }
            else
            {
                Sql = $"UPDATE T_Categories set ";
                Sql += DbContext.CreateSqlStatementWithParameters(parameters, 2);
                Sql += $" Where CatId={Id}";
            }

            int TotalRec = Db.ExecuteNonQueryWithParams(Sql, parameters);
            Db.Close();
            return Id;// החזרת קוד קטגוריה
        }


        public static int RemoveById(int Id, int ClientId, string Token)
        {
            var parameters = DbContext.CreateParameters(new { Id = Id, ClientId = ClientId });
            string Sql = $" Delete FROM T_Categories Where CatId=@Id ";
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

                //CatId = -1,
                CatName = "ללא שם",
                CatParentId=0,
                CatStatus=1

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