<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar_User.ascx.cs" Inherits="ETMS_Website.Navbar_User" %>

<nav class="navbar navbar-expand-lg navbar-light mask-custom shadow-0 bg-secondary bg-gradient"
    style="--bs-bg-opacity: .2">
    <div class=" container">
        <a href="/">
            <asp:Image runat="server" CssClass="img-logo-brand" ImageUrl="~/Images/logo_event.png" />
        </a>
        <a class="navbar-brand" href="~/"><span style="color: #9292b7;">Event</span><span
            style="color: #d77bd9;">Ticket</span><span style="color: #2F4F4F;">System</span></a>
        <button class="navbar-toggler" type="button" data-mdb-toggle="collapse"
            data-mdb-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
            aria-expanded="false" aria-label="Toggle navigation">
            <i class="fas fa-bars"></i>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto">
                <li class="nav-item">
                    <a class="nav-link" href="/">Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="/User/Events">Events</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="/User/BuyTickets">Buy Tickets</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#!">News</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#!">About</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="/User/Contact">Contact</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#!">Help</a>
                </li>
            </ul>
            <ul class="navbar-nav d-flex flex-row">
                <li class="nav-item me-3 me-lg-0">
                    <a class="nav-link position-relative" href="/User/Cart">
                        <i class="fas fa-shopping-cart"></i>
                        <span
                            class="position-absolute top-0 start-100 translate-middle badge rounded-pill badge-primary"><%: ((Dictionary<int, int>)Session["cartData"]).Count %>
                            <span class="visually-hidden">products</span>
                        </span>
                    </a>
                </li>
                <% if (Context.User != null && Context.User.Identity.IsAuthenticated)
                    { %>
                <li class="nav-item d-flex ms-4">
                    <div class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Welcome, <%: GetNameByUsername()  %>
                        </button>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="/User/Profiles">Profiles</a></li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li><a class="dropdown-item" href="/Logout.aspx">Logout</a></li>
                        </ul>
                    </div>
                </li>
                <% }
                    else
                    { %>
                <li class="nav-item d-flex ms-4 gap-3">
                    <a class="btn btn-outline-primary" href="/Login.aspx">Login</a>
                    <a class="btn btn-outline-secondary" href="/Register.aspx">Register</a>
                </li>
                <% } %>
            </ul>
        </div>
    </div>
</nav>
