using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BomChange
{
    /// <summary>
    /// GetDatasHandler 的摘要说明
    /// </summary>
    public class GetDatasHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string result = "";
            DataSet ds = DBhelper2.GetDatasByAdapter("SELECT * FROM TEST1 ");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result += dr["Product Name"] + ",";
                }

            }
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}