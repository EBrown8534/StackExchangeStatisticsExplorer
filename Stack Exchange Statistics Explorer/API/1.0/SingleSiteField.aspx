﻿<%@ Page Title="Single Site Field Request" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SingleSiteField.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.API._1._0.SingleSiteFieldSummary" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.API._1._0.Requests" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Models" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Single Site Field Request</h2>
    <h3>URL</h3>
    <p>
        <code>/API/1.0/SingleSiteField.ashx</code>
    </p>
    <h3>Parameters</h3>
    <table class="medium-12">
        <thead>
            <tr>
                <th>Name</th>
                <th>Type</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Site</td>
                <td>Guid</td>
                <td>The GUID ID for the site.</td>
            </tr>
            <tr>
                <td>Field</td>
                <td>string</td>
                <td>The field/property to return. See <a href="#ValidPropertiesTitle">Valid Properties</a> below.</td>
            </tr>
            <tr>
                <td>DateFormat</td>
                <td>string</td>
                <td>An optional format to return each date in the <code>Gathered</code> column as. Defaults to <code><%:SingleSiteFieldRequest.DefaultDateFormat %></code>.</td>
            </tr>
            <tr>
                <td>FieldFormat</td>
                <td>string</td>
                <td>An optional format for the field to be returned in.</td>
            </tr>
        </tbody>
    </table>
    <h3>General</h3>
    <p>
        This request returns the site history for a specific <code>Site</code> with the <code>Gathered</code> dates and values for whatever extra field was specified.
    </p>
    <h3>Returns</h3>
    <table class="medium-12">
        <thead>
            <tr>
                <th>Response Property Name</th>
                <th>Type</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Gathered</td>
                <td>string</td>
                <td>The date the data was gathered in the <code>DateFormat</code> format.</td>
            </tr>
            <tr>
                <td>FieldValue</td>
                <td>string</td>
                <td>The value for the <code>Field</code> requested on the <code>Gathered</code> date in <code>FieldFormat</code>.</td>
            </tr>
        </tbody>
    </table>
    <h3 id="ValidPropertiesTitle">Valid Properties</h3>
    <asp:ListView runat="server" ID="ValidProperties">
        <LayoutTemplate>
            <table class="medium-12">
                <thead>
                    <tr>
                        <th>Property Name</th>
                        <th>Type</th>
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
                <td><%#Binder.Eval<string>(Container, "Name") %></td>
                <td><%#FormatAsSimpleType(typeof(SiteStatsCalculated).GetProperties().Where(x => x.Name == Binder.Eval<string>(Container, "Name")).First().PropertyType.ToString()) %></td>
                <td><%#typeof(SiteStatsCalculated).GetProperties().Where(x => x.Name == Binder.Eval<string>(Container, "Name")).First().GetCustomAttributes(typeof(ApiDescriptionAttribute), true).Count() > 0 ? ((ApiDescriptionAttribute)typeof(SiteStatsCalculated).GetProperties().Where(x => x.Name == Binder.Eval<string>(Container, "Name")).First().GetCustomAttributes(typeof(ApiDescriptionAttribute), true).First())?.Description : "" %></td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <h3>Examples</h3>
    <p>
        Returns the percentage of questions that remained unanswered for the site <code>Code Review</code> with dates in <code>d-MMM-yy</code> format and the values in <code>0.00%</code> format:
    </p>
    <p>
        <code class="block">
            /API/1.0/SingleSiteField.ashx?Site=<%:CodeReviewId %>&Field=UnansweredRate&DateFormat=d-MMM-yy&FieldFormat=0.00%
        </code>
    </p>
    <p>
        Example response:
    </p>
    <p>
        <code class="block">
            {"Items":[{"Gathered":"10-Apr-16","FieldValue":"6.36%"},{"Gathered":"11-Apr-16","FieldValue":"6.35%"},{"Gathered":"12-Apr-16","FieldValue":"6.33%"},{"Gathered":"13-Apr-16","FieldValue":"6.32%"},{"Gathered":"14-Apr-16","FieldValue":"6.35%"},{"Gathered":"15-Apr-16","FieldValue":"6.37%"},{"Gathered":"16-Apr-16","FieldValue":"6.40%"},{"Gathered":"17-Apr-16","FieldValue":"6.41%"},{"Gathered":"18-Apr-16","FieldValue":"6.41%"},{"Gathered":"19-Apr-16","FieldValue":"6.44%"},{"Gathered":"20-Apr-16","FieldValue":"6.45%"},{"Gathered":"21-Apr-16","FieldValue":"6.45%"},{"Gathered":"22-Apr-16","FieldValue":"6.48%"},{"Gathered":"23-Apr-16","FieldValue":"6.48%"},{"Gathered":"24-Apr-16","FieldValue":"6.49%"},{"Gathered":"25-Apr-16","FieldValue":"6.49%"},{"Gathered":"26-Apr-16","FieldValue":"6.49%"},{"Gathered":"27-Apr-16","FieldValue":"6.48%"},{"Gathered":"28-Apr-16","FieldValue":"6.50%"},{"Gathered":"29-Apr-16","FieldValue":"6.52%"}, ... ],"HasMore":false,"QuotaMax":2147483647,"QuotaRemaining":2147483647,"Backoff":null,"IsError":false}
        </code>
    </p>
</asp:Content>
