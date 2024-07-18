<%@ Page Title="Home" Language="C#" MasterPageFile="~/SiteUser.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ETMS_Website.User.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteUserHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteUserContentPlaceHolder" runat="server">
    <header class="masthead text-center text-white">
        <asp:Image runat="server" CssClass="masthead-img-background" ImageUrl="~/Images/background_image.jpg" />
        <div class="masthead-content">
            <div class="container px-5">
                <h1 class="masthead-heading mb-0 masthead-text">Effortless Ticket Booking Experience</h1>
                <h3 class="masthead-subheading mb-0 masthead-text mt-2">Discover many exciting events at affordable
                        ticket prices</h3>
                <a class="btn btn-primary btn-xl rounded-pill mt-4" href="#scroll">Learn More</a>
            </div>
        </div>
    </header>
    <section id="scroll">
        <div class="container px-5">
            <div class="row gx-5 align-items-center">
                <div class="col-lg-6 order-lg-2">
                    <div class="p-5">
                        <asp:Image runat="server" CssClass="img-fluid rounded-circle" ImageUrl="~/Images/hot_event.png" />
                    </div>
                </div>
                <div class="col-lg-6 order-lg-1">
                    <div class="p-5">
                        <h2 class="display-4">Hot Events</h2>
                        <p>
                            Explore the most exhilarating events around the globe, from music festivals and fashion
                                shows to cultural and sports happenings. Discover unique experiences that promise to
                                enhance your adventures and ignite your passions. Stay updated on the latest and
                                greatest events, ensuring you never miss out.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section>
        <div class="container px-5">
            <div class="row gx-5 align-items-center">
                <div class="col-lg-6">
                    <div class="p-5">
                        <asp:Image runat="server" CssClass="img-fluid rounded-circle" ImageUrl="~/Images/ticket_type_variant.jpg" />
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="p-5">
                        <h2 class="display-4">Various types of tickets</h2>
                        <p>
                            Each event offers a diverse range of ticket options, catering to different preferences
                                and budgets. From VIP passes that provide exclusive access and perks to standard tickets
                                that are economically priced, there's something for everyone. Additionally, look out for
                                special promotions and early bird discounts that make attending events even more
                                accessible and enjoyable.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section>
        <div class="container px-5">
            <div class="row gx-5 align-items-center">
                <div class="col-lg-6 order-lg-2">
                    <div class="p-5">
                        <asp:Image runat="server" CssClass="img-fluid rounded-circle" ImageUrl="~/Images/promotion_home.png" />
                    </div>
                </div>
                <div class="col-lg-6 order-lg-1">
                    <div class="p-5">
                        <h2 class="display-4">Many attractive promotions.</h2>
                        <p>
                            Discover numerous attractive promotions designed to enhance your event-going experience.
                                From early bird discounts to group offers, our promotions make it easier and more
                                affordable to enjoy your favorite events. Stay tuned for seasonal specials and exclusive
                                deals that add extra value to your purchases.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <main>
        <section>
            <div class="container px-5">
                <div class="row gx-5 align-items-center">
                    <div class="col-lg-12 order-lg-2 d-flex justify-content-center">
                        <h1 class="text-uppercase text-center text-underline-border">Ongoing events</h1>
                    </div>
                </div>
                <div class="d-flex flex-column">
                    <div class="w-100 d-flex justify-content-center align-content-center" id="emptyOngoing" runat="server" visible="false">
                        <strong>No ongoing events
                        </strong>
                    </div>
                    <asp:Repeater runat="server" ID="rpOngoingEvents">
                        <ItemTemplate>
                            <div class="ongoing-event-info">
                                <div class="border row ongoing-event-items">
                                    <div class="col-md-12 d-flex justify-content-center align-items-center">
                                        <strong class="ongoing-event-text"><%# Eval("EventName") %></strong>
                                    </div>
                                    <a href='<%# "/User/EventDetails.aspx?id=" + Eval("EventID") %>' class="ongoing-link-detail">See details</a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </section>
        <section>
            <div class="container px-5">
                <div class="row gx-5 align-items-center">
                    <div class="col-lg-12 order-lg-2 d-flex justify-content-center">
                        <h1 class="text-uppercase text-center text-underline-border">Upcoming events</h1>
                    </div>
                </div>
                <div class="d-flex flex-column">
                    <div class="w-100 d-flex justify-content-center align-content-center" id="emptyUpcoming" runat="server" visible="false">
                        <strong>No upcoming events
                        </strong>
                    </div>

                    <asp:Repeater runat="server" ID="rpUpcomingEvents">
                        <ItemTemplate>
                            <div class="upcoming-event-info">
                                <div class="row upcoming-event-tickettypes-items">
                                    <div class="col-md-3 d-flex justify-content-center border align-items-center">
                                        <strong><%# Eval("EventStartDate") %></strong>
                                    </div>
                                    <div class="col-md-9 d-flex justify-content-center border align-items-center">
                                        <strong><%# Eval("EventName") %></strong>
                                    </div>
                                    <a href='<%# "/User/EventDetails.aspx?id=" + Eval("EventID") %>' class="upcoming-link-detail">See details</a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </section>
        <section>
            <div class="container px-5">
                <div class="row gx-5 align-items-center">
                    <div class="col-lg-12 order-lg-2 d-flex justify-content-center">
                        <h1 class="text-uppercase text-center text-underline-border">Hot ticket types</h1>
                    </div>
                </div>
                <div class="d-flex flex-column">
                    <div class="upcoming-event-info">
                        <div class="row upcoming-event-tickettypes-items">
                            <div class="col-md-3 d-flex justify-content-center border align-items-center">
                                <strong>2024-09-10 - 2024-09-29</strong>
                            </div>
                            <div class="col-md-9 d-flex justify-content-center border align-items-center">
                                <strong>Normal Tickets - World Championship 2024 - 200000d</strong>
                            </div>
                            <a href="#" class="upcoming-link-detail">See details</a>
                        </div>
                    </div>
                    <div class="upcoming-event-info">
                        <div class="row upcoming-event-tickettypes-items">
                            <div class="col-md-3 d-flex justify-content-center border align-items-center">
                                <strong>2024-09-10 - 2024-09-29</strong>
                            </div>
                            <div class="col-md-9 d-flex justify-content-center border align-items-center">
                                <strong>Normal Tickets - World Championship 2024 - 200000d</strong>
                            </div>
                            <a href="#" class="upcoming-link-detail">See details</a>
                        </div>
                    </div>
                    <div class="upcoming-event-info">
                        <div class="row upcoming-event-tickettypes-items">
                            <div class="col-md-3 d-flex justify-content-center border align-items-center">
                                <strong>2024-09-10 - 2024-09-29</strong>
                            </div>
                            <div class="col-md-9 d-flex justify-content-center border align-items-center">
                                <strong>Normal Tickets - World Championship 2024 - 200000d</strong>
                            </div>
                            <a href="#" class="upcoming-link-detail">See details</a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container px-5">
                <div class="row gx-5 align-items-center">
                    <div class="col-lg-12 order-lg-2 d-flex justify-content-center">
                        <h1 class="text-uppercase text-center text-underline-border">Promotions</h1>
                    </div>
                </div>
                <div class="w-100 h-100 d-flex justify-content-center">
                    <strong>No promotions</strong>
                </div>
            </div>
            <section>
                <div class="container px-5">
                    <div class="row gx-5 align-items-center">
                        <div class="col-lg-12 order-lg-2 d-flex justify-content-center">
                            <h1 class="text-uppercase text-center text-underline-border">News</h1>
                        </div>
                    </div>
                </div>
                <div class="w-100 h-100 d-flex justify-content-center">
                    <strong>No news</strong>
                </div>
            </section>
        </section>
    </main>
</asp:Content>
