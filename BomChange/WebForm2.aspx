<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="BomChange.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function GetDatas() {
            alert(' GetDatas() ')
            $.ajax(
                {
                    url: 'Handler2.ashx',
                    type: 'post',
                    datatype:'json',
                    error: function (data) { alert('失败');},
                    success: function (data) {
                        alert(data);

                    }

                }
            )
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <input type="button" onclick="GetDatas()" value="获取" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
</asp:Content>
