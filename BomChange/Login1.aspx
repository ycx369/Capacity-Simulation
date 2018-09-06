<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="BomChange.Login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <br />
    <br />
     <br />
    <br />
    <div style="width:100%;text-align:center;align-content:center;position:absolute;margin-left:0px">
    <table  style="width:700px;margin:0 auto;height:180px;font-size:x-large;border-color:limegreen" border="1" >
      
        <tr>
              <td rowspan="4"  style="width:340px" > <img src="pic/Schneider2.jpg"  style="width:340px; height:180px"/> </td>
            <td style="width:360px;color:#27A844;font-style:italic;font-size:large"  colspan="3" align="center" >Material Change System</td>
        </tr>
             <tr>
               
                 <td align="center" style="font-size:medium">账号</td>
                    <td  style="font-size:medium">
                        <asp:TextBox CssClass="text-body" ID="txtaccount" runat="server"></asp:TextBox>
                    </td>        
                </tr>
             <tr>
             
                 <td align="center" style="font-size:medium" >密码</td>
                    <td  style="font-size:medium">
                        <asp:TextBox ID="txtpassword"  CssClass="text-body"  runat="server"   TextMode="Password" ></asp:TextBox>
                    </td>
                </tr>
               <tr>      
               
                <td colspan="3" align="center"> 
                    <asp:Button runat="server" ID="UserName" CssClass="btn btn-primary btn-sm"  Text="登陆" OnClick="Button1_Click"/>
                    <asp:Button runat="server" ID="Password" CssClass="btn btn-primary btn-sm"  Text="取消"/>
                  </td>
          </tr>
    </table>
        </div>
    <br />
      <br />
    <br />
      <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
