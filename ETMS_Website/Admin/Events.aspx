<%@ Page Title="Admin - Events" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="ETMS_Website.Admin.Events" %>

<%@ Register Src="~/Admin/UserControls/EventsUserControl/TypesUserControl/EventTypes.ascx" TagName="EventTypes" TagPrefix="uc" %>
<%@ Register Src="~/Admin/UserControls/EventsUserControl/ManagementsUserControl/EventManagements.ascx" TagName="EventManagements" TagPrefix="uc" %>
<%@ Register Src="~/Admin/UserControls/EventsUserControl/ImagesUserControl/ImageManagements.ascx" TagName="EventImageManagements" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <main aria-labelledby="title" class="container-fluid w-100 h-100 mh-100-vh d-flex flex-column">
        <div class="row flex-take-2">
            <h1 id="title">Events Management</h1>
            <div class="row" id="ListMenus" runat="server">
                <asp:LinkButton runat="server" ID="linkEventTypes" Text="Type of Events" CssClass="col-md-3 btn btn-primary" OnClick="LinkEventTypes_Click" />
                <asp:LinkButton runat="server" ID="linkEventManagements" Text="Event Managements" CssClass="col-md-3 btn btn-secondary ms-2" OnClick="LinkEventManagements_Click" />
                <asp:LinkButton runat="server" ID="LinkEventImgManagements" Text="Image Managements" CssClass="col-md-3 btn btn-light ms-2" OnClick="LinkEventImgManagements_Click" />
            </div>
        </div>
        <div class="row flex-take-8 overflow-scroll position-relative">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <uc:EventTypes runat="server" ID="ucEventTypes" Visible="false" />
                    <uc:EventManagements runat="server" ID="ucEventManagements" Visible="false" />
                    <uc:EventImageManagements runat="server" ID="ucEventImgManagements" Visible="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </main>
</asp:Content>
