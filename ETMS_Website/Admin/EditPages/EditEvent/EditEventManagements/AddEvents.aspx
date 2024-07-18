<%@ Page Title="Admin - Add Event" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="AddEvents.aspx.cs" Inherits="ETMS_Website.Admin.EditPages.EditEvent.EditEventManagements.AddEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <div class="container-fluid d-flex flex-column">
        <div class="row w-100">
            <h2>Add Event</h2>
        </div>
        <div class="d-flex flex-column">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEventName" Text="Event Name"></asp:Label>
                <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" Placeholder="Type event name..." AutoCompleteType="Enabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEventName" runat="server" ControlToValidate="txtEventName" ErrorMessage="Event name is required" Display="Dynamic" ForeColor="Red" ValidationGroup="groupEvents1" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEventDescription" Text="Event Description"></asp:Label>
                <asp:TextBox ID="txtEventDescription" runat="server" CssClass="form-control" Placeholder="Type description..." AutoCompleteType="Enabled"></asp:TextBox>
            </div>
            <div class="row container-fluid">

                <div class="col-6 d-flex flex-column align-items-center">
                    <asp:Label runat="server" AssociatedControlID="dtStart" Text="Select Start Date" />
                    <asp:Calendar ID="dtStart" runat="server" Height="200px" Width="300px"
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
                    <asp:Label runat="server" AssociatedControlID="dtEnd" Text="Select End Date" />

                    <asp:Calendar ID="dtEnd" runat="server" Height="200px" Width="300px"
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
            <div class="form-group row">
                <asp:Label runat="server" Text="Choose a locale" AssociatedControlID="ddlVenues" CssClass="flex-take-2" />
                <asp:DropDownList runat="server" ID="ddlVenues" AutoPostBack="true" CssClass="flex-take-5">
                </asp:DropDownList>
                <asp:Label runat="server" Text="Filter" AssociatedControlID="txtFilterVenues" CssClass="flex-take-1"></asp:Label>
                <asp:TextBox runat="server" ID="txtFilterVenues" CssClass="flex-take-2" OnTextChanged="txtFilterVenues_TextChanged" />
            </div>
            <div class="form-group row">
                <asp:Label runat="server" Text="Choose type of event" AssociatedControlID="ddlTypes" CssClass="flex-take-2" />
                <asp:DropDownList runat="server" ID="ddlTypes" AutoPostBack="true" CssClass="flex-take-5">
                </asp:DropDownList>
                <asp:Label runat="server" Text="Filter" AssociatedControlID="txtFilterTypes" CssClass="flex-take-1"></asp:Label>
                <asp:TextBox runat="server" ID="txtFilterTypes" CssClass="flex-take-2" OnTextChanged="txtFilterTypes_TextChanged" />
            </div>
        </div>
    </div>
    <div class="w-100 custom-footer">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary custom-button" Text="Add" OnClick="btnAdd_Click" ValidationGroup="groupEvents1" />
    </div>
    <asp:CustomValidator ID="cvCustomValidator" runat="server"
        OnServerValidate="cvCustomValidator_ServerValidate"
        Display="Dynamic" ForeColor="Red" ValidationGroup="groupEvents1" />
</asp:Content>
