<%@ Page Title="Admin - Users" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="ETMS_Website.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <main aria-labelledby="title" class="container-fluid w-100 h-100 mh-100-vh d-flex flex-column">
        <div class="row flex-take-1_5">
            <h1 id="title">Users Management</h1>
        </div>
        <div class="row flex-take-8_5 overflow-scroll position-relative">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gvUsers" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnRowDeleting="gvUsers_RowDeleting" OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="userID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <%# Eval("Username") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUsername" runat="server" Text='<%# Bind("Username") %>' CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" Display="Dynamic" ForeColor="Red" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revUsername" runat="server"
                                        ControlToValidate="txtUsername"
                                        ErrorMessage="Invalid username. Must be at least 8 characters, include one letter, one number, and may have a hyphen (_-) not at the beginning or end."
                                        Display="Dynamic"
                                        ForeColor="Red"
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+([-_][A-Za-z\d]+)?$"
                                        ValidationGroup="EditGroup" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Password (Hashed)">
                                <ItemTemplate>
                                    <%# Eval("Password") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPassword" runat="server" Placeholder="New Password" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" Display="Dynamic" ForeColor="Red" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revPassword" runat="server"
                                        ControlToValidate="txtPassword"
                                        ErrorMessage="Invalid password. Must contain at least 8 characters, including at least 1 letter (lowercase or uppercase), number, and 1 special character."
                                        ValidationGroup="EditGroup"
                                        Display="Dynamic"
                                        ForeColor="Red"
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[\W_]).{8,}$" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <%# Eval("Email") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" Display="Dynamic" ForeColor="Red" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is not a valid format" Display="Dynamic" ValidationExpression="^(\w+.@\w+.\w{2,4})$" ValidationGroup="EditGroup" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Usertype">
                                <ItemTemplate>
                                    <%# Eval("UserType") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="d-flex flex-column">
                                        <asp:Label runat="server" Text="Choose User Type:" CssClass="w-100"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlUserTypes" CssClass="w-100">
                                            <asp:ListItem Value="Customer" Text="Customer" />
                                            <asp:ListItem Value="Admin" Text="Admin" />
                                        </asp:DropDownList>
                                    </div>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <div class="custom-footer">
                                        <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-secondary custom-button" />
                                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger custom-button" />
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="custom-footer">
                                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" ValidationGroup="EditGroup" CssClass="btn btn-primary custom-button" />
                                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="False" CssClass="btn btn-secondary custom-button" />
                                    </div>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="container-fluid d-flex flex-column">
                        <div class="row w-100">
                            <h4>Add New User</h4>
                        </div>
                        <div class="d-flex flex-column">
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
                                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupRegister1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is not a valid format" Display="Dynamic" ValidationExpression="^(\w+.@\w+.\w{2,4})$" ValidationGroup="groupRegister1" />
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" Text="Choose User Type:" CssClass="w-100"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlUserTypes" CssClass="w-100">
                                    <asp:ListItem Value="Customer" Text="Customer" Selected="True" />
                                    <asp:ListItem Value="Admin" Text="Admin" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnAddNewUser" runat="server" Text="Add New User" CssClass="btn btn-primary" OnClick="btnAddNewUser_Click" ValidationGroup="groupRegister1" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </main>
</asp:Content>
