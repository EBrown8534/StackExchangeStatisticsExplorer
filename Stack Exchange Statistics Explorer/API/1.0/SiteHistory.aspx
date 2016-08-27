<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteHistory.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.API._1._0.SiteHistory1" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.API._1._0.Requests" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Models" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.API._1._0.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Site History Request</h2>
    <h3>URL</h3>
    <p>
        <code>/API/1.0/SiteHistory.ashx</code>
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
                <td>Site</td>
                <td>Guid</td>
                <td>The GUID ID for the site.</td>
            </tr>
        </tbody>
    </table>
    <h3>General</h3>
    <p>
        This request returns the site history for a specific <code>Site</code> with all properties.
    </p>
    <h3>Returns</h3>
    <asp:ListView runat="server" ID="ValidProperties">
        <LayoutTemplate>
            <table class="medium-12">
                <thead>
                    <tr>
                        <th>Property Name</th>
                        <th>Type</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    <tr runat="server" id="groupPlaceholder"></tr>
                </tbody>
            </table>
        </LayoutTemplate>

        <GroupTemplate>
            <tr runat="server" id="itemPlaceholder"></tr>
        </GroupTemplate>

        <ItemTemplate>
            <tr runat="server">
                <td><%#Binder.Eval<string>(Container, "Name") %></td>
                <td><%#FormatAsSimpleType(typeof(SiteHistoryResponseItem).GetProperties().Where(x => x.Name == Binder.Eval<string>(Container, "Name")).First().PropertyType.ToString()) %></td>
                <td><%#typeof(SiteHistoryResponseItem).GetProperties().Where(x => x.Name == Binder.Eval<string>(Container, "Name")).First().GetCustomAttributes(typeof(ApiDescriptionAttribute), true).Count() > 0 ? ((ApiDescriptionAttribute)typeof(SiteHistoryResponseItem).GetProperties().Where(x => x.Name == Binder.Eval<string>(Container, "Name")).First().GetCustomAttributes(typeof(ApiDescriptionAttribute), true).First())?.Description : "" %></td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
