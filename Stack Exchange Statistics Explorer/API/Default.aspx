<%@ Page Title="API Reference" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.API.Default" %>

<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="The API description for the Stack Exchange Statistics Explorer." />
    <link rel="canonical" href="API/Default/" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Stack Exchange Statistics Explorer API</h2>
    <h3>General</h3>
    <p>
        The Stack Exchange Statistics Explorer API is provided to help facilitate the integration of statistics
        for Stack Exchange sites into other applications. All data provided by this API unless otherwise noted
        is still property of Stack Exchange, and as such any use of it is subject to the Stack Exchange API
        terms of use.
    </p>
    <p>
        The number one concern of all developers when reviewing API documentation is price, and the
        rate-limits. Currently, there are no prices or rate-limits on the API, however that is subject to
        change if it begins to be abused. Data changes once a day which means that websites using this API
        should cache aggressively. Data is updated in the API at 00:30:00UTC+00:00 plus or minus several
        minutes.
    </p>
    <h3>Rate Limiting</h3>
    <p>
        The API is not currently rate-limited, however, if any abuse of it is noted then access to the API for
        the caller (or anyone for that matter) may be cutoff. There may be a rate-limit imposed at any time and
        this may be done with or without prior notice. All callers should expect to be subject to a rate limit
        and follow all expected procedures. (Acknowledge any backoff request, follow any quota limits, etc.)
    </p>
    <h3>Versions</h3>
    <asp:ListView runat="server" ID="ApiVersions">
        <LayoutTemplate>
            <div runat="server" id="groupPlaceholder"></div>
        </LayoutTemplate>

        <GroupTemplate>
            <div runat="server" id="itemPlaceholder"></div>
        </GroupTemplate>

        <ItemTemplate>
            <div runat="server">
                <a href='/API/<%#Binder.Eval<string>(Container, "Name") %>'><%#Binder.Eval<string>(Container, "Name") %></a>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
