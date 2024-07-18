<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventManagements.ascx.cs" Inherits="ETMS_Website.Admin.EventsUserControl.EventManagements" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hfScrollHeight" runat="server" />
        <input type="text" id="hdnSvHeightHidden" hidden value="<%: hfScrollHeight.ClientID %>" />
        <asp:GridView runat="server" ID="gvEvents" CssClass="table table-striped table-bordered" OnRowEditing="gvEvents_RowEditing" OnRowDeleting="gvEvents_RowDeleting" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="eventID" Value='<%# DataBinder.Eval(Container.DataItem, "EventID") %>' />
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:BoundField DataField="EventName" HeaderText="Name" />
                <asp:BoundField DataField="EventDescription" HeaderText="Description" />
                <asp:BoundField DataField="EventStartDate" HeaderText="Start Date" />
                <asp:BoundField DataField="EventEndDate" HeaderText="End Date" />
                <asp:BoundField DataField="VenueName" HeaderText="Location" />
                <asp:BoundField DataField="TypeName" HeaderText="Type" />
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
        <asp:Button ID="btnAddEvent" runat="server" Text="Add New Event" CssClass="btn btn-primary" OnClick="btnAddEvent_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
