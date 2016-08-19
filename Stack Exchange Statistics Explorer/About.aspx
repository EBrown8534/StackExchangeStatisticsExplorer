<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.About" %>

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
</asp:Content>
