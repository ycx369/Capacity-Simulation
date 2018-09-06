<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="WebForm6.aspx.cs" Inherits="BomChange.WebForm6" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <table>
                <tr><th>
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                        <asp:ListItem>请选择</asp:ListItem>
                        <asp:ListItem>ECN</asp:ListItem>
                        <asp:ListItem>SO</asp:ListItem>
                        <asp:ListItem>MO</asp:ListItem>
                    </asp:DropDownList>
                    </th></tr>
                <tr>
                    <th class="auto-style1">操作</th>
                    <th class="auto-style1">账号</th>
                    <th class="auto-style1">密码</th>
                    <th class="auto-style1">性别</th>
                    <th class="auto-style1">生日</th>
                    <th class="auto-style1">地址</th>
                    <th class="auto-style1">电话</th>
                </tr>

                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <th>

                                <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Edit" CommandArgument='<%#Eval("userId") %>' />
                                <asp:Button ID="Button1" runat="server" Text="删除" CommandName="Del" />
                            </th>
                            <th><%#Eval("account") %></th>
                            <th><%#Eval("account") %></th>
                            <th><%#Eval("account") %></th>
                            <th><%#Eval("account") %></th>
                            <th><%#Eval("account") %></th>
                            <th><%#Eval("account") %></th>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

                <tr>
                    <th colspan="7">
                        <asp:Button ID="btnFirst" runat="server" Text="首页" OnClick="btnFirst_Click" />
                        <asp:Button ID="btnAhead" runat="server" Text="上一页" OnClick="btnAhead_Click" />
                        <asp:Button ID="btnLast" runat="server" Text="下一页" />
                        <asp:Button ID="btnEnd" runat="server" Text="尾页" OnClick="btnEnd_Click" />
                        跳转<asp:TextBox ID="txtTurnPage" runat="server"></asp:TextBox>
                        页 
                        <asp:Button ID="btnTurn" runat="server" Text="跳转" />
                        当前<asp:Label ID="lblNowPage" runat="server" Text="Label"></asp:Label>
                        页&nbsp; 共<asp:Label ID="lblTotalPage" runat="server" Text="Label"></asp:Label>
                        页共<asp:Label ID="lblTotalCount" runat="server" Text="Label"></asp:Label>
                        条
                    </th>
                </tr>

            </table>

    </form>
</body>
</html>
