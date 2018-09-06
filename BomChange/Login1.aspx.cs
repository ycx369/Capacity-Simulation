using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BomChange
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        static public string auth;
        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtaccount.Text == "")
                {
                    Response.Write("<script>alert('用户名不能为空');</script>");

                }
                else
                {
                    if (txtpassword.Text == "")
                    {
                        Response.Write("<script>alert('密码不能为空');</script>");
                    }
                    else
                    {
                        //登录是一定要连接数据库验证的，所以要建立sql数据库，建立登录信息的表admin_login，vs2010自带的sql数据库足够我们用，所以用起来也比较方便。
                     
                        int i = Convert.ToInt32(DBHelper1.ExecuteScalar("select count(*) from personalinfo where `姓名`='" + txtaccount.Text.Trim() + "' and  `密码` ='" + txtpassword.Text.Trim() + "'"));
          
                        if (i > 0)
                        {
                            Response.Write("<script>alert('登陆成功');</script>");
                            Session["userName"] = txtaccount.Text;
                            Response.Cookies["user"].Value = txtaccount.Text;
                            Response.Cookies["password"].Value = Password.Text;  //将值写入到客户端硬盘Cookie
                            Response.Cookies["user"].Expires = DateTime.Now.AddMinutes(60);
                            Response.Cookies["password"].Expires = DateTime.Now.AddMinutes(60);//设置Cookie过期时间
                                                                                               //auth =DBHelper1.ExecuteScalar("select Standardsystem from employee where name='" + UserName.Text.Trim() + "'").ToString();
                                                                                               //Response.Redirect("BomChange1.aspx");
                            Session.Timeout = 60;
                            //Response.Write("<script>alert('"+Session["userName"]+"');</script>");
                            Response.Redirect("BomChange1.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('用户名或者密码错误');</script>");

                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('用户名或者密码错误');</script>");
            }
            finally
            { }
        }
    }
}