<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer._Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="The Stack Exchange Statistics Explorer reports, graphs and provides an API for analyzing statistical participation data on all Stack Exchange sites." />
    <link rel="canonical" href="/" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Stack Exchange Statistics Explorer</h1>
        <p class="lead">This website is dedicated to providing specific information regarding certain statistics on all Stack
            Exchange Network sites. There are currently <%:SiteCount %> sites logged in this database (<%:NonMetaSiteCount %>
            non-Meta sites).</p>
        <p>
            <a runat="server" href="~/Sites" class="button radius success">View the Data &raquo;</a>
            <a href="http://www.stackexchange.com" class="button radius">Visit Stack Exchange &raquo;</a>
        </p>
    </div>
    <p>
        This website is not directly supported by, endorsed with, or involved with Stack Exchange, and should be treated as
        such. Data on this website is provided for historical documentation and information purposes only, and is not intended
        to be used in a manner that relies heavily on accuracy. This data is perfect for viewing general trends of Stack
        Exchange sites over a period of time, or comparing multiple sites across any number of gathered metrics.
    </p>
    <p>
        The source code for this website is publicly available on GitHub at the following URL:
        <a href="https://github.com/EBrown8534/StackExchangeStatisticsExplorer">https://github.com/EBrown8534/StackExchangeStatisticsExplorer</a>.
    </p>
    <p>
        I'm constantly adding features and fixing bugs, so if you notice anything bizarre happening (or have a feature you
        want to see, or bug you found) feel free to report them to the
        <a href="https://github.com/EBrown8534/StackExchangeStatisticsExplorer/issues">GitHub repository</a>. I'll gladly look
        into them to determine what the best course of action is, and should have responses to them very quickly.
    </p>
    <p>
        Some features and bugs end up getting priority over others (usually bugs are top priority), so if your issue isn't
        acknowledged for a short time, feel free to hit me up on <a href="https://twitter.com/EBrown8534">Twitter</a> and I'll
        give you a run-down of what is going on with it. I'm a single developer, and while I do enjoy working on this project,
        I don't get any sort of compensation (or even acknowledgement, really) for it, which makes it tough to prioritize. (No,
        I do not want any sort of compensation, but if you would like to help out feel free to look at the
        <a runat="server" href="~/About/">About</a> page and find out if anything there fits your skillset or desires.)
    </p>
    <p>
        If you can help in ways which aren't listed on the <a runat="server" href="~/About/">About</a> page, you can contact me
        on <a href="https://twitter.com/EBrown8534">Twitter</a> and we can discuss them.
    </p>
</asp:Content>
