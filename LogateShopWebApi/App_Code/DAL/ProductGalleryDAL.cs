using DATA;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class ProductGalleryDAL
    {
        public static List<object> Get(string Query, int ClientId, string Token)
        {
            List<object> lst = new List<object>();
            var parameters = DbContext.CreateParameters(new { Query = "%" + Query + "%", ClientId = ClientId });
            DbContext Db = new DbContext();
            string Sql = "SELECT * FROM t_Products_Gallery Where 1=1";
            if (ClientId > 0)
                Sql += " And ClientId=@ClientId";
            if (Query != null && Query != "")
            {
                Sql += $" And  ProductPic like @Query ";
            }

            DataTable Dt = Db.ExecuteWithParams(Sql, parameters);
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                lst.Add(new
                {
                    PicId = int.Parse(Dt.Rows[i]["PicId"] + ""),
                    ProdId = int.Parse(Dt.Rows[i]["ProdId"] + ""),
                    ProductPic = Dt.Rows[i]["ProductPic"] + ""
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
                var tmp = new
                {
                    PicId = int.Parse(Dt.Rows[0]["PicId"] + ""),
                    ProdId = int.Parse(Dt.Rows[0]["ProdId"] + ""),
                    ProductPic = Dt.Rows[0]["ProductPic"] + ""
                };
                return tmp;
            }
            return null;
        }
        public static int Save(dynamic Tmp, int Id, string Token)
        {

            var parameters = DbContext.CreateParameters(Tmp);
            string Sql = null;
            DbContext Db = new DbContext();

            if (Id == -1)
            {
                Sql = "INSERT INTO T_Products_Gallery ";

                Sql += DbContext.CreateSqlStatementWithParameters(parameters, 1);

            }
            else
            {
                Sql = $"UPDATE T_Products_Gallery set ";
                Sql += DbContext.CreateSqlStatementWithParameters(parameters, 2);
                Sql += $" Where PicId={Id}";
            }

            int TotalRec = Db.ExecuteNonQueryWithParams(Sql, parameters);
            if (Id == -1)
            {
                Id = Db.GetMaxId("T_Products_Gallery", "PicId");

            }
            Db.Close();
            return Id;// החזרת קוד תמונת המוצר
        }


        public static int RemoveById(int Id, int ClientId, string Token)
        {
            var parameters = DbContext.CreateParameters(new { Id = Id, ClientId = ClientId });
            string Sql = $" Delete FROM T_Products_Gallery Where PicId=@Id ";
            if (ClientId > 0)
                Sql += " And ClientId=@ClientId";
            DbContext Db = new DbContext();
            int TotalRec = Db.ExecuteNonQueryWithParams(Sql, parameters);
            Db.Close();
            return TotalRec;
        }
    }
}