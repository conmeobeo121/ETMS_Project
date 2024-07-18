<%@ Page Title="Admin - Update Ticket Type" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="UpdateTicketTypes.aspx.cs" Inherits="ETMS_Website.Admin.EditPages.EditTicketTypes.UpdateTicketTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <div class="container-fluid d-flex flex-column">
        <div class="row w-100">
            <h2>Update Ticket Type</h2>
        </div>
        <div class="w-100 h-auto d-flex flex-column">
            <asp:HiddenField runat="server" ID="hdnIDTicketType" />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtTicketName" Text="Ticket Type Name"></asp:Label>
                <asp:TextBox ID="txtTicketName" runat="server" CssClass="form-control" Placeholder="Name" AutoCompleteType="Enabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTicketTypeName" runat="server" ControlToValidate="txtTicketName" ErrorMessage="Name is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupTicketTypes1" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPrice" Text="Price"></asp:Label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Placeholder="Price" AutoCompleteType="Enabled" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="Price is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupTicketTypes1" />
                <asp:CompareValidator ID="compPrice" runat="server" ControlToValidate="txtPrice" Display="Dynamic" ErrorMessage="Price must be greater than 0" ValueToCompare="0" Operator="GreaterThan" ForeColor="Red" ValidationGroup="groupTicketTypes1"></asp:CompareValidator>
            </div>
            <div class="form-group row">
                <asp:Label runat="server" Text="Choose a event" AssociatedControlID="ddlEvents" CssClass="flex-take-2" />
                <asp:DropDownList runat="server" ID="ddlEvents" AutoPostBack="true" CssClass="flex-take-5">
                </asp:DropDownList>
                <asp:Label runat="server" Text="Filter" AssociatedControlID="txtFilterEvents" CssClass="flex-take-1"></asp:Label>
                <asp:TextBox runat="server" ID="txtFilterEvents" CssClass="flex-take-2" AutoPostBack="true" OnTextChanged="txtFilterEvents_TextChanged" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="chkbHasSeat" Text="Has Seat"></asp:Label>
                <asp:CheckBox runat="server" ID="chkbHasSeat" />
            </div>
            <div class="row container-fluid">

                <div class="col-6 d-flex flex-column align-items-center">
                    <asp:Label runat="server" AssociatedControlID="dtStartSell" Text="Select Start Date" />
                    <asp:Calendar ID="dtStartSell" runat="server" Height="200px" Width="300px"
                        BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="2px">
                        <DayHeaderStyle Height="40px" />
                        <DayStyle BorderStyle="Solid"
                            BorderColor="Black"
                            BorderWidth="1"
                            HorizontalAlign="Right"
                            VerticalAlign="Top" Height="40px" />
                        <OtherMonthDayStyle BackColor="LightSteelBlue" />
                        <SelectorStyle CssClass="btn-info" />

                        <TitleStyle Height="40px" />

                        <TodayDayStyle BackColor="LightSkyBlue" />
                        <WeekendDayStyle BackColor="Ivory" Height="30px" />
                    </asp:Calendar>
                </div>
                <div class="col-6 d-flex flex-column align-items-center">
                    <asp:Label runat="server" AssociatedControlID="dtEndSell" Text="Select End Date" />

                    <asp:Calendar ID="dtEndSell" runat="server" Height="200px" Width="300px"
                        BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="2px">
                        <DayHeaderStyle Height="40px" />
                        <DayStyle BorderStyle="Solid"
                            BorderColor="Black"
                            BorderWidth="1"
                            HorizontalAlign="Right"
                            VerticalAlign="Top" Height="40px" />
                        <OtherMonthDayStyle BackColor="LightSteelBlue" />
                        <SelectorStyle CssClass="btn-info" />

                        <TitleStyle Height="40px" />

                        <TodayDayStyle BackColor="LightSkyBlue" />
                        <WeekendDayStyle BackColor="Ivory" Height="30px" />
                    </asp:Calendar>
                </div>
            </div>
        </div>
    </div>
    <div class="w-100 custom-footer">
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary custom-button" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="groupTicketTypes1" />
    </div>
    <asp:CustomValidator runat="server" ID="cvCheckTicketType" ForeColor="Red" CssClass="alert alert-danger" OnServerValidate="cvCheckTicketType_ServerValidate" Display="Dynamic" ValidationGroup="groupTicketTypes1"></asp:CustomValidator>
</asp:Content>
