<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ETMS_Website.Register" %>

<asp:Content ID="HeadRegisterContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/Content/loginAndRegisterCss.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="RegisterContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <main aria-labelledby="title">
        <h1 style="display: none" id="title">Register</h1>
        <div class="custom-background-image"></div>
        <div class="container login-container">
            <div class="row w-100 no-gutters shadow-lg rounded form-container bg-white p-6">
                <div class="col bg-white p-6 rounded">
                    <div class="text-center mb-4">
                        <img src="Images/logo_event.png" width="100" height="100" alt="Logo">
                    </div>
                    <h1 class="h3 mb-3 font-weight-normal text-center">Sign up for your account</h1>
                    <p class="text-center">Have an account? <a href="Login">Login now!</a></p>
                    <div class="row">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtUsername" Text="Username"></asp:Label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupRegister1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revUsername" runat="server"
                                ControlToValidate="txtUsername"
                                ErrorMessage="Invalid username. Must be at least 8 characters, include one letter, one number, and may have a hyphen (_-) not at the beginning or end."
                                Display="Dynamic"
                                ForeColor="Red"
                                ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+([-_][A-Za-z\d]+)?$"
                                ValidationGroup="groupRegister1" />
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Password"></asp:Label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" Placeholder="Password" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupRegister1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPassword" runat="server"
                                ControlToValidate="txtPassword"
                                ErrorMessage="Invalid password. Must contain at least 8 characters, including at least 1 letter (lowercase or uppercase), number, and 1 special character."
                                ValidationGroup="groupRegister1"
                                Display="Dynamic"
                                ForeColor="Red"
                                ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[\W_]).{8,}$" />
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtRePassword" Text="Re-enter Password"></asp:Label>
                            <asp:TextBox ID="txtRePassword" TextMode="Password" runat="server" CssClass="form-control" Placeholder="Re-enter Password" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRePassword" runat="server" ControlToValidate="txtRePassword" ErrorMessage="Re-Password is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupRegister1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvRePassword" runat="server" ControlToValidate="txtRePassword" ControlToCompare="txtPassword" ErrorMessage="Re-password do not match" Display="Dynamic" ForeColor="Red" ValidationGroup="groupRegister1" Operator="Equal"></asp:CompareValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupRegister1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is not a valid format" Display="Dynamic" ValidationExpression="^(\w+.@\w+.\w{2,4})$" ValidationGroup="groupRegister1" />
                        </div>
                        <div class="form-group form-check d-flex justify-content-between p-0">
                            <div>
                                <asp:CheckBox runat="server" ID="ckbAutoLogin" />
                                <asp:Label runat="server" Text="Auto login after register" CssClass="form-check-label" AssociatedControlID="ckbAutoLogin"></asp:Label>
                            </div>
                        </div>
                        <asp:Button CssClass="btn btn-primary w-100" runat="server" ID="btnRegister" OnClick="btnRegister_Click" Text="Sign up"  ValidationGroup="groupRegister1"/>
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
