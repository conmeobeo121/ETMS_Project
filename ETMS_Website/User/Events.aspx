<%@ Page Title="Events" Language="C#" MasterPageFile="~/SiteUser.master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="ETMS_Website.User.Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteUserHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteUserContentPlaceHolder" runat="server">
    <section style="background-color: #eee;" class="row">
        <div class="col-md-12 px-5">
            <asp:TextBox runat="server" ID="txtSearchByName" Placeholder="Search by name..." OnTextChanged="txtSearchByName_TextChanged" CssClass="w-100"></asp:TextBox>
        </div>
        <div class="col-md-3 d-flex flex-wrap flex-row flex-md-column">
            <div class="w-100">
                <h3 class="text-center">Select types</h3>
            </div>
            <asp:RadioButtonList ID="rblTypesEvent" runat="server" CssClass="form-check me-3" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rblTypesEvent_SelectedIndexChanged">
            </asp:RadioButtonList>
        </div>
        <div class="col-md-9 container py-5">
            <asp:Repeater runat="server" ID="rfEvents">
                <ItemTemplate>
                    <div class="row justify-content-center mb-3">
                        <div class="col-md-12 col-xl-10">
                            <div class="card shadow-0 border rounded-3">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-12 col-lg-3 col-xl-3 mb-4 mb-lg-0">
                                            <div class="bg-image hover-zoom ripple rounded ripple-surface">
                                                <asp:Image runat="server" CssClass="w-100" ImageUrl='<%#  (new ETMS_DatabaseHandle.BLL.ImagesEventBLL()).GetTop1ImageEventByEventID((int)Eval("EventID")) %>' />
                                                <asp:LinkButton runat="server" PostBackUrl='<%# "~/User/EventDetails.aspx?id=" + Eval("EventID") %>'>
                                                    <div class="hover-overlay">
                                                        <div class="mask" style="background-color: rgba(253, 253, 253, 0.15);">
                                                        </div>
                                                    </div>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-lg-6 col-xl-6">
                                            <asp:Label runat="server" Text='<%# Eval("EventName") %>' />
                                            <br />
                                            <div class="d-flex flex-row">
                                                <span>Remaining: <%# ((new ETMS_DatabaseHandle.BLL.EventsBLL())).GetRemainingSeats((int)Eval("EventID")) %></span>
                                            </div>
                                            <p class="text-wrap mb-4 mb-md-0">
                                                <%# Eval("EventDescription") %>
                                            </p>
                                        </div>
                                        <div class="col-md-6 col-lg-3 col-xl-3 border-sm-start-none border-start">
                                            <div class="d-flex flex-column mt-4">
                                                <asp:LinkButton runat="server" PostBackUrl='<%# "~/User/EventDetails.aspx?id=" + Eval("EventID") %>' Text="View Details" CssClass="btn btn-primary btn-sm">
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
