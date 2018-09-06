using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BomChange
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;


            Bind(1);
        }

        private void Bind(int page)
        {
            string where = "";
            if (!string.IsNullOrEmpty(txtCode.Text.Trim())&& !string.IsNullOrEmpty(ddlType.SelectedValue))
            {
                where += "   where  ";
            }
       
            string sql = "select top 10 * from  UseInfo where  userId  !=(select top  " + ((page - 1) * 10) + "  userId  from  UserInfo )     ";
            List<string> users = new List<string>();
            lblTotalCount.Text = users.Count.ToString();
            lblNowPage.Text = page.ToString();
            lblTotalPage.Text = (int.Parse(lblTotalCount.Text) / 10).ToString();

            Repeater1.DataSource = users;
            Repeater1.DataBind();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string aa = e.CommandArgument.ToString();


            }
            if (e.CommandName == "Del")
            {
                string aa = e.CommandArgument.ToString();

            }

        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Bind(1);
        }

        protected void btnEnd_Click(object sender, EventArgs e)
        {
            int page = int.Parse(lblTotalPage.Text);
            Bind(page);
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAhead_Click(object sender, EventArgs e)
        {

        }
    }
}