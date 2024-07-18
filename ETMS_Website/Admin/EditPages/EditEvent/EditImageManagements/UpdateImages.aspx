<%@ Page Title="Admin - Update Image" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="UpdateImages.aspx.cs" Inherits="ETMS_Website.Admin.EditPages.EditEvent.EditImageManagements.UpdateImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <div class="container-fluid d-flex flex-column">
        <div class="row w-100">
            <h2>Update Image for Event</h2>
        </div>
        <div class="w-100 h-auto d-flex flex-column">
            <asp:HiddenField runat="server" ID="hdnIDImage" />
            <div class="form-group row">
                <asp:Label runat="server" Text="Choose event: " AssociatedControlID="ddlEvents" CssClass="flex-take-2" />
                <asp:DropDownList runat="server" ID="ddlEvents" CssClass="flex-take-5">
                </asp:DropDownList>
                <asp:Label runat="server" Text="Filter" AssociatedControlID="txtFilterEvents" CssClass="flex-take-1"></asp:Label>
                <asp:TextBox runat="server" ID="txtFilterEvents" CssClass="flex-take-2" OnTextChanged="txtFilterEvents_TextChanged" />
            </div>
            <div class="form-group row">
                <asp:Label runat="server" ID="lbCurrentFile" Text="" CssClass="flex-take-10" />
                <asp:Label runat="server" Text="Choose Image Url: " CssClass="flex-take-5" />
                <asp:FileUpload runat="server" ID="fuChooseImg" CssClass="flex-take-5 form-control form-control-lg"
                    accept=".bmp, .gif, .ico, .cur, .jpg, .jpeg, .jfif, .pjpeg, .pjp, .png, .svg, .tif, .tiff, .webp" />
            </div>
        </div>
    </div>
    <div class="w-100 custom-footer">
        <asp:Button runat="server" ID="btnUpdateImg" Text="Update Image" ValidationGroup="groupImages1" OnClick="btnUpdateImg_Click" />
    </div>
    <asp:CustomValidator ID="cvFullCheckValidate" runat="server"
        OnServerValidate="cvFullCheckValidate_ServerValidate"
        Display="Dynamic" ForeColor="Red" ValidationGroup="groupImages1" />
</asp:Content>
