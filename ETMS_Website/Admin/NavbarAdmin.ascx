<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavbarAdmin.ascx.cs" Inherits="ETMS_Website.Admin.NavbarAdmin" %>
<nav class="col-md-2 d-none d-md-block bg-light sidebar">
    <div class="sidebar-sticky">
        <div class="nav-item w-100 h-auto">
            <div class="text-center">
                <asp:Image runat="server" AlternateText="event logo" class="img-fluid mb-2" ImageUrl="~/Images/logo_event.png" Width="200" Height="200" />
                <h1 class="sidebar-heading" style="font-size: 1.5rem;">Ticket Management System</h1>
            </div>
        </div>
        <div class="row no-gutters w-100 h-auto">
            <div class="col-12">
                <h5 class="h5 text-center">Welcome, <%: (Context.User.Identity.IsAuthenticated ? Context.User.Identity.Name : "") %></h5>
            </div>
        </div>
        <ul class="nav flex-column" id="MenuItems" runat="server">
            <li>
                <h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
                    <span>Menu</span>
                </h6>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/Dashboard.aspx" runat="server">
                    <i class="fa fas fa-home"></i>
                    Dashboard
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/Venues.aspx" runat="server">
                    <i class="fa-regular fas fa-location-dot"></i>
                    Venues
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/Events.aspx" runat="server">
                    <i class="fa-regular fas fa-calendar"></i>
                    Events
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/TicketTypes.aspx" runat="server">
                    <i class="fa-regular fas fa-object-group"></i>
                    Ticket Types
                </a>
            </li>
            <%--<li class="nav-item">
                <a class="nav-link" href="/Admin/Tickets.aspx" runat="server">
                    <i class="fa-regular fas fa-ticket"></i>
                    Tickets
                </a>
            </li>--%>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/Orders.aspx" runat="server">
                    <i class="fa fas fa-shopping-cart"></i>
                    Orders <span class="badge badge-primary">16</span>
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/Promotions.aspx" runat="server">
                    <i class="fa-solid fas fa-percent"></i>
                    Promotions
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/Notification.aspx" runat="server">
                    <i class="fa-solid fas fa-bullhorn"></i>
                    Notification
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/News.aspx" runat="server">
                    <i class="fa-solid fas fa-newspaper"></i>
                    News
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/Users.aspx" runat="server">
                    <i class="fa fas fa-user-plus"></i>
                    Users
                </a>
            </li>
            <li>
                <h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
                    <span>Settings</span>
                </h6>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="/Admin/PersonalSettings.aspx" runat="server">
                    <i class="fa fas fa-cog"></i>
                    Personal Settings
                </a>
            </li>
            <li class="nav-item">
                <hr />
            </li>
            <li class="nav-item">
                <a class="nav-link" href="~/Logout.aspx" runat="server">
                    <i class="fa fas fa-logout"></i>
                    Logout
                </a>
            </li>
        </ul>
    </div>
</nav>
