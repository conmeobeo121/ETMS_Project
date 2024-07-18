<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageManagements.ascx.cs" Inherits="ETMS_Website.Admin.EventsUserControl.ImagesUserControl.ImageManagements" %>

<asp:UpdatePanel runat="server" ID="upImgManagements">
    <ContentTemplate>
        <asp:HiddenField ID="hfScrollHeight" runat="server" />
        <input type="text" id="hdnSvHeightHidden" hidden value="<%: hfScrollHeight.ClientID %>" />
        <asp:GridView runat="server" ID="gvImages" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" OnRowEditing="gvImages_RowEditing" OnRowDeleting="gvImages_RowDeleting">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnImageID" Value='<%#  DataBinder.Eval(Container.DataItem, "ImageID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Event Name">
                    <ItemTemplate>
                        <%# Eval("EventName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <div class="d-flex justify-content-center">
                            <asp:Image runat="server" ID="imgLink" ImageUrl='<%# Eval("ImageUrl") %>' Width="200" Height="200" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <div class="custom-footer">
                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-secondary custom-button" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger custom-button" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button runat="server" ID="btnAddImg" Text="Add New Image" OnClick="btnAddImg_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
