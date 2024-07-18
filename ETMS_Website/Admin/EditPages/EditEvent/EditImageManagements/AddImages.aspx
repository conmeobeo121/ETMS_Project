<%@ Page Title="Admin - Add Image" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="AddImages.aspx.cs" Inherits="ETMS_Website.Admin.EditPages.EditEvent.EditImageManagements.AddImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <div class="container-fluid d-flex flex-column">
        <div class="row w-100">
            <h2>Add Image for Event</h2>
        </div>
        <div class="w-100 h-auto d-flex flex-column">
            <div class="form-group row">
                <asp:Label runat="server" Text="Choose event: " AssociatedControlID="ddlEvents" CssClass="flex-take-2" />
                <asp:DropDownList runat="server" ID="ddlEvents" CssClass="flex-take-5">
                </asp:DropDownList>
                <asp:Label runat="server" Text="Filter" AssociatedControlID="txtFilterEvents" CssClass="flex-take-1"></asp:Label>
                <asp:TextBox runat="server" ID="txtFilterEvents" CssClass="flex-take-2" OnTextChanged="txtFilterEvents_TextChanged" />
            </div>
            <div class="form-group row">
                <asp:Label runat="server" Text="Choose Image Url: " CssClass="flex-take-5" />
                <asp:FileUpload runat="server" ID="fuChooseImg" CssClass="flex-take-5 form-control form-control-lg"
                    accept=".bmp, .gif, .ico, .cur, .jpg, .jpeg, .jfif, .pjpeg, .pjp, .png, .svg, .tif, .tiff, .webp" />
            </div>
        </div>
    </div>
    <div class="w-100 custom-footer">
        <asp:Button runat="server" ID="btnAddImg" Text="Add New Image" ValidationGroup="groupImages1" OnClick="btnAddImg_Click" />
    </div>
    <asp:CustomValidator ID="cvFullCheckValidate" runat="server"
        OnServerValidate="cvFullCheckValidate_ServerValidate"
        Display="Dynamic" ForeColor="Red" ValidationGroup="groupImages1" />
</asp:Content>
