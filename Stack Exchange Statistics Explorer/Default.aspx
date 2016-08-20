<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Stack Exchange Statistics Explorer</h1>
        <p class="lead">This website is dedicated to providing specific information regarding certain statistics on all Stack Exchange Network sites. There are currently <%:SiteCount %> sites logged in this database (<%:NonMetaSiteCount %> non-Meta sites).</p>
        <p>
            <a runat="server" href="~/Sites" class="button radius success">View the Data &raquo;</a>
            <a href="http://www.stackexchange.com" class="button radius">Visit Stack Exchange &raquo;</a>
        </p>
    </div>
</asp:Content>
