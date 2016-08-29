<%@ Page Title="Sites" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sites.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.API._1._0.Sites1" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Sites Request</h2>
    <h3>URL</h3>
    <p>
        <code>/API/1.0/Sites.ashx</code>
    </p>
    <h3>Parameters</h3>
    <table class="medium-12">
        <thead>
            <tr>
                <th>Name</th>
                <th>Type</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td colspan="3" class="grey center">None</td>
            </tr>
        </tbody>
    </table>
    <h3>General</h3>
    <p>
        This request returns all sites in the database.
    </p>
</asp:Content>
