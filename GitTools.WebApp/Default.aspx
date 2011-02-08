<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="GitTools.WebApp._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Git Web Access
    </h2>
    <p>
        This site provides web access to Git repositories. To start use this site,</p>
    <ol>
        <li>Please make sure <a href="http://code.google.com/p/msysgit/downloads/list">Git for Windows (1.7+)</a> is installed and enter the path of git executable 
            (git.exe)</li>
        <li>Please enter your git repositories' root folder. It is the parent folder of all repositories.</li>
    </ol>
    <table cellpadding="4" cellspacing="4">
        <tr>
            <td>
                Git Executable Path:
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="380px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Git Repository Root Folder:
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Width="380px"></asp:TextBox>
            </td>
        </tr>

                <tr>
            <td>
                
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save" />
            </td>
        </tr>
    </table>
    <p>Once the above information is saved, you can start to <a href="Repository.aspx">manage your repositories</a>, 
        or <a href="Help.aspx">read help</a>.</p>
</asp:Content>
