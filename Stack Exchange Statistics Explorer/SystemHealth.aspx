<%@ Page Title="System Health" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemHealth.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.SystemHealth" %>

<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>
<%@ Import Namespace="Evbpc.Framework.Utilities.Extensions" %>
<%@ Import Namespace="Evbpc.Framework.Utilities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>System Health</h2>
    <h3>Data Store Health</h3>
    <h4>Primary Database</h4>
    <table class="medium-12">
        <thead>
            <tr>
                <th>Table Name</th>
                <th>Created</th>
                <th>Last Updated</th>
                <th>Row Count</th>
                <th>Index Count</th>
                <th>Total Size</th>
                <th>Used Space</th>
                <th>Used Space / Row</th>
                <th>Rows Added per Day</th>
                <th>Space Used per Day</th>
            </tr>
        </thead>
        <tbody>
            <asp:ListView runat="server" ID="CoreTableSizeResults">
                <LayoutTemplate>
                    <tr runat="server" id="groupPlaceholder"></tr>
                </LayoutTemplate>

                <GroupTemplate>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </GroupTemplate>

                <ItemTemplate>
                    <tr runat="server">
                        <td><%#((TableInfo)Container.DataItem).FullTableName %></td>
                        <td><%#((TableInfo)Container.DataItem).Created.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        <td><%#((TableInfo)Container.DataItem).Modified.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        <td><%#((TableInfo)Container.DataItem).RowCount.ToString("n0") %></td>
                        <td><%#((TableInfo)Container.DataItem).IndexCount %></td>
                        <td><%#((TableInfo)Container.DataItem).TotalSpace.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></td>
                        <td><%#((TableInfo)Container.DataItem).UsedSpace.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></td>
                        <td><%#((TableInfo)Container.DataItem).UsedSpacePerRow.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></td>
                        <td><%#((TableInfo)Container.DataItem).RowsPerDay.ToString("0.00") %></td>
                        <td><%#(((TableInfo)Container.DataItem).UsedSpacePerRow * ((TableInfo)Container.DataItem).RowsPerDay).GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <tr>
                <th><%:CoreTableSizeTotals.FullTableName %></th>
                <th></th>
                <th></th>
                <th><%:CoreTableSizeTotals.RowCount.ToString("n0") %></th>
                <th><%:CoreTableSizeTotals.IndexCount %></th>
                <th><%:CoreTableSizeTotals.TotalSpace.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></th>
                <th><%:CoreTableSizeTotals.UsedSpace.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></th>
                <th><%:CoreTableSizeTotals.UsedSpacePerRow.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></th>
                <th></th>
                <th></th>
            </tr>
        </tbody>
    </table>
    <h4>Production Database</h4>
    <table class="medium-12">
        <thead>
            <tr>
                <th>Table Name</th>
                <th>Created</th>
                <th>Last Updated</th>
                <th>Row Count</th>
                <th>Index Count</th>
                <th>Total Size</th>
                <th>Used Space</th>
                <th>Used Space / Row</th>
                <th>Rows Added per Day</th>
                <th>Space Used per Day</th>
            </tr>
        </thead>
        <tbody>
            <asp:ListView runat="server" ID="WebsiteTableSizeResults">
                <LayoutTemplate>
                    <tr runat="server" id="groupPlaceholder"></tr>
                </LayoutTemplate>

                <GroupTemplate>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </GroupTemplate>

                <ItemTemplate>
                    <tr runat="server">
                        <td><%#((TableInfo)Container.DataItem).FullTableName %></td>
                        <td><%#((TableInfo)Container.DataItem).Created.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        <td><%#((TableInfo)Container.DataItem).Modified.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        <td><%#((TableInfo)Container.DataItem).RowCount.ToString("n0") %></td>
                        <td><%#((TableInfo)Container.DataItem).IndexCount %></td>
                        <td><%#((TableInfo)Container.DataItem).TotalSpace.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></td>
                        <td><%#((TableInfo)Container.DataItem).UsedSpace.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></td>
                        <td><%#((TableInfo)Container.DataItem).UsedSpacePerRow.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></td>
                        <td><%#((TableInfo)Container.DataItem).RowsPerDay.ToString("0.00") %></td>
                        <td><%#(((TableInfo)Container.DataItem).UsedSpacePerRow * ((TableInfo)Container.DataItem).RowsPerDay).GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <tr>
                <th><%:WebsiteTableSizeTotals.FullTableName %></th>
                <th></th>
                <th></th>
                <th><%:WebsiteTableSizeTotals.RowCount.ToString("n0") %></th>
                <th><%:WebsiteTableSizeTotals.IndexCount %></th>
                <th><%:WebsiteTableSizeTotals.TotalSpace.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></th>
                <th><%:WebsiteTableSizeTotals.UsedSpace.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></th>
                <th><%:WebsiteTableSizeTotals.UsedSpacePerRow.GetLargestWholeSize(SizeUnit.Bytes).ToString("0.00") %></th>
                <th></th>
                <th></th>
            </tr>
        </tbody>
    </table>
    <h3>Data Gathering Health</h3>
    <asp:ListView runat="server" ID="ApiLogResults">
        <LayoutTemplate>
            <table class="medium-12">
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
                <td class='<%#((ApiBatchLog)Container.DataItem).StartDateTime.GetClassOption(x => x.TimeOfDay <= new TimeSpan(0, 1, 0), x => x.TimeOfDay > new TimeSpan(0, 1, 30)) %>'>
                    <%#((ApiBatchLog)Container.DataItem).StartDateTime.ToString("yyyy-MM-dd HH:mm:ss") %>
                </td>
                <td class='<%#((ApiBatchLog)Container.DataItem).EndDateTime.GetClassOption(x => x.TimeOfDay <= new TimeSpan(0, 15, 0), x => x.TimeOfDay > new TimeSpan(0, 20, 0)) %>'>
                    <%#((ApiBatchLog)Container.DataItem).EndDateTime.ToString("yyyy-MM-dd HH:mm:ss") %>
                </td>
                <td class='<%#((ApiBatchLog)Container.DataItem).TimeTaken.GetClassOption(x => x <= new TimeSpan(0, 10, 0), x => x > new TimeSpan(0, 15, 0)) %>'>
                    <%#((ApiBatchLog)Container.DataItem).TimeTaken.ToString(@"h\:mm\:ss") %>
                </td>
                <td class='<%#((ApiBatchLog)Container.DataItem).RequestCount.GetClassOption(x => x < 7500, x => x > 2000) %>'>
                    <%#((ApiBatchLog)Container.DataItem).RequestCount %>
                </td>
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
                <td class='<%#((ApiBatchLog)Container.DataItem).BackoffCount.GetClassOption(x => x <= 6, x => x > 9) %>'>
                    <%#((ApiBatchLog)Container.DataItem).BackoffCount %>
                </td>
                <td class='<%#((ApiBatchLog)Container.DataItem).TotalBackoff.GetClassOption(x => x <= 60, x => x > 90) %>'>
                    <%#((ApiBatchLog)Container.DataItem).TotalBackoff %>
                </td>
                <td class='<%#((ApiBatchLog)Container.DataItem).EndQuotaRemaining.GetClassOption(x => x >= 2000, x => x < 1500) %>'>
                    <%#((ApiBatchLog)Container.DataItem).EndQuotaRemaining %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
