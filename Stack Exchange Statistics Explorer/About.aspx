<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.About" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="General information about the Stack Exchange Statistics Explorer." />
    <link rel="canonical" href="About/" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <h3>General</h3>
    <p>
        The Stack Exchange Statistics Explorer was created to help provide public access to regularly updated data to allow for
        interested parties to examine and analyze the performance and health of all sites in the Stack Exchange network.
    </p>

    <h3>Data Acquisition</h3>
    <p>
        All data provided in this database is gathered from the Stack Exchange API at approximately 00:00:00UTC+00:00 on a daily
        basis. All data is provided as-is and is the rightful property of Stack Exchange. A list of all sites is gathered from
        the Stack Exchange API, then each site is queried for basic statistics (number of questions, answers, votes, etc.). All
        data is inserted into the database and then provided to the public at 00:30:00UTC+00:00.
    </p>

    <h3>Affiliations</h3>
    <p>
        This website is not directly affiliated with Stack Exchange or Stack Overflow. This website stands as it's own resource
        utilizing data collected from Stack Exchange. As such, use of this software is subject to the original terms of data
        collected from the Stack Exchange API.
    </p>

    <h3>Contact</h3>
    <p>
        You can contact the developer of the Stack Exchange Statistics Explorer on Twitter
        <a href="https://twitter.com/EBrown9534/">@EBrown8534</a>, or interact with us on
        <a href="https://github.com/EBrown8534/StackExchangeStatisticsExplorer">GitHub</a>.
    </p>

    <h3>Contributing</h3>
    <p>
        If you wish to contribute to the Stack Exchange Statistics Explorer project, you can do so in one of several ways:
    </p>
    <ul>
        <li>Present issues on the <a href="https://github.com/EBrown8534/StackExchangeStatisticsExplorer/issues">GitHub repository</a></li>
        <li>Submit Pull Requests to the <a href="https://github.com/EBrown8534/StackExchangeStatisticsExplorer">GitHub repository</a></li>
        <li>Spread the word on <a href="https://twitter.com">Twtter</a></li>
        <li>Spread the word on <a href="https://facebook.com">Facebook</a></li>
    </ul>
    <p class="panel">
        Disclaimer: the budget for this project is <code>$0.00 USD</code>, please do not expect any compensation from me for
        any contribution you might make. I will gladly include your name/information and a description of the contribution on
        this website somewhere, but I cannot afford to pay anyone for any involvement with this project.
    </p>
</asp:Content>
