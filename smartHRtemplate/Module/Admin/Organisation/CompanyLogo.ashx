<%@ WebHandler Language="C#" Class="CompanyLogo" %>

using System;
using System.Web;

public class CompanyLogo : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        System.Data.SqlClient.SqlDataReader rdr = null;
        System.Data.SqlClient.SqlConnection conn = null;
        System.Data.SqlClient.SqlCommand selcmd = null;
        try
        {
            conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["GOSHRMConnectionString"].ConnectionString);
          selcmd = new System.Data.SqlClient.SqlCommand("select isnull(imglogo,'') imgfile, isnull(imgtype,'') imgtype from general_info", conn);
          conn.Open();
          rdr = selcmd.ExecuteReader();
          while (rdr.Read())
          {
              context.Response.ContentType = Convert.ToString(rdr["imgtype"]);
              context.Response.BinaryWrite((byte[])rdr["imgfile"]);
          }
          if (rdr != null)
            rdr.Close();
        }
        finally
        {
          if (conn != null)
              conn.Close();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}