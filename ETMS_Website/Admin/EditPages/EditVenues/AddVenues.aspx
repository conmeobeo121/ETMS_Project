<%@ Page Title="Admin - Add Venue" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="AddVenues.aspx.cs" Inherits="ETMS_Website.Admin.EditPages.EditVenues.AddVenues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <div class="container-fluid d-flex flex-column">
        <div class="row w-100">
            <h2>Add Venue</h2>
        </div>
        <div class="w-100 h-auto d-flex flex-column">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtVenueName" Text="Venue Name"></asp:Label>
                <asp:TextBox ID="txtVenueName" runat="server" CssClass="form-control" Placeholder="Name" AutoCompleteType="Enabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvVenueName" runat="server" ControlToValidate="txtVenueName" ErrorMessage="Name is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupVenues1" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtVenueAddress" Text="Venue Address"></asp:Label>
                <asp:TextBox ID="txtVenueAddress" runat="server" CssClass="form-control" Placeholder="Address" AutoCompleteType="Enabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvVenueAddress" runat="server" ControlToValidate="txtVenueAddress" ErrorMessage="Address is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupVenues1" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtVenueCapacity" Text="Venue Capacity"></asp:Label>
                <asp:TextBox ID="txtVenueCapacity" runat="server" CssClass="form-control" Placeholder="Capacity" AutoCompleteType="Enabled" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvVenueCapacity" runat="server" ControlToValidate="txtVenueCapacity" ErrorMessage="Capacity is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupVenues1" />
                <asp:CompareValidator ID="cvVenueCapacity" runat="server" ControlToValidate="txtVenueCapacity" ValueToCompare="0" Operator="GreaterThan" ErrorMessage="Capacity must be greater than zero" ForeColor="Red" Display="Dynamic" ValidationGroup="groupVenues1" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtVenueCity" Text="Venue City"></asp:Label>
                <asp:TextBox ID="txtVenueCity" runat="server" CssClass="form-control" Placeholder="City" AutoCompleteType="Enabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvVenueCity" runat="server" ControlToValidate="txtVenueCity" ErrorMessage="City is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupVenues1" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtVenueState" Text="Venue State"></asp:Label>
                <asp:TextBox ID="txtVenueState" runat="server" CssClass="form-control" Placeholder="State" AutoCompleteType="Enabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvVenueState" runat="server" ControlToValidate="txtVenueState" ErrorMessage="State is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupVenues1" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtVenueZipCode" Text="Venue Zip Code"></asp:Label>
                <asp:TextBox ID="txtVenueZipCode" runat="server" CssClass="form-control" Placeholder="Zip Code" AutoCompleteType="Enabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvVenueZipCode" runat="server" ControlToValidate="txtVenueZipCode" ErrorMessage="Zip Code is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupVenues1" />
            </div>
        </div>
    </div>
    <div class="w-100 custom-footer">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary custom-button" Text="Add" OnClick="btnAdd_Click" ValidationGroup="groupVenues1" />
    </div>
</asp:Content>
