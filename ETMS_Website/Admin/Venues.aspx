<%@ Page Title="Admin - Venues" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="Venues.aspx.cs" Inherits="ETMS_Website.Admin.Venues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <main aria-labelledby="title" class="container-fluid w-100 h-100 mh-100-vh d-flex flex-column">
        <div class="row flex-take-1_5">
            <h1 id="title">Venues Management</h1>
        </div>
        <div class="row flex-take-8_5 overflow-scroll position-relative">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gvVenues" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnRowDeleting="gvVenues_RowDeleting" OnRowEditing="gvVenues_RowEditing">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="venueID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "VenueID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="VenueName" HeaderText="Name" />
                            <asp:BoundField DataField="VenueAddress" HeaderText="Address" />
                            <asp:BoundField DataField="VenueCapacity" HeaderText="Capacity" />
                            <asp:BoundField DataField="VenueCity" HeaderText="City" />
                            <asp:BoundField DataField="VenueState" HeaderText="State" />
                            <asp:BoundField DataField="VenueZipCode" HeaderText="Zip Code" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <div class="custom-footer">
                                        <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-secondary custom-button" />
                                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger custom-button" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnAddVenue" runat="server" Text="Add New Venue" CssClass="btn btn-primary" OnClick="btnAddVenue_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </main>
</asp:Content>
