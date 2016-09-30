<%@ Page Title="Last Update Request" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LastUpdate.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.API._1._0.LastUpdate1" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Last Update Request</h2>
    <h3>URL</h3>
    <p>
        <code>/API/1.0/LastUpdate.ashx</code>
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
                <td colspan="3" class="center grey">None</td>
            </tr>
        </tbody>
    </table>
    <h3>General</h3>
    <p>
        This request returns the last time the database was updated for a specific site or sites.
    </p>
    <h3>Returns</h3>
    <table class="medium-12">
        <thead>
            <tr>
                <th>Response Property Name</th>
                <th>Type</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Id</td>
                <td>Guid</td>
                <td>The primary key for the site being returned.</td>
            </tr>
            <tr>
                <td>Updated</td>
                <td>DateTime</td>
                <td>The date and time the site was last updated in the database.</td>
            </tr>
        </tbody>
    </table>
    <h3>Examples</h3>
    <p>
        <code class="block">
            /API/1.0/LastUpdate.ashx
        </code>
    </p>
    <p>
        Example response:
    </p>
    <p>
        <code class="block">
            {"Items":[{"Id":"3c9d37da-6dff-e511-80c0-00155d918203","Updated":"2016-09-30 00:00:51.3892417"},{"Id":"3e9d37da-6dff-e511-80c0-00155d918203","Updated":"2016-09-30 00:01:06.2643575"}, ... ],"HasMore":false,"QuotaMax":2147483647,"QuotaRemaining":2147483647,"Backoff":null,"IsError":false}
        </code>
    </p>
</asp:Content>
