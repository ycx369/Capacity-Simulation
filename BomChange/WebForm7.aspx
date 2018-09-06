<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="BomChange.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
<div class="col-lg-7">
        <div class="input-group">
            <div  style="font-size:xx-large; color:#33cc33">
           SEEE 库房Material Change 表单查看 
                </div>
            <div style="width:150px" >
            <input type="text" name="FliterTxt" class="form-control" style="margin-left:140px;margin-top:8px;width:150px"    aria-label="...">
                </div>
            <div>     
                <div class="input-group-btn"; style="margin-top:6px">
                <button type="button"  name="btntype"  class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="margin-left:150px">筛选 <span class="caret"></span></button>
                <asp:Button ID="Button6" runat="server" CssClass="btn btn-default" Text="返回" OnClick="Button7_Click" />
                <ul class="dropdown-menu dropdown-menu-right">
                    <li><a href="#"  runat="server"   onserverclick="ActionButton_Click" name="ActionTxt" >ECN</a></li>
                    <li><a href="#" runat="server" onserverclick="SO_Click" name="SObtn">SO</a></li>
                    <li><a href="#" runat="server" onserverclick="MO_Click" name="MObtn">MO</a></li>
                </ul>
            </div>

            </div>
      
            <!-- /btn-group -->
        </div>
        <!-- /input-group -->
    </div>
    <!-- /.col-lg-6 -->





    <div style="margin-left: 10px;text-align:center">
        <asp:GridView ID="GridView1" runat="server"  RowStyle-Wrap="false" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" CssClass="align-centent-center"    AlternatingRowStyle-Wrap="false" CellPadding="4" FooterStyle-Font-Size="XX-Large" ForeColor="#333333" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                    </ItemTemplate>
                    <HeaderStyle Wrap="True" />
                    <ItemStyle Wrap="True" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="操作" ShowEditButton="True" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#27A844" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#27A844" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#27A844" ForeColor="White" HorizontalAlign="Center" />
            <%-- </PagerSettings> --%>
            <RowStyle BackColor="#ccff99" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />




            <PagerTemplate>
                当前第:
                                     <%--//((GridView)Container.NamingContainer)就是为了得到当前的控件--%>
                <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                页/共:
                                    <%--//得到分页页面的总数--%>
                <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                页
                                    <%--//如果该分页是首分页，那么该连接就不会显示了.同时对应了自带识别的命令参数CommandArgument--%>
                <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                    Visible='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>首页</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                    CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>上一页</asp:LinkButton>
                <%--//如果该分页是尾页，那么该连接就不会显示了--%>
                <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                    Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>下一页</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                    Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>尾页</asp:LinkButton>
                转到第
                                    <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />页
                                    <%--//这里将CommandArgument即使点击该按钮e.newIndex 值为3 --%>
                <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                    CommandName="Page" Text="GO" />
            </PagerTemplate>



        </asp:GridView>
    </div>
    <br />
    <div style="text-align: left; margin-left: 10px">
        <asp:Button ID="Button4" runat="server" CssClass="btn btn-warning" Text="确认已处理" OnClick="Button4_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button5" runat="server" CssClass="btn btn-info" Text="查看已处理" OnClick="Button5_Click" />
        <div class="btn-group" style="margin-left: -30px">
            <button type="button" class="btn btn-default" style="text-align: center">统计</button>
            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <span class="caret"></span>
                <span class="sr-only">Toggle Dropdown</span>
            </button>
            <ul class="dropdown-menu" style="align-content: center">
                <li><a href="#">项目ECN统计</a></li>
                <li><a href="WebForm5.aspx">人员ECN统计(Done)</a></li>
                <li><a href="#">时间段ECN统计</a></li>
            </ul>
        </div>
        <br />
        <br />
       
        <asp:FileUpload ID="FileUpload1" runat="server" BorderStyle="Solid" BorderWidth="1" BorderColor="#0066ff" Width="363px" />
        <asp:Button ID="Button1" runat="server" Text="上传"  CssClass="btn btn-info"  OnClick="Button1_Click" style="width:72px;margin-top:-6px"/>
        
    </div>


    <asp:HiddenField ID="HiddenField1" runat="server" />

    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>

    <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>

    <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>

    <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>

    <asp:Label ID="Label6" runat="server" Text="Label" Visible="False"></asp:Label>

    <asp:Label ID="Label7" runat="server" Text="Label" Visible="False"></asp:Label>

    <asp:Label ID="Label8" runat="server" Text="Label" Visible="False"></asp:Label>

    <asp:Label ID="Label9" runat="server" Text="Label" Visible="False"></asp:Label>


    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" Visible="False" />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" Visible="False" />


    <hr />
    <%--            <footer style="text-align:center;color:white;margin:0 auto;margin-bottom:20px" > 
               @ CopyRight XI'AN SEEE  2018 version Beta 1.00
            </footer> --%>
    <%--    </form>--%>
    <p>
        <asp:Label ID="Label10" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>
</asp:Content>
