<%@ Page Title="Event Details" Language="C#" MasterPageFile="~/SiteUser.master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="ETMS_Website.User.EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteUserHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteUserContentPlaceHolder" runat="server">
    <main>
        <section class="py-5 position-relative" style="min-height: 80vh;">
            <asp:Image runat="server" CssClass="event-details-image-background" ID="imgBackgroundShow" />
            <div class="container px-4 px-lg-5 my-5">
                <div class="row gx-4 gx-lg-5 align-items-center">
                    <div class="col-md-6 carousel slide" id="carouselExampleIndicators" data-bs-ride="carousel"
                        data-bs-interval="2400">
                        <div class="carousel-indicators">
                            <asp:Repeater ID="rptIndicators" runat="server">
                                <ItemTemplate>
                                    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to='<%# Container.ItemIndex %>' class='<%# Container.ItemIndex == 0 ? "active" : "" %>' aria-current='<%# Container.ItemIndex == 0 ? "true" : "false" %>' aria-label='<%# "Slide " + (Container.ItemIndex + 1).ToString() %>'></button>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="carousel-inner">
                            <asp:Repeater ID="rptImages" runat="server">
                                <ItemTemplate>
                                    <div class='<%# "carousel-item" + (Container.ItemIndex == 0 ? " active" : "") %>' style="height: 400px;">
                                        <asp:Image runat="server" ImageUrl='<%# Eval("ImageUrl") %>' CssClass="d-block w-100" AlternateText="Image" Height="400" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators"
                            data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators"
                            data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                    </div>
                    <div class="col-md-6">
                        <asp:Label runat="server" ID="lbTitleEvent" CssClass="h1 display-5 fw-bolder" />
                        <br />
                        <asp:Label CssClass="lead" runat="server" ID="lbDescription" />
                        <br />
                        <asp:Label CssClass="lead" runat="server" ID="lbLocation" />
                        <br />
                        <asp:Label CssClass="lead" runat="server" ID="lbAddress" />
                        <br />
                        <asp:Label CssClass="lead" runat="server" ID="lbCapacity" />
                        <br />
                        <div class="d-flex">
                            <a class="btn btn-outline-dark flex-shrink-0" href="#scroll">
                                <i class="bi-cart-fill me-1"></i>
                                See available tickets
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section id="scroll" class="mt-5">
            <div class="container px-5">
                <div class="row gx-5 align-items-center">
                    <div class="col-lg-12 order-lg-2 d-flex justify-content-center">
                        <h1 class="text-center text-underline-border">Available Ticket Types</h1>
                    </div>
                </div>
                <div class="d-flex flex-column">
                    <div class="w-100 d-flex justify-content-center align-content-center" id="emptyTicketTypes" runat="server" visible="false">
                        <strong>Not available tickets
                        </strong>
                    </div>
                    <asp:Repeater runat="server" ID="rpAvailableTickets" OnItemCommand="rpAvailableTickets_ItemCommand">
                        <ItemTemplate>
                            <div class="available-type-info">
                                <div class="row upcoming-event-tickettypes-items border rounded-3">
                                    <div class="col-md-9 border row m-0 p-0">
                                        <div class="col-md-12 m-0 p-0 d-flex flex-column">
                                            <div class="w-100 text-break d-flex justify-content-center">
                                                <strong class="h2">
                                                    <%# Eval("TypeName") %>
                                                </strong>
                                            </div>
                                            <div class="w-100 row">
                                                <div class="col-md-6 text-break d-flex justify-content-center">
                                                    <strong
                                                        class="h4">
                                                        <i>Start Sell: <%# Eval("StartSell") %>
                                                        </i>
                                                    </strong>
                                                </div>
                                                <div class="col-md-6 text-break d-flex justify-content-center">
                                                    <strong
                                                        class="h4">
                                                        <i>End Sell: <%# Eval("EndSell") %>
                                                        </i>
                                                    </strong>
                                                </div>
                                            </div>
                                            <div class="w-100 text-break d-flex justify-content-center">
                                                <strong class="h4">
                                                    <i>Has Seat: <%# ((bool)Eval("HasSeat") == true ? "True" : "False") %>
                                                    </i>
                                                </strong>
                                            </div>
                                            <div class="w-100 text-break d-flex justify-content-center">
                                                <strong class="h4">
                                                    <i>Cost: <%# Eval("Price") %>
                                                    </i>
                                                </strong>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 d-flex justify-content-center align-items-center flex-column border">
                                        <label>Number:</label>
                                        <asp:TextBox runat="server" ID="txtNumber" class="w-100" TextMode="Number" ReadOnly='<%# ((DateTime)Eval("EndSell")) < DateTime.Now ? true : false %>' />
                                        <asp:RequiredFieldValidator runat="server" ID="rfvCheckNumber" Display="Dynamic" ControlToValidate="txtNumber" ValidationGroup='<%# "checkNumberValid" + Container.ItemIndex.ToString() %>' ForeColor="Red" ErrorMessage="Number tickets cannot be null" />
                                        <asp:CompareValidator runat="server" ID="cvCheckNumber" Display="Dynamic" ControlToValidate="txtNumber" ValidationGroup='<%# "checkNumberValid" + Container.ItemIndex.ToString() %>' ValueToCompare="0" Operator="GreaterThan" ForeColor="Red" ErrorMessage="Number must be greater than zero." />
                                        <div class="w-100 mt-3 mb-3">
                                            <asp:LinkButton runat="server" ID="btnBuy" CssClass="w-100 btn btn-primary" Text="Buy now" CommandName="Buy" CommandArgument='<%# Eval("TypeID") %>' Enabled='<%# ((DateTime)Eval("EndSell")) < DateTime.Now ? false : true %>' ValidationGroup='<%# "checkNumberValid" + Container.ItemIndex.ToString() %>' />
                                            <asp:LinkButton runat="server" ID="btnAddToCart" CssClass="w-100 mt-1 btn btn-secondary" CommandName="AddToCart" CommandArgument='<%# Eval("TypeID") %>' Enabled='<%# ((DateTime)Eval("EndSell")) < DateTime.Now ? false : true %>' ValidationGroup='<%# "checkNumberValid" + Container.ItemIndex.ToString() %>'>Add to Cart</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="w-100 text-center" runat="server" id="totalOrders">
                    </div>
                </div>
            </div>
        </section>
        <!-- Related items section-->
        <section class="py-5 bg-light">
            <div class="container px-4 px-lg-5 mt-5">
                <div class="col-lg-12 order-lg-2 d-flex justify-content-center">
                    <h2 class="text-center text-underline-border">Related events</h2>
                </div>
                <div class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                    <div class="w-100 d-flex justify-content-center align-content-center" id="emptyRelatedEvents" runat="server" visible="false">
                        <strong>No events related
                        </strong>
                    </div>
                    <asp:Repeater runat="server" ID="rpRelatedEvents">
                        <ItemTemplate>
                            <div class="col mb-5">
                                <div class="card h-100">
                                    <!-- Product image-->
                                    <asp:Image runat="server" CssClass="card-img-top" ImageUrl='<%# (new ETMS_DatabaseHandle.BLL.ImagesEventBLL()).GetTop1ImageEventByEventID((int)Eval("EventID")) %>' AlternateText="..." />
                                    <div class="card-body p-4">
                                        <div class="text-center">
                                            <!-- Product name-->
                                            <h5 class="fw-bolder"><%# Eval("EventName") %></h5>
                                        </div>
                                    </div>
                                    <!-- Product actions-->
                                    <div class="card-footer p-4 pt-0 border-top-0 bg-transparent">
                                        <div class="text-center">
                                            <asp:LinkButton runat="server" CssClass="btn btn-outline-dark mt-auto" ID="btnViewDetailRelativeEvent" CommandArgument='<%# Eval("EventID") %>' OnClick="btnViewDetailRelativeEvent_Click" Text="View Details"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
