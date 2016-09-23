<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer._Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="The Stack Exchange Statistics Explorer reports, graphs and provides an API for analyzing statistical participation data on all Stack Exchange sites." />
    <link rel="canonical" href="Default/" />
    <link rel="canonical" href="Default.aspx" />
    <link rel="canonical" href="Default" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Stack Exchange Statistics Explorer</h1>
        <p class="lead">This website is dedicated to providing specific information regarding certain statistics on all Stack Exchange Network sites. There are currently <%:SiteCount %> sites logged in this database (<%:NonMetaSiteCount %> non-Meta sites).</p>
        <p>
            <a runat="server" href="~/Sites" class="button radius success">View the Data &raquo;</a>
            <a href="http://www.stackexchange.com" class="button radius">Visit Stack Exchange &raquo;</a>
        </p>
    </div>
    <p>
        The source code for this website is publicly avaialable on GitHub at the following URL: <a href="https://github.com/EBrown8534/StackExchangeStatisticsExplorer">https://github.com/EBrown8534/StackExchangeStatisticsExplorer</a>.
    </p>
</asp:Content>
