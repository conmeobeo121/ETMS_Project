<%@ Page Title="Cart" Language="C#" MasterPageFile="~/SiteUser.master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ETMS_Website.User.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteUserHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteUserContentPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="h-100">
                <div class="container h-100 py-5">
                    <div class="row d-flex justify-content-center align-items-center h-100">
                        <div class="col-10">
                            <div class="d-flex justify-content-between align-items-center mb-4">
                                <h3 class="fw-normal mb-0">Shopping Cart</h3>
                            </div>
                            <div class="w-100 d-flex justify-content-center align-content-center" id="lbEmptyCart" runat="server" visible="false">
                                <strong>No products, go shopping now
                                </strong>
                            </div>                            
                            <asp:Repeater runat="server" ID="rfCarts">
                                <ItemTemplate>
                                    <div class="card rounded-3 mb-4">
                                        <div class="card-body p-4">
                                            <div class="row d-flex justify-content-between align-items-center">
                                                <div class="col-md-3 col-lg-3 col-xl-3">
                                                    <asp:Label runat="server" CssClass="lead fw-normal mb-2" Text='<%# Eval("TypeName") %>' />
                                                    <br />
                                                    <p>
                                                        <span class="text-muted">Event Name: </span><%# Eval("EventName") %>
                                                    </p>
                                                    <p>
                                                        <span class="text-muted">Start Sell: </span><%# Eval("StartSell") %>
                                                    </p>
                                                    <p>
                                                        <span class="text-muted">End Sell: </span><%# Eval("EndSell") %>
                                                    </p>
                                                </div>
                                                <div class="col-md-3 col-lg-3 col-xl-2 d-flex">
                                                    <asp:HiddenField runat="server" ID="hdnIDType" Value='<%# Eval("TypeID") %>' />
                                                    <asp:TextBox runat="server" ID="txtQuantity" CssClass="form-control form-control-sm" TextMode="Number" Text='<%# ((Dictionary<int, int>)Session["cartData"])[(int)Eval("TypeID")].ToString()  %>' ReadOnly='<%# ((DateTime)Eval("EndSell")) < DateTime.Now ? true : false %>' AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged" />
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvCheckQuantity" Display="Dynamic" ControlToValidate="txtQuantity" ValidationGroup="checkQuantityValid" ForeColor="Red" ErrorMessage="Quantity cannot be null" />
                                                    <asp:CompareValidator runat="server" ID="cvCheckQuantity" Display="Dynamic" ControlToValidate="txtQuantity" ValidationGroup="checkQuantityValid" ValueToCompare="0" Operator="GreaterThan" ForeColor="Red" ErrorMessage="Quantity must be greater than zero." />
                                                </div>
                                                <div class="col-md-3 col-lg-2 col-xl-2 offset-lg-1">
                                                    <asp:Label runat="server" ID="lbPrice" CssClass="h5 mb-0" Text='<%# ((Dictionary<int, int>)Session["cartData"])[(int)Eval("TypeID")] * (int)Eval("Price") %>' />
                                                </div>
                                                <div class="col-md-1 col-lg-1 col-xl-1 text-end">
                                                    <button class="text-danger border-0"><i class="fas fa-trash fa-lg"></i></button>
                                                    <asp:Button runat="server" ID="btnDelete" CommandArgument='<%# Container.ItemIndex  %>' OnClick="btnDelete_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <div class="card mb-4">
                                <div class="card-body p-4 d-flex flex-row">
                                    <div class="form-outline flex-fill">
                                        <asp:TextBox runat="server" type="text" ID="txtDiscount" CssClass="form-control form-control-lg" Placeholder="Discount code" />
                                    </div>
                                    <button type="button" class="btn btn-outline-warning btn-lg ms-3">Apply</button>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-body">
                                    <asp:Button runat="server" ID="btnProcessToPay" CssClass="btn btn-warning btn-lg w-100" Text="Proceed to Pay" ValidationGroup="checkQuantityValid" OnClick="btnProcessToPay_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
