<%@ Page Title="System Health" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemHealth.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.SystemHealth" %>

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
                        <th>Milliseconds / Request</th>
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
                <td><%#((ApiBatchLog)Container.DataItem).StartDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                <td><%#((ApiBatchLog)Container.DataItem).EndDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                <td>
                    <%#((ApiBatchLog)Container.DataItem).TimeTaken.ToString(@"h\:mm\:ss") %>
                </td>
                <td><%#((ApiBatchLog)Container.DataItem).RequestCount %></td>
                <td class='<%#((ApiBatchLog)Container.DataItem).RequestsPerSecond.GetClassOption(x => x >= 3, x => x < 2) %>'>
                    <%#((ApiBatchLog)Container.DataItem).RequestsPerSecond.ToString("0.00") %>
                </td>
                <td class='<%#((ApiBatchLog)Container.DataItem).MillisecondsPerRequest.GetClassOption(x => x <= 300, x => x > 500) %>'>
                    <%#((ApiBatchLog)Container.DataItem).MillisecondsPerRequest.ToString("0.00") %>
                </td>
                <td><%#((ApiBatchLog)Container.DataItem).SiteCount %></td>
                <td class='<%#((ApiBatchLog)Container.DataItem).MillisecondsPerSite.GetClassOption(x => x <= 1200, x => x > 1500) %>'>
                    <%#((ApiBatchLog)Container.DataItem).MillisecondsPerSite.ToString("0.00") %>
                </td>
                <td><%#((ApiBatchLog)Container.DataItem).BackoffCount %></td>
                <td class='<%#((ApiBatchLog)Container.DataItem).TotalBackoff.GetClassOption(x => x <= 70, x => x > 150) %>'>
                    <%#((ApiBatchLog)Container.DataItem).TotalBackoff %>
                </td>
                <td><%#((ApiBatchLog)Container.DataItem).EndQuotaRemaining %></td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
