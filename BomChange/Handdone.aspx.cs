using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BomChange
{
    public partial class Handdone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from handdone").Tables[0];
                GridView1.DataBind();
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
    
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox ckb = (CheckBox)this.GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    string huohao = this.GridView1.Rows[i].Cells[1].Text.ToString();
                    string num = DBHelper.ExcuteSqlreturnInt("update ecn1 SET `已处理`='',`处理人员`='',`完成时间`=Null  where id='" + huohao + "'").ToString();
                    //Response.Write("<script>alert('" + huohao + "');</script>");
                    //Response.Write("<script>alert('" + huohao + "');</script>");
                }

            }
            GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from ecn1").Tables[0];

            GridView1.DataBind();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("BomChange1.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


            #region 方法2

            //GridView1.PageIndex = e.NewPageIndex;
            //InitGridView();

            #endregion

            #region 方法3


            // 得到该控件
            GridView theGrid = sender as GridView;
            int newPageIndex = 0;
            if (e.NewPageIndex == -3)
            {
                //点击了Go按钮
                TextBox txtNewPageIndex = null;

                //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
                GridViewRow pagerRow = theGrid.BottomPagerRow;

                if (pagerRow != null)
                {
                    //得到text控件
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (txtNewPageIndex != null)
                {
                    //得到索引
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
                }
            }
            else
            {
                //点击了其他的按钮
                newPageIndex = e.NewPageIndex;
            }
            //防止新索引溢出
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

            //得到新的值
            theGrid.PageIndex = newPageIndex;

            //重新绑定
            GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from handdone").Tables[0];
            GridView1.DataBind();
            #endregion

        }
    }
}