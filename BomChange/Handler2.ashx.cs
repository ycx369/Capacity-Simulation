using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.Common;
using MySql.Data.MySqlClient;

namespace BomChange
{
    /// <summary>
    /// Handler2 的摘要说明
    /// </summary>
    public class Handler2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

       
       

            //DataTable db = DBhelper2.GetDatasByAdapter("select * from test2").Tables[0];    
            MySqlDataReader sr1 = DBHelper.GetDatasByReader("select count(*) as 数量,`申请人` from ecn1 group by `申请人` ");
            List<users> list = new List<users>();
            while (sr1.Read())
            {
                users u = new users();
                u.id = sr1["数量"].ToString();
                u.Product_Name = sr1["申请人"].ToString();
               //u.p2 = sr["p2"].ToString();
                list.Add(u);
            }
            context.Response.ContentType = "text/plain";
            string jason = JsonConvert.SerializeObject(list);
            context.Response.Write(jason);

            //DataSet dt = DBhelper2.GetDatasByAdapter("select * from test2");
            //List<users> list = new List<users>();
            //while (sr.Read())
            //{
            //    users u = new users();
            //    u.id = sr["ID"].ToString();
            //    u.Product_Name = sr["Product_Name"].ToString();
            //    u.p2 = sr["p2"].ToString();
            //    list.Add(u);
            //}
            //string jason = JsonConvert.SerializeObject(list);





            //DataSet ds = DBhelper2.GetDatasByAdapter("SELECT * FROM TEST1 ");
            ////ArrayList Testname = new ArrayList();
            //List<Testname> TestnameList = new List<Testname>();
            //var query = from s in db
            //            select new
            //            {
            //                Product_Name = s.Product_Name,
            //                p2 = s.p2,
            //            };
            //foreach (DataRow row in ds.ReadXml)
            //foreach (var item in query)
            //{
            //    Testname testname = new Testname
            //    {
            //        Product_Name = item.Product_Name,
            //        p2 = item.p2,
            //    };
            //    Testnamelist.Add(testname);
            //}
            //lbMsg.InnerText = JsonConvert.SerializeObject(Testnamelist);



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