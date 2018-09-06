<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BomChange.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <%--    	<%@include file="WEB-INF/jsp/head.jsp"%>--%>
    <img src="Images/loginbackground.jpg" id="surfacePic"   width="1200px" />

    <div class="loginbackground">
		<div class="container">
			<br>
			<br>
			<div class="row">
				<div class="col-xs-8"></div>
				<div class="col-xs-4">
					<form action="UserLogin" method="post" class="form-horizontal">
						<div class="form-group">
							<div class="col-sm-offset-3 col-sm-9">
							</div>
							<label for="inputSESAID"
								class="col-sm-3 control-label labelcolor">账号:</label>
							<div class="col-sm-9">
								<input name="staff.sesaid" type="SESAID" class="form-control"
									id="SESAIDs" placeholder="SESAID">
							</div>
						</div>
						<div class="form-group">
							<label for="inputPassword3"
								class="col-sm-3 control-label labelcolor">密码:</label>
							<div class="col-sm-9">
								<input name="staff.password" type="password" class="form-control"
									id="inputPassword3" placeholder="Password">
							</div>
						</div>

						<div class="form-group">
							<div class="col-sm-offset-3 col-sm-2">
								<button type="submit" class="btn btn-default">Sign in</button>
							</div>
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>

</asp:Content>
