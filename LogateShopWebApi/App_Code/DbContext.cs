using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Reflection;
using System.Data.OleDb;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace DATA
{
    public class DbContext
    {
        public string ConnStr { get; set; }
        
        public OleDbConnection Conn { get; set; }
        public DbContext(string ConnnStrToken) {
            ConnStr = ConfigurationManager.ConnectionStrings[ConnnStrToken].ToString();          
            Conn = new OleDbConnection(ConnStr);
            Open();
        }
        public DbContext()
        {
            string ConnStrName = "lev";            
            ConnStr = ConfigurationManager.ConnectionStrings[ConnStrName].ToString();
            Conn = new OleDbConnection(ConnStr);
            Open();
        }
        public void Open()
        {
            Conn.Open();
        }
        public void Close()
        {
            Conn.Close();
        }
        public DataTable Execute(string Sql)
        {
            DataTable Dt = new DataTable();
            OleDbCommand Cmd = new OleDbCommand(Sql, Conn);

            OleDbDataAdapter Da = new OleDbDataAdapter(Cmd);
            Da.Fill(Dt);
            Cmd.Dispose();
            //  Close();
            return Dt;
        }
        public DataTable ExecuteWithParams(string Sql, List<OleDbParameter> Params)
        {
            DataTable Dt = new DataTable();
            OleDbCommand Cmd = new OleDbCommand(Sql, Conn);
            for (int i = 0; i < Params.Count; i++)
            {
                Cmd.Parameters.Add(Params[i]);
            }
            OleDbDataAdapter Da = new OleDbDataAdapter(Cmd);
            Da.Fill(Dt);
            Cmd.Dispose();
            //  Close();
            return Dt;
        }

        public int ExecuteNonQuery(string Sql)
        {
            int RecCount = 0;
            OleDbCommand Cmd = new OleDbCommand(Sql, Conn);
            RecCount = Cmd.ExecuteNonQuery();
            Cmd.Dispose();
            //Close();
            return RecCount;
        }
        public int ExecuteNonQueryWithParams(string Sql, List<OleDbParameter> Params)
        {
            int RecCount = 0;
            OleDbCommand Cmd = new OleDbCommand(Sql, Conn);
            for (int i = 0; i < Params.Count; i++)
            {
                Cmd.Parameters.Add(Params[i]);
            }
            RecCount = Cmd.ExecuteNonQuery();
            Cmd.Dispose();
            //  Close();
            return RecCount;
        }

        public int GetMaxId(string TableName, string PrimaryKeyName)
        {
            int MaxId = -1;
            string Sql = $"SELECT MAX( {PrimaryKeyName }) FROM {TableName} ";
            OleDbCommand Cmd = new OleDbCommand(Sql, Conn);
            MaxId = int.Parse(Cmd.ExecuteScalar()+"");
            Cmd.Dispose();
            //   Close();
            return MaxId;
        }
        public string GetValueByKey(string TableName, string KeyName,string ValueName,string KeyValue)
        {
            string RetValue=null;
            string Sql = $"SELECT top 1 {ValueName} FROM {TableName} where {KeyName}='{KeyValue}'  ";
            OleDbCommand Cmd = new OleDbCommand(Sql, Conn);
            RetValue = (string) (Cmd.ExecuteScalar() +"");
            Cmd.Dispose();
            //   Close();
            return RetValue;
        }
       
        public static List<OleDbParameter> CreateParameters(object parametersObject)
        {
            var parameters = new List<OleDbParameter>();

            if (parametersObject is ExpandoObject)
            {
                var dictionary = (IDictionary<string, object>)parametersObject;
                foreach (var kvp in dictionary)
                {
                    parameters.Add(new OleDbParameter($"@{kvp.Key}", kvp.Value ?? DBNull.Value));
                }
            }
            else
            {
                foreach (PropertyInfo property in parametersObject
                    .GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    // Ensure there are no indexed properties causing the TargetParameterCountException
                    if (property.GetIndexParameters().Length == 0)
                    {
                        parameters.Add(new OleDbParameter($"@{property.Name}", property.GetValue(parametersObject) ?? DBNull.Value));
                    }
                }
            }

            return parameters;
        }
        public static List<OleDbParameter> CreateDynamicParameters(JObject data)
        {
            var parameters = new List<OleDbParameter>();

            foreach (var property in data.Properties())
            {
                var fieldName = property.Name;
                var fieldValue = property.Value.ToString();

                // Save field name and value in the dictionary (or any other storage mechanism)
                parameters.Add(new OleDbParameter($"@{fieldName}", fieldValue));
            }
           
            return parameters;
        }

        public static string CreateSqlStatementWithParameters(List<OleDbParameter> LstParams, int StringType)
        {
            string Sql = "";
            string St1 = "", St2 = "";

            for(int i = 0; i < LstParams.Count; i++)
            {
                if (StringType == 1) // Insert statement
                {
                    St1 += LstParams[i].ParameterName.Replace("@","") + ",";
                    St2 +=  LstParams[i].ParameterName + ",";

                }
                else // Update statement
                {
                    St1 += LstParams[i].ParameterName.Replace("@", "") + "=" + LstParams[i].ParameterName + ",";

                }
            }
            if (StringType == 1) // Insert statement
            {
                St1 = St1.Substring(0, St1.Length - 1);
                St2 = St2.Substring(0, St2.Length - 1);
                Sql = $"({St1}) values({St2})";

            }
            else // Update statement
            {
                St1 = St1.Substring(0, St1.Length - 1);
                Sql = St1;


            }




            return Sql;
        }
        public static void AddDefaultMissingFields(List<OleDbParameter> LstParams,object Tmp)
        {

           
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



