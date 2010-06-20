<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Repository.aspx.cs" Inherits="GitTools.WebApp.Repository" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EnableViewState="false"
        BorderWidth="0" CellSpacing="2" CellPadding="2" GridLines="None" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="Folder" DataField="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Access URL" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <%# GetUrl(Container.DataItem) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
