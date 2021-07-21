using AccompProject.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccompProject.Models
{
    public class PolygonCode : NewIA
    {

        private IAEntities db = new IAEntities();

        public int Add(Polygon temp)
        {

            int i;
            var idsssss = temp.IDgeo;
            string Constring = "";

            if (temp.tempdata == "dam")
            {
                Constring = System.Configuration.ConfigurationManager.ConnectionStrings["DamConnection"].ConnectionString;
            }

            if (temp.tempdata == "inventory")
            {
                Constring = System.Configuration.ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            }

            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand();
                //SqlCommand cmd = new SqlCommand("insert into mappic set latitude = '" + lat + "'" + ", longitude = '" + lng + "'" + " where idtable = " + id, CON);
                cmd.CommandText = "Insert into mappingdata (drawingtype, geometry,farmerlot, farmername,[id], iddam,overlayid,polygonarea) values (@drawingtype, @geometry,@farmerlot, @farmername,@id, @iddam,@overlayid,@polygonarea)";

                cmd.Connection = CON;


                cmd.Parameters.AddWithValue("@drawingtype", temp.DrawingType);
                cmd.Parameters.AddWithValue("@geometry", temp.geometry);
                cmd.Parameters.AddWithValue("@farmerlot", temp.Farmerlot);
                cmd.Parameters.AddWithValue("@farmername", temp.Farmername);
                cmd.Parameters.AddWithValue("@id", temp.IDgeo);
                cmd.Parameters.AddWithValue("@iddam", temp.idsystem);
                cmd.Parameters.AddWithValue("@overlayid", temp.overlayid);
                cmd.Parameters.AddWithValue("@polygonarea", temp.PolygonArea);


                i = cmd.ExecuteNonQuery();


            }


            return i;

        }


        #region IA

        public int AddPolygonIA(Polygon temp)
        {
            int i = 1;
            var MappingDataIA = new MappingDataSystemIA()
            {
               drawingtype = temp.DrawingType,
               year_covered = YearReference(),
               farmerlot = temp.Farmerlot,
               farmername = temp.Farmername,
               geometry = temp.geometry,
              overlayid = temp.overlayid,
              polygonarea = temp.PolygonArea,
              ProfileID = temp.profileid
              

            };

            db.MappingDataSystemIA.Add(MappingDataIA);
            db.SaveChanges();

            return i;

        }

        public int DeleteIA(int? polygonid) 
        {
            int i=0;

            MappingDataSystemIA mappoly = db.MappingDataSystemIA.Find(polygonid);
            db.MappingDataSystemIA.Remove(mappoly);
            db.SaveChanges();



            return i;
        }


        #endregion

        public int AddPromotional(PolygonPromotional temp)
        {

            int i;
            var idsssss = temp.idsystem;
            string Constring = "";





            Constring = System.Configuration.ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;





            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("select * from mappromotional where idsystem = '" + temp.idsystem + "'", CON);

                string str = Convert.ToString(cmd.ExecuteScalar());
                if (string.IsNullOrEmpty(str))
                {
                    str = "Yes";
                }
                else
                {
                    str = "Yes";

                }


                if (str == "Yes")
                {

                    cmd.CommandText = "Insert into mappromotional (idsystem, geometry,drawingtype, polygonsize,newarea, restorearea,canallining,roads,COCONET, GRAVEL,HDPE,STRUCTURES,CANAL) values (@idsystem, @geometry,@drawingtype, @polygonsize,@newarea, @restorearea,@canallining,@roads,@COCONET, @GRAVEL,@HDPE,@STRUCTURES,@CANAL)";

                }
                if (str == "No")
                {

                    cmd.CommandText = "update mappromotional set idsystem = @idsystem, geometry = @geometry, drawingtype = @drawingtype ,polygonsize = @polygonsize,newarea=@newarea,restorearea=@restorearea,canallining=@canallining,roads=@roads,COCONET=@COCONET, GRAVEL=@GRAVEL,HDPE=@HDPE,STRUCTURES=@STRUCTURES,CANAL=@CANAL where idsystem = @idsystem";
                }

                //SqlCommand cmd = new SqlCommand();
                //SqlCommand cmd = new SqlCommand("insert into mappic set latitude = '" + lat + "'" + ", longitude = '" + lng + "'" + " where idtable = " + id, CON);

                cmd.Connection = CON;


                cmd.Parameters.AddWithValue("@idsystem", temp.idsystem);
                cmd.Parameters.AddWithValue("@geometry", temp.geometry);
                cmd.Parameters.AddWithValue("@drawingtype", temp.drawingtype);
                cmd.Parameters.AddWithValue("@polygonsize", temp.polygonsize);
                cmd.Parameters.AddWithValue("@newarea", temp.newarea);
                cmd.Parameters.AddWithValue("@restorearea", temp.restorearea);
                cmd.Parameters.AddWithValue("@canallining", temp.canallining);
                cmd.Parameters.AddWithValue("@roads", temp.roads);



                cmd.Parameters.AddWithValue("@COCONET", temp.coconet);
                cmd.Parameters.AddWithValue("@GRAVEL", temp.GRAVEL);
                cmd.Parameters.AddWithValue("@HDPE", temp.HDPE);
                cmd.Parameters.AddWithValue("@STRUCTURES", temp.STRUCTURES);
                cmd.Parameters.AddWithValue("@CANAL", temp.CANAL);


                i = cmd.ExecuteNonQuery();


            }


            return i;

        }

        public int Delete(string ID, string tempdata)
        {

            int i;

            string Constring = "";

            if (tempdata == "dam")
            {
                Constring = System.Configuration.ConfigurationManager.ConnectionStrings["DamConnection"].ConnectionString;
            }

            if (tempdata == "inventory")
            {
                Constring = System.Configuration.ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            }

            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand();
                //SqlCommand cmd = new SqlCommand("insert into mappic set latitude = '" + lat + "'" + ", longitude = '" + lng + "'" + " where idtable = " + id, CON);
                //cmd.CommandText = "delete from polygon where geometry like %@geometry%";

                cmd.CommandText = "delete from mappingdata where id = @id";

                cmd.Connection = CON;


                cmd.Parameters.AddWithValue("@id", ID);


                i = cmd.ExecuteNonQuery();


            }

            return i;

        }





    }
}