<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="GitTools.WebApp.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script type="text/javascript" src="Scripts/jquery-1.5.min.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
        Put content here.
    </p>
    <script type="text/javascript">

        $(function () {

            $.ajaxSetup({
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });

            $.ajax({
                url: "odata/Repositories('Test2.git')/Commits",
                success: function (data) { alert(data.d); },
                error: function (xhr) { alert(xhr.message); }
            });

        });
    </script>
</asp:Content>
