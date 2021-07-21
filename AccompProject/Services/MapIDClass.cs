using AccompProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AccompProject.Services
{
    public class MapIDClass
    {
     
        
        protected internal static string constr = @"Server=172.16.4.91\EATER,49823;Database=isds;User Id=niauser;Password=123456;";

        internal static string GetOldID(string NewID)
        {
            string ret = "";
            SqlConnection cn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("GetOldID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NewID", NewID);
            
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    ret = dr["OldID"].ToString();
                }
                else
                    ret = "";
                cn.Close();
            }
            catch (Exception ex)
            {
                // ret = ex.Message
                ret = "";
            }
            finally
            {
                cn.Close();
            }
            return ret;
        }

        internal static int InsertUpdateMapId(string NewID, string OldID)
        {
            int ret = -2;
           SqlConnection cn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("InsertUpdateMapID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NewID", NewID);
            cmd.Parameters.AddWithValue("@OldID", OldID);
            try
            {
                cn.Open();
                ret = cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                ret = ex.HResult;
            }
            finally
            {
                cn.Close();
            }
            return ret;
        }


        internal static string GetUserInfo(HttpRequestBase request)
        {
            string ret = "";
            // http://eater-pc.nia.gov.ph:50661/user/oldid
            using (HttpClient client = new HttpClient())
            {
                // Add Session token
                for (var i = 0; i <= request.Headers.Count - 1; i++)
                {
                    var name = request.Headers.GetKey(i);
                    var value = request.Headers[i];
                    client.DefaultRequestHeaders.Add(name, value);
                }
                //client.BaseAddress = new Uri("http://eater-pc.nia.gov.ph:50661");
                client.BaseAddress = new Uri("https://accounts.nia.gov.ph");
                // Add Media Type
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("user/oldid").Result;
                if (response.IsSuccessStatusCode)
                    ret = response.Content.ReadAsStringAsync().Result;
            }
            return ret;
        }


        internal static string GetNewID(string OldID)
        {
            string ret = "";
            SqlConnection cn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("GetNewID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OldID", OldID);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    ret = dr["NewID"].ToString();
                }
                else
                    ret = "";
                cn.Close();
            }
            catch (Exception ex)
            {
                // ret = ex.Message
                ret = "";
            }
            finally
            {
                cn.Close();
            }
            return ret;
        }

        internal static int GetMapIdCount()
        {
            int ret = -2;
            SqlConnection cn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("SELECT [dbo].[GetMapIdCount] () AS 'count'", cn);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    ret = (int)dr["count"];
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                ret = ex.HResult;
            }
            finally
            {
                cn.Close();
            }
            return ret;
        }

        // Check is NewID is actually in the table assigned by table id_new_mix
        internal static bool GetEmpInfoNewIDAssignedExist(string NewID)
        {
            bool ret = false;
            SqlConnection cn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("GetEmpInfoNewIDAssignedExist", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NewID", NewID);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ret = dr.HasRows;
                cn.Close();
            }
            catch (Exception ex)
            {
                // ret = ex.Message
                ret = false;
            }
            finally
            {
                cn.Close();
            }
            return ret;
        }


     

    }
}