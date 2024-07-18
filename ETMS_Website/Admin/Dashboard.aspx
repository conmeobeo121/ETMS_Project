<%@ Page Title="Admin - Dashboard" Language="C#" MasterPageFile="~/Admin/SiteAdmin.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ETMS_Website.Admin.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteAdminHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteAdminContentPlaceHolder" runat="server">
    <main aria-labelledby="title" class="container-fluid w-100 h-100 mh-100-vh d-flex flex-column">
        <div class="row flex-take-1_5">
            <h1 id="title">Dashboard</h1>
        </div>
        <div class="row flex-take-8_5 overflow-scroll position-relative">
            <div class="col-md-6">
                <h3>Event Types</h3>
                <asp:Chart ID="EventTypeChart" runat="server" Width="600px" Height="400px">
                    <Series>
                        <asp:Series Name="Series1" ChartType="Column"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
            <%--<div class="col-md-6">
                <h3>Event Types</h3>
                <asp:Chart ID="Chart1" runat="server" Width="600px" Height="400px">
                    <Series>
                        <asp:Series Name="Series1" ChartType="Pie"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>--%>
        </div>
    </main>
</asp:Content>
