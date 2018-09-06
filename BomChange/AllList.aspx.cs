using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BomChange
{
    public partial class AllList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from alllist").Tables[0];
                GridView1.DataBind();
            }
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
            GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from alllist").Tables[0];
            GridView1.DataBind();
            #endregion

        }
    }
}