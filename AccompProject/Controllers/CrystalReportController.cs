using AccompProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AccompProject.Controllers
{
    public class CrystalReportController : Controller
    {
        //
        // GET: /CrystalReport/
        public ActionResult CrystalReport()
        {
            return View();
        }

        public void GenerateCrystalReport() {

            ReportParams<ACCOMPLISHMENT> objReportParams = new ReportParams<ACCOMPLISHMENT>();
            objReportParams.DataSource = GetAccomplishment();
            objReportParams.RptFileName = "CrystalReport.rpt";
            this.HttpContext.Session["ReportType"] = "CrystalReport";
            this.HttpContext.Session["ReportParam"] = objReportParams;

        
        
        
        }

        public List<ACCOMPLISHMENT> GetAccomplishment() {

            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable dt = new DataTable();
            string sql = "Select * from sampleuser ";
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            var list = ConvertDataTableToList<ACCOMPLISHMENT>(dt);
            return list;
        
        }


        private static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            
            }
            return data;
        
        }

        private static T GetItem<T>(DataRow dr) {

            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())

                {
                    if (pro.Name == column.ColumnName)
                        
                  //  pro.SetValue(obj, dr[column.ColumnName].ToString(), null);
                        SetValue(obj,pro.Name,dr[column.ColumnName].ToString());

                      // pro.SetValue(obj, dr[column.ColumnName] == DBNull.Value ? Convert.ToDouble(0) : dr[column.ColumnName].ToString(), null);
                    else
                        continue;
                
                
                }
            
            
            }

            return obj;
        }

        public static void SetValue(object inputObject, string propertyName, object propertyVal)
        {
            //find out the type
            Type type = inputObject.GetType();

            //get the property information based on the type
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

            //find the property type
            Type propertyType = propertyInfo.PropertyType;

            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;


            if(targetType.Name == "String"){

                propertyVal = propertyVal;
            }
            else
            {
                         
                if(Convert.ToDouble(propertyVal.ToString()) > 0 ){

                    propertyVal = propertyVal;
                }else{
                
                
                propertyVal = 0;
                }
            }
            //Returns an System.Object with the specified System.Type and whose value is
            //equivalent to the specified object.
            propertyVal = Convert.ChangeType(propertyVal, targetType);
            
            //Set the value of the property
            propertyInfo.SetValue(inputObject, propertyVal, null);

        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }




	}
}