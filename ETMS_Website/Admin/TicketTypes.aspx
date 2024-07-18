<%@ Page Title="Admin - Ticket Types" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="TicketTypes.aspx.cs" Inherits="ETMS_Website.Admin.TicketTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <main aria-labelledby="title" class="container-fluid w-100 h-100 mh-100-vh d-flex flex-column">
        <div class="row flex-take-1_5">
            <h1 id="title">Ticket Types</h1>
        </div>
        <div class="row flex-take-8_5 overflow-scroll position-relative">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfScrollHeight" runat="server" />
                    <input type="text" id="hdnSvHeightHidden" hidden value="<%: hfScrollHeight.ClientID %>" />
                    <asp:GridView runat="server" ID="gvTicketTypes" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnRowDeleting="gvTicketTypes_RowDeleting" OnRowEditing="gvTicketTypes_RowEditing">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="typeID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TypeID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TypeName" HeaderText="Type name" />
                            <asp:BoundField DataField="Price" HeaderText="Price" />
                            <asp:BoundField DataField="EventName" HeaderText="Event Name" />
                            <asp:BoundField DataField="HasSeat" HeaderText="Has Seat" />
                            <asp:BoundField DataField="StartSell" HeaderText="Start Sell" />
                            <asp:BoundField DataField="EndSell" HeaderText="End Sell" />
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
                    <asp:Button ID="btnAddTicketTypes" runat="server" Text="Add New Ticket Types" CssClass="btn btn-primary" OnClick="btnAddTicketTypes_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </main>
</asp:Content>
