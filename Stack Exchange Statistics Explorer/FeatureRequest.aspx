<%@ Page Title="Feature Request" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeatureRequest.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.FeatureRequest" %>

<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="small-12 medium-12 columns">
        <h3>Proposed Features</h3>
        <p>
            Note: as time permits, features proposed here will be ported to the <a href="https://github.com/EBrown8534/StackExchangeStatisticsExplorer">GitHub Repository</a>. It is preferred to propose features and report bugs as <a href="https://github.com/EBrown8534/StackExchangeStatisticsExplorer/issues">issues</a> there.
        </p>
        <asp:ListView runat="server" ID="Requests">
            <LayoutTemplate>
                <table class="medium-12">
                    <thead>
                        <tr>
                            <th>Priority</th>
                            <th>Area</th>
                            <th>Status</th>
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
                    <td><%#Binder.Eval<string>(Container, "PriorityString") %></td>
                    <td><%#Binder.Eval<string>(Container, "Area") %></td>
                    <td><%#Binder.Eval<string>(Container, "Status") %></td>
                    <td><%#Binder.Eval<string>(Container, "Description") %></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <h3>Propose Feature / Report Bug</h3>
        <p>
            For bug reports, simply describe the issue as if it were a feature request to fix the bug.
        </p>
    </div>
    <div class='small-12 medium-12 columns alert-box radius <%:Message.Text.StartsWith("Error:") ? "alert" : "success" %> <%:Message.Text.Length == 0 ? "hidden" : "" %>'>
        <asp:Literal runat="server" ID="Message"></asp:Literal>
    </div>
    <div class="small-6 medium-3 columns">
        Proposed By (Optional):
        <asp:TextBox ID="ProposedBy" runat="server" CssClass="radius"></asp:TextBox>
    </div>
    <div class="small-6 medium-3 columns">
        Area (Optional): 
        <asp:TextBox ID="Area" runat="server" CssClass="radius"></asp:TextBox>
    </div>
    <div class="small-12 medium-6 columns">
    </div>
    <div class="small-12 medium-12 columns">
        Description (Required):
        <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" CssClass="radius"></asp:TextBox>
    </div>
    <p class="small-12 columns small">
        Note: to prevent abuse, feature requests are not automatically added. They are first manually reviewed to determine legitimacy.
    </p>
    <div class="small-12 columns">
        <asp:Button ID="Submit" runat="server" CssClass="button radius" Text="Submit" OnClick="Submit_Click" />
    </div>
</asp:Content>
