<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Repository.aspx.cs" Inherits="GitTools.WebApp.Repository" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gwRepos" runat="server" AutoGenerateColumns="false" EnableViewState="false"
        BorderWidth="0" CellSpacing="2" CellPadding="2" GridLines="None" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="Repository" DataField="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Access URL" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <%# GetUrl(Container.DataItem) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField HeaderStyle-HorizontalAlign="Left"
                 DataNavigateUrlFormatString="ViewRepository.aspx?name={0}"  DataNavigateUrlFields="Id" Text="view"/>
<%--            <asp:HyperLinkField HeaderStyle-HorizontalAlign="Left"
                 DataNavigateUrlFormatString="ViewCommits.aspx?name={0}"  DataNavigateUrlFields="Id" Text="explore" />
--%>        </Columns>
    </asp:GridView>
    <br /><br />
    <div>
        To create new repository, enter a name:
        <asp:TextBox ID="tbCreateFolderName" runat="server" Style="margin-left: 0px" Width="280px"></asp:TextBox>&nbsp;<asp:Button
            ID="btnCreateFolder" runat="server" OnClick="btnCreateFolder_Click" Text="Create" />
    </div>
</asp:Content>
