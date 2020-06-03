<%@ WebHandler Language="C#" Class="AssessImage" %>

using System;
using System.Web;

public class AssessImage : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        System.Data.SqlClient.SqlDataReader rdr = null;
        System.Data.SqlClient.SqlConnection conn = null;
        System.Data.SqlClient.SqlCommand selcmd = null;
        try
        {
            conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["GOSHRMConnectionString"].ConnectionString);
            selcmd = new System.Data.SqlClient.SqlCommand("if not exists (select * from Emp_Training_Learning_Assessment where id=" + context.Request.QueryString["imgid"] + ") begin select isnull(imgfile,'') imgfile, isnull(imgtype,'') imgtype from training_test_questions where id=" + context.Request.QueryString["imgid"] + " end else begin select isnull(imgfile,'') imgfile, isnull(imgtype,'') imgtype from Emp_Training_Learning_Assessment where id=" + context.Request.QueryString["imgid"] + " end", conn);
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