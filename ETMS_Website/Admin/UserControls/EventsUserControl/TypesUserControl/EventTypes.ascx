<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventTypes.ascx.cs" Inherits="ETMS_Website.Admin.EventsUserControl.EventTypes" %>


<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:GridView runat="server" ID="gvEventTypes" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnRowDeleting="gvEventTypes_RowDeleting" OnRowEditing="gvEventTypes_RowEditing" OnRowUpdating="gvEventTypes_RowUpdating" OnRowCancelingEdit="gvEventTypes_RowCancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="typeID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TypeID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# Eval("TypeName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditTypeName" runat="server" Text='<%# Bind("TypeName") %>' CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEditTypeName" runat="server"
                            ControlToValidate="txtEditTypeName"
                            ErrorMessage="Event type cannot be empty."
                            Display="Dynamic"
                            ForeColor="Red"
                            ValidationGroup="EditGroup">
                             </asp:RequiredFieldValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <div class="custom-footer">
                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-secondary custom-button" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger custom-button" />
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="custom-footer">
                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" ValidationGroup="EditGroup" CssClass="btn btn-primary custom-button" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="False" CssClass="btn btn-secondary custom-button" />
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="show-flex">
            <asp:TextBox ID="txtEventType" runat="server" Placeholder="Type type of event here..." CssClass="w-100" />
        </div>
        <asp:Button ID="btnAddTypeEvent" runat="server" Text="Add New Type" CssClass="btn btn-primary" OnClick="btnAddTypeEvent_Click" ValidationGroup="groupTypeEvent" />
        <asp:RequiredFieldValidator runat="server" ID="rfvEventType" ControlToValidate="txtEventType" ErrorMessage="Event type cannot be empty." Display="Dynamic" ForeColor="Red" ValidationGroup="groupTypeEvent" />
    </ContentTemplate>
</asp:UpdatePanel>

