﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Web.Security;

namespace BomChange
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("<script>alert('123')</script>");
        //Response.Write("<script>alert('" + Session["userName"] + "')</script> ");
        if (!IsPostBack)
        {
            if (Session["userName"] == null)
            {
                //Response.Write("<script>alert('789')</script>");
                Response.Redirect("Login1.aspx");
            }
            else
            {
                Loaddata("where ISNULL(已处理) or `已处理`=''");
            }
        }
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvw = (GridView)sender;
        if (e.NewPageIndex < 0)
        {
                TextBox pageNum = (TextBox)gvw.BottomPagerRow.FindControl("txtNewPageIndex");
            int Pa = int.Parse(pageNum.Text);
            if (Pa <= 0)
            {
                gvw.PageIndex = 0;
            }
            else
            {
                gvw.PageIndex = Pa - 1;
            }
        }
        else
        {
            gvw.PageIndex = e.NewPageIndex;
        }
        Loaddata("where ISNULL(已处理) or `已处理`=''");
        //GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from ECN1").Tables[0];
        //GridView1.DataBind();
        //你绑定数据的方法
    }
    #region 上传Excel并写入SQL
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
        {
            Response.Write("<script>alert('请您选择Excel文件')</script> ");
            return;//当无文件时,返回
        }
        string IsXls = Path.GetExtension(FileUpload1.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
        if (IsXls != ".xls" && IsXls != ".xlsx")
        {
            Response.Write("<script>alert('只可以选择Excel文件')</script>");
            return;//当选择的不是Excel文件时,返回
        }
        string filename = FileUpload1.FileName;              //获取Excel文件名  DateTime日期函数
        string savePath = Server.MapPath(("uploadfiles\\") + filename);//Server.MapPath 获得虚拟服务器相对路径
        DataTable ds = new DataTable();
        FileUpload1.SaveAs(savePath);          //SaveAs 将上传的文件内容保存在服务器上
        ds = GetExcelDatatable(savePath);           //调用自定义方法
        DataRow[] dr = ds.Select();            //定义一个DataRow数组
        int rowsnum = ds.Rows.Count;
        int successly = 0;
        int isnotnull = 0;

        //Label2.Text = getExcelOneCell(@savePath, 2, 2);
        //Label3.Text = getExcelOneCell(@savePath, 2, 9);
        //Label4.Text = getExcelOneCell(@savePath, 3, 2);
        //Label5.Text = getExcelOneCell(@savePath, 3, 3);
        //Label6.Text = getExcelOneCell(@savePath, 3, 6);
        //Label7.Text = getExcelOneCell(@savePath, 3, 7);
        //Label8.Text = getExcelOneCell(@savePath, 2, 14);



        string a18 = getExcelOneCell(@savePath, 2, 2);  //ECN号码
        string a19 = getExcelOneCell(@savePath, 2, 9);  // SO号
        string a20 = getExcelOneCell(@savePath, 3, 2); //Change 申请人
        DateTime a21 = DateTime.Parse(getExcelOneCell(@savePath, 3, 3)); // 申请日期

        //Convert.ToDateTime(getExcelOneCell(@savePath, 3, 3)).ToString("yyyy-MM-dd");
        //DateTime a21 = DateTime.ParseExact(getExcelOneCell(@savePath, 3, 3), "MMddyyyy", System.Globalization.CultureInfo.CurrentCulture);
        string a22 = getExcelOneCell(@savePath, 3, 6);  // 批准人
        DateTime a23 = DateTime.Parse(getExcelOneCell(@savePath, 3, 7));  // 批准日期
        string a24 = getExcelOneCell(@savePath, 2, 14);  // 项目名称

        //string a18 = getExcelOneCell(@savePath, 2, 2).ToString();  //ECN号码
        //string a19 = getExcelOneCell(@savePath, 2, 9).ToString();  // SO号
        //string a20 = getExcelOneCell(@savePath, 3, 2).ToString(); //Change 申请人
        //string a21 = getExcelOneCell(@savePath, 3, 3).ToString();  // 申请日期
        //string a22 = getExcelOneCell(@savePath, 3, 6).ToString();  // 批准人
        //string a23 = getExcelOneCell(@savePath, 3, 7).ToString();  // 批准日期
        //string a24 = getExcelOneCell(@savePath, 2, 14).ToString();  // 项目名称

        if (rowsnum == 0)
        {
            Response.Write("<script>alert('Excel表为空表,无数据!')</script>");   //当Excel表为空时,对用户进行提示
        }

        else
        {
            string _Result = "";
            for (int i = 0; i < dr.Length; i++)
            {

                //前面除了你需要在建立一个“upfiles”的文件夹外，其他的都不用管了，你只需要通过下面的方式获取Excel的值，然后再将这些值用你的方式去插入到数据库里面
                string a1 = dr[i]["Add or Cancel _增加或减少"].ToString();
                string a2 = dr[i]["cubical/drawer/Busway No#"].ToString();
                string a3 = dr[i]["Des#_柜子描述"].ToString();
                string a4 = dr[i]["MO No# _生产订单号"].ToString();
                string a5 = dr[i]["Material No#_物料号"].ToString();
                string a6 = dr[i]["Qty_数量"].ToString();
                string a7 = dr[i]["Operation Code _工段号"].ToString();
                string a8 = dr[i]["Attribute 物料属性"].ToString();
                string a9 = dr[i]["Bin 库位"].ToString();
                string a10 = dr[i]["Total Qty"].ToString();
                string a11 = dr[i]["Change_Reason 变更原因"].ToString();
                string a12 = dr[i]["Position 位置"].ToString();
                string a13 = dr[i]["Sub-contract or not                  是否外包"].ToString();
                string a14 = dr[i]["MO release to WS _生产订单是否下达车间"].ToString();
                string a15 = dr[i]["Change Impact 变更影响"].ToString();
                string a16 = dr[i]["Material Des#物料描述"].ToString();




                /*string a3 = Label3.Text; */  //申请人
                                               //string title = dr[i]["Add or Cancel 增加或减少"].ToString();
                                               //string a2 = dr[i]["柜子描述"].ToString();
                                               //string categoryname = dr[i]["分类"].ToString();
                                               //string customername = dr[i]["内容摘要"].ToString();
                if (a1 != "" && a1 != "Note: Add or Cancel define" && a1 != "01: Add material" && a1 != "02: Delete material")
                {
                    isnotnull++;
                    try
                    {
                        var uuid = Guid.NewGuid().ToString();
                        //string sql = string.Format("insert into ECN1(`增加或减少`,`柜号`,`柜子描述`,`MO`,`物料号`,`数量`,`工段号`,`物料属性`,`库位`,`总数量`,`变更原因`,`位置`,`是否外包`,`MO下达车间与否`,`变更影响`,`物料描述`,`ECN`,`SO`,`申请人`,`申请日期`,`批准人`,`批准日期`,`项目名称`) \n" +
                        // " values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')", a1, a2, a3,a4,a5,a6,a7,a8,a9,a10,a11,a12,a13,a14,a15,a16,Label2.Text,Label3.Text,Label4.Text,Label5.Text,Label6.Text,Label7.Text,Label8.Text);
                        //                           string sql = string.Format("insert into ECN1(`增加或减少`,`柜号`,`柜子描述`,`MO`,`物料号`,`数量`,`工段号`,`物料属性`,`库位`,`总数量`,`变更原因`,`位置`,`是否外包`,`MO下达车间与否`,`变更影响`,`物料描述`,`ECN`,`SO`,`申请人`,`申请日期`,`批准人`,`批准日期`,`项目名称`) \n" +
                        //" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')", a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a18, a19, a20, a21, a22, a23, a24);
                        //string sql = string.Format("insert into ECN1(`增加或减少`,`柜号`,`柜子描述`,`MO`,`物料号`,`数量`,`工段号`,`物料属性`,`库位`,`总数量`,`变更原因`,`位置`,`是否外包`,`MO下达车间`,`变更影响`,`物料描述`,`ECN`,`SO`,`申请人`,`申请日期`,`批准人`,`批准日期`,`项目名称`,`完成日期`) \n" +
                        //" values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')", a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a18, a19, a20, a21, a22, a23, a24, null);
                        string sql = string.Format("INSERT into ECN1(`增加或减少`,`柜号`,`柜子描述`,`MO`,`物料号`,`数量`,`工段号`,`物料属性`,`库位`,`总数量`,`变更原因`,`位置`,`是否外包`,`MO下达车间`,`变更影响`,`物料描述`,`ECN`,`SO`,`申请人`,`申请日期`,`批准人`,`批准日期`,`项目名称`,`完成时间`) \n" +
                        " values ('" + a1 + "','" + a2 + "','" + a3 + "','" + a4 + "','" + a5 + "','" + a6 + "','" + a7 + "','" + a8 + "','" + a9 + "','" + a10 + "','" + a11 + "','" + a12 + "','" + a13 + "','" + a14 + "','" + a15 + "','" + a16 + "','" + a18 + "','" + a19 + "','" + a20 + "','" + a21 + "','" + a22 + "','" + a23 + "','" + a24 + "',NULL)");
                        //string sql = string.Format("INSERT into ecn1(`处理人员`,`完成时间`) VALUES ('张三',NULL)");

                        int count = DBHelper.ExecuteNonQuery(sql);
                        if (count > 0)
                        {
                            successly++;
                        }

                    }
                    catch (Exception ex)
                    {
                        _Result = _Result + ex.InnerException + "\\n\\r";
                    }
                }

            }
            if (successly == isnotnull)
            {
                string strmsg = "Excel表导入成功!";
                System.Web.HttpContext.Current.Response.Write("<Script Language='JavaScript'>window.alert('" + strmsg + "');</script>");
            }
            else
            {
                Response.Write("<script>alert('Excel表导入失败!');</script>");
            }
        }




        GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from ECN1").Tables[0];
        GridView1.DataBind();
    }
    #endregion

    public System.Data.DataTable GetExcelDatatable(string fileUrl)
    {
        //支持.xls和.xlsx，即包括office2010等版本的   HDR=Yes代表第一行是标题，不是数据；
        string cmdText = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
        System.Data.DataTable dt = null;
        //建立连接
        OleDbConnection conn = new OleDbConnection(string.Format(cmdText, fileUrl));
        try
        {
            //打开连接
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string strSql = "select * from [BOM change form$A5:P1000]";
            OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }
        catch (Exception exc)
        {
            throw exc;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string str = getExcelOneCell(@"c:\BOM change-福晶-20180719-ECN163224X-E-SAP.xlsx", 2, 2);
        Response.Write("<script>alert('" + str + "');</script>");
    }

    ///<summary>
    /// 获取指定文件的指定单元格内容
    ///</summary>
    /// <param name="fileName">文件路径</param>
    /// <param name="row">行号</param>
    /// <param name="column">列号</param>
    /// <returns>返回单元指定单元格内容</returns>
    public string getExcelOneCell(string fileName, int row, int column)
    {
        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook wbook = app.Workbooks.Open(fileName, Type.Missing, Type.Missing,
             Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
             Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
             Type.Missing, Type.Missing);

        Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)wbook.Worksheets[1];

        string temp = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[row, column]).Text.ToString();
        //string temp = ((Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal)workSheet.Cells[row, column]).Text.ToString();

        wbook.Close(false, fileName, false);
        app.Quit();
        NAR(app);
        NAR(wbook);
        NAR(workSheet);
        return temp;

    }

    //此函数用来释放对象的相关资源
    private void NAR(Object o)
    {
        try
        {
            //使用此方法，来释放引用某些资源的基础 COM 对象。 这里的o就是要释放的对象
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
        }
        catch { }
        finally
        {
            o = null; GC.Collect();
        }
    }


    protected void Button3_Click(object sender, EventArgs e)
    {

        string filename = FileUpload1.FileName;              //获取Execle文件名  DateTime日期函数
        string savePath = Server.MapPath(("uploadfiles\\") + filename);//Server.MapPath 获得虚拟服务器相对路径
        Response.Write("<script>alert('" + savePath + "');</script>");

        //string str = getExcelOneCell(@savePath, 2, 2);
        //Response.Write("<script>alert('" + str + "');</script>");
        Label2.Text = getExcelOneCell(@savePath, 5, 1);
        Label3.Text = getExcelOneCell(@savePath, 5, 3);
        Label4.Text = getExcelOneCell(@savePath, 3, 3);
        Label5.Text = getExcelOneCell(@savePath, 3, 6);
        Label6.Text = getExcelOneCell(@savePath, 3, 7);
        Label7.Text = getExcelOneCell(@savePath, 2, 9);
        Label8.Text = getExcelOneCell(@savePath, 2, 14);

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public SqlConnection GetConnection()
    {
        string myStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
        SqlConnection myConn = new SqlConnection(myStr);
        return myConn;
    }

    protected void GridViewBind1(string where)
    {
        Loaddata1();
    }

    #region 分页代码
    /// <summary>
    /// 翻页操作
    /// 在GridView当前索引正在更改时触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

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
        //InitGridView();
        if (ViewState["ecnnum"] != null)
        {
            if ((int)ViewState["ecnnum"] == 2)
            {

                Loaddata("where `ECN`='" + ViewState["ecnstring"] + "'");
            }
            if ((int)ViewState["ecnnum"] == 3)
            {

                Loaddata("where `SO`='" + ViewState["ecnstring"] + "'");
            }
            if ((int)ViewState["ecnnum"] == 4)
            {

                Loaddata("where `MO`='" + ViewState["ecnstring"] + "'");
            }
        }
        else if (ViewState["ecnnum"] == null)
        {
            Loaddata("where ISNULL(已处理) or `已处理`=''");

        }

        //GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from ECN1 where ISNULL(已处理) or `已处理`='' ").Tables[0];
        //GridViewBind1("");
        #endregion

    }


    #endregion

    protected void Button4_Click(object sender, EventArgs e)
    {
        string UserName = Session["userName"].ToString();
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)this.GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
            if (ckb.Checked)
            {
                string huohao = this.GridView1.Rows[i].Cells[2].Text.ToString();
                string num = DBHelper.ExcuteSqlreturnInt("update ecn1 SET `已处理`='已处理',`处理人员`='" + UserName + "',`完成时间`=NOW()  where id='" + huohao + "'").ToString();
                //Response.Write("<script>alert('" + huohao + "');</script>");
                //Response.Write("<script>alert('" + huohao + "');</script>");
            }

        }

        GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from ECN1 where ISNULL(已处理)").Tables[0];
        GridView1.DataBind();

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("Handdone.aspx");
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (ViewState["ecnnum"] != null)
        {
            if ((int)ViewState["ecnnum"] == 2)
            {
                this.GridView1.EditIndex = e.NewEditIndex;
                Loaddata("where `ECN`='" + ViewState["ecnstring"] + "'");
            }
            if ((int)ViewState["ecnnum"] == 3)
            {
                this.GridView1.EditIndex = e.NewEditIndex;
                Loaddata("where `SO`='" + ViewState["ecnstring"] + "'");
            }
            if ((int)ViewState["ecnnum"] == 4)
            {
                this.GridView1.EditIndex = e.NewEditIndex;
                Loaddata("where `MO`='" + ViewState["ecnstring"] + "'");
            }
        }
        else if (ViewState["ecnnum"] == null)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridViewBind1("");

        }

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string ID = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        string str1 = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[10].Controls[0])).Text.ToString().Trim();
        int num = DBHelper.ExcuteSqlreturnInt("UPDATE ecn1 SET `库位`= '" + str1 + "' WHERE id = '" + ID + "'");

        if (num > 0)//使用Usersmr类UpdateByProc方法修改用户信息
        {

            Response.Write("<script language=javascript>alert('更新成功!');</script>");
            GridView1.EditIndex = -1;

            if (ViewState["ecnnum"] != null)
            {
                if ((int)ViewState["ecnnum"] == 2)
                {

                    Loaddata("where `ECN`='" + ViewState["ecnstring"] + "'");
                }
                if ((int)ViewState["ecnnum"] == 3)
                {

                    Loaddata("where `SO`='" + ViewState["ecnstring"] + "'");
                }
                if ((int)ViewState["ecnnum"] == 4)
                {

                    Loaddata("where `MO`='" + ViewState["ecnstring"] + "'");
                }
            }
            else if (ViewState["ecnnum"] == null)
            {
                Loaddata("where ISNULL(已处理) or `已处理`=''");

            }


            //GridViewBind1("");

        }
        //GridView1.EditIndex = -1;
        //GridViewBind();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        if (ViewState["ecnnum"] != null)
        {
            if ((int)ViewState["ecnnum"] == 2)//ECN检索
            {
                GridView1.EditIndex = -1;
                Loaddata("where `ECN`='" + ViewState["ecnstring"] + "'");
            }
            if ((int)ViewState["ecnnum"] == 3)
            {
                this.GridView1.EditIndex = -1;
                Loaddata("where `SO`='" + ViewState["ecnstring"] + "'");
            }
            if ((int)ViewState["ecnnum"] == 4)
            {
                this.GridView1.EditIndex = -1;
                Loaddata("where `MO`='" + ViewState["ecnstring"] + "'");
            }
        }

        else
        {
            GridView1.EditIndex = -1;
            GridViewBind1("");
        }
    }
    private void Loaddata(string where)
    {
        GridView1.DataSource = DBHelper.GetDatasByAdapter("SELECT\n" +
        "ecn1.id,\n" +
        "ecn1.`增加或减少`,\n" +
        "ecn1.`柜号`,\n" +
        "ecn1.`柜子描述`,\n" +
        "ecn1.MO,\n" +
        "ecn1.`物料号`,\n" +
        "ecn1.`数量`,\n" +
        "ecn1.`物料属性`,\n" +
        "ecn1.`库位`,\n" +
        "ecn1.`变更原因`,\n" +
        "ecn1.`总数量`,\n" +
        "ecn1.`MO下达车间`,\n" +
        "ecn1.`变更影响`,\n" +
        "ecn1.`物料描述`,\n" +
        "ecn1.ECN,\n" +
        "ecn1.SO,\n" +
        "ecn1.`申请人`,\n" +
        "ecn1.`申请日期`,\n" +
        "ecn1.`批准人`,\n" +
        "ecn1.`批准日期`,\n" +
        "ecn1.`项目名称`,\n" +
        "ecn1.`已处理`,\n" +
        "ecn1.`处理人员`,\n" +
        "ecn1.`完成时间`\n" +
        "FROM\n" +
        "ecn1\n" + where).Tables[0];
        //"where ISNULL(已处理) or `已处理`=''").Tables[0];

        GridView1.DataBind();

    }

    private void Loaddata1()
    {
        GridView1.DataSource = DBHelper.GetDatasByAdapter("SELECT\n" +
        "ecn1.id,\n" +
        "ecn1.`增加或减少`,\n" +
        "ecn1.`柜号`,\n" +
        "ecn1.`柜子描述`,\n" +
        "ecn1.MO,\n" +
        "ecn1.`物料号`,\n" +
        "ecn1.`数量`,\n" +
        "ecn1.`物料属性`,\n" +
        "ecn1.`库位`,\n" +
        "ecn1.`变更原因`,\n" +
        "ecn1.`总数量`,\n" +
        "ecn1.`MO下达车间`,\n" +
        "ecn1.`变更影响`,\n" +
        "ecn1.`物料描述`,\n" +
        "ecn1.ECN,\n" +
        "ecn1.SO,\n" +
        "ecn1.`申请人`,\n" +
        "ecn1.`申请日期`,\n" +
        "ecn1.`批准人`,\n" +
        "ecn1.`批准日期`,\n" +
        "ecn1.`项目名称`,\n" +
        "ecn1.`已处理`,\n" +
        "ecn1.`处理人员`,\n" +
        "ecn1.`完成时间`\n" +
        "FROM\n" +
        "ecn1\n" +
        "where ISNULL(已处理) or `已处理`=''").Tables[0];

        GridView1.DataBind();

    }

    protected void Action_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=javascript>alert('修改成功1111!');</script>");
    }
    #region ECN筛选
    protected void ActionButton_Click(object sender, EventArgs e)
    {
        string ecnstring = Request.Params["FliterTxt"].ToString().Trim();
        ViewState["ecnstring"] = ecnstring;

        if (ecnstring != "")
        {
            Loaddata("where `ECN`= '" + ecnstring + "'");
            GridView1.DataBind();
            ViewState["ecnnum"] = 2;
            //int ecnnum = 1;
            //Response.Write("<script language=javascript>alert('"+ecnnum+"');</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('请输入相应信息！');</script>");
        }

    }
    #endregion

    #region SO筛选
    protected void SO_Click(object sender, EventArgs e)
    {
        string ecnstring = Request.Params["FliterTxt"].ToString().Trim();
        ViewState["ecnstring"] = ecnstring.ToString();

        if (ecnstring != "")
        {
            Loaddata("where `SO`= '" + ecnstring + "'");
            GridView1.DataBind();
            ViewState["ecnnum"] = 3;
            //int ecnnum = 1;
            //Response.Write("<script language=javascript>alert('"+ecnnum+"');</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('请输入相应信息！');</script>");
        }
    }
    #endregion

    #region MO筛选
    protected void MO_Click(object sender, EventArgs e)
    {
        string ecnstring = Request.Params["FliterTxt"].ToString().Trim();
        ViewState["ecnstring"] = ecnstring;

        if (ecnstring != "")
        {
            Loaddata("where `MO`= '" + ecnstring + "'");
            GridView1.DataBind();
            ViewState["ecnnum"] = 4;
        }
        else
        {
            Response.Write("<script language=javascript>alert('请输入相应信息！');</script>");
        }
    }
    #endregion

    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.location.href=window.location.href;</script>");
    }
}

}