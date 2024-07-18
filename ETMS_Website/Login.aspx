<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ETMS_Website.Login" %>

<asp:Content ID="HeadLoginContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/Content/loginAndRegisterCss.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <main aria-labelledby="title">
        <h1 style="display: none" id="title">Login</h1>
        <div class="custom-background-image"></div>
        <div class="container login-container">
            <div class="row w-100 no-gutters shadow-lg rounded form-container bg-white p-6">
                <div class="col bg-white p-6 rounded">
                    <div class="text-center mb-4">
                        <img src="Images/logo_event.png" width="100" height="100" alt="Logo">
                    </div>
                    <h1 class="h3 mb-3 font-weight-normal text-center">Sign in to your account</h1>
                    <p class="text-center">Not a member? <a href="Register">Register now!</a></p>
                    <div class="form">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtUsername" Text="Username"></asp:Label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username" AutoCompleteType="Enabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupLogin1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Password"></asp:Label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" Placeholder="Password" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupLogin1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group form-check d-flex justify-content-between p-0">
                            <div>
                                <asp:CheckBox runat="server" ID="ckbRememberMe" />
                                <asp:Label runat="server" Text="Remember me" CssClass="form-check-label" AssociatedControlID="ckbRememberMe"></asp:Label>
                            </div>
                            <a href="#" class="float-right">Forgot password?</a>
                        </div>
                        <asp:Button CssClass="btn btn-primary w-100" runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Sign in" ValidationGroup="groupLogin1" />
                    </div>
                    <hr>
                    <div class="text-center">Or continue with</div>
                    <div class="d-flex justify-content-center mt-3">
                        <button class="btn btn-outline-secondary mr-2"><i class="fa fa-google"></i>Google</button>
                        <button class="btn btn-outline-secondary"><i class="fa fa-facebook"></i>Facebook</button>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
