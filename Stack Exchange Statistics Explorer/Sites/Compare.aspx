<%@ Page Title="Compare Sites" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compare.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.Sites.Compare" %>

<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Models" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h2>Site Comparison <a runat="server" href="~/Sites/Select/" class="button success radius right">Back to Selection &raquo;</a></h2>
        <asp:ListView runat="server" ID="MainListView" OnItemDataBound="MainListView_ItemDataBound">
            <LayoutTemplate>
                <table class="medium-12">
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
                    <th><%#Binder.Eval<string>(Container, "Header") %></th>
                    <td>
                        <asp:Repeater runat="server" ID="Columns">
                            <ItemTemplate>
                                <%#Container.DataItem %></td><td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
