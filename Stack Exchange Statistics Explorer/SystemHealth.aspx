﻿<%@ Page Title="System Health" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemHealth.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.SystemHealth" %>

<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>
<%@ Import Namespace="Evbpc.Framework.Utilities.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>System Health</h2>
    <h3>Data Gathering Health</h3>
    <asp:ListView runat="server" ID="ApiLogResults">
        <LayoutTemplate>
            <table>
                <thead>
                    <tr>
                        <th>Start</th>
                        <th>End</th>
                        <th>Time Taken</th>
                        <th>Total Requests</th>
                        <th>Requests / Second</th>
                        <th>Sites Loaded</th>
                        <th>Milliseconds / Site</th>
                        <th>Backoff Count</th>
                        <th>Backoff Seconds</th>
                        <th>End Quota</th>
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
                <td><%#((ApiBatchLog)Container.DataItem).StartDateTime.ToString("yyyy-MM-dd HH:mm:ss.fffffff") %></td>
                <td><%#((ApiBatchLog)Container.DataItem).EndDateTime.ToString("yyyy-MM-dd HH:mm:ss.fffffff") %></td>
                <td>
                    <%#((ApiBatchLog)Container.DataItem).TimeTaken %>
                </td>
                <td><%#((ApiBatchLog)Container.DataItem).RequestCount %></td>
                <td class='<%#new Dictionary<string, Predicate<double>> { ["good"] = x => x >= 3, ["neutral"] = x => x > 2, ["bad"] = x => true }.FindKey(((ApiBatchLog)Container.DataItem).RequestsPerSecond) %>'>
                    <%#((ApiBatchLog)Container.DataItem).RequestsPerSecond.ToString("0.00") %>
                </td>
                <td><%#((ApiBatchLog)Container.DataItem).SiteCount %></td>
                <td class='<%#new Dictionary<string, Predicate<double>> { ["good"] = x => x <= 1200, ["neutral"] = x => x <= 1500, ["bad"] = x => true }.FindKey(((ApiBatchLog)Container.DataItem).MillisecondsPerSite) %>'>
                    <%#((ApiBatchLog)Container.DataItem).MillisecondsPerSite.ToString("0.00") %>
                </td>
                <td><%#((ApiBatchLog)Container.DataItem).BackoffCount %></td>
                <td class='<%#new Dictionary<string, Predicate<int>> { ["good"] = x => x <= 90, ["neutral"] = x => x <= 150, ["bad"] = x => true }.FindKey(((ApiBatchLog)Container.DataItem).TotalBackoff) %>'>
                    <%#((ApiBatchLog)Container.DataItem).TotalBackoff %>
                </td>
                <td><%#((ApiBatchLog)Container.DataItem).EndQuotaRemaining %></td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
