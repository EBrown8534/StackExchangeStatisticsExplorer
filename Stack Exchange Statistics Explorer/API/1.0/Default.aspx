<%@ Page Title="V1.0 Reference" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.API._1._0.Default" %>

<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Version 1.0</h2>
    <h3>Shared Parameters</h3>
    <p>
        All API Version 1.0 endpoints share the following parameters (specified in the query string):
    </p>
    <table class="medium-12">
        <thead>
            <tr>
                <th>Name</th>
                <th>Valid Values</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>FileType</td>
                <td><code>json</code>, <code>xml</code>, <code>tsv</code>, <code>csv</code>, <code>psv</code></td>
                <td>Specifies what type of data format should be returned.</td>
            </tr>
        </tbody>
    </table>
    <h3>FileType Descriptions</h3>
    <table class="medium-12">
        <thead>
            <tr>
                <th>Value</th>
                <th>Name</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>json</td>
                <td>Javascript Object Notation</td>
                <td>
                    A graph object that maps property names to object values, including all sub-properties and collections.<br />
                    <code class="block">{"property":"value","property2":{"property2.property":"value"}}</code>
                </td>
            </tr>
            <tr>
                <td>xml</td>
                <td>Extensible Markup Language</td>
                <td>
                    A graph object that maps property names to object values, including all sub-properties and collections.<br />
                    <code class="block"><pre>&lt;?xml version="1.0" encoding="utf-16"?&gt;<br />&lt;RootElement&gt;<br />&lt;Property&gt;Value&lt;/Property&gt;<br />&lt;Property2&gt;<br />&lt;Property2.Property&gt;Value&lt;/Property2.Property&gt;<br />&lt;/Property2&gt;<br />&lt;/RootElement&gt;</pre></code>
                </td>
            </tr>
            <tr>
                <td>csv</td>
                <td>Comma-Separated Values</td>
                <td>
                    Returns the Items of the response in a delimited format using new lines (<code>\r\n</code>) as row/record delimiters and literal commas (<code>,</code>) as column/field delimiters. Collections and sub-objects will not be returned. Names and values containing a literal comma (<code>,</code>) will be escaped with the unicode sequence for a comma: <code>\u002C</code>.<br />
                    <code class="block">Property,Property2<br />Value,Value2</code>
                </td>
            </tr>
            <tr>
                <td>tsv</td>
                <td>Tab-Separated Values</td>
                <td>
                    Returns the Items of the response in a delimited format using new lines (<code>\r\n</code>) as row/record delimiters and literal tabs (<code>\t</code>) as column/field delimiters. Collections and sub-objects will not be returned. Names and values containing a literal tab (<code>\t</code>) will be escaped with the escape sequence for a tab: <code>\t</code>.<br />
                    <code class="block">Property&nbsp;&nbsp;&nbsp;&nbsp;Property2<br />Value&nbsp;&nbsp;&nbsp;Value2</code>
                </td>
            </tr>
            <tr>
                <td>psv</td>
                <td>Pipe-Separated Values</td>
                <td>
                    Returns the Items of the response in a delimited format using new lines (<code>\r\n</code>) as row/record delimiters and literal pipes (<code>|</code>) as column/field delimiters. Collections and sub-objects will not be returned. Names and values containing a literal pipe (<code>|</code>) will be escaped with the unicode sequence for a pipe: <code>\u007C</code>.<br />
                    <code class="block">Property|Property2<br />Value|Value2</code>
                </td>
            </tr>
        </tbody>
    </table>
    <h3>Response</h3>
    <p>
        All API Version 1.0 endpoint responses are wrapped in a common "wrapper" object (excepting TSV/CSV/PSV
        file formats). This wrapper object has the following properties:
    </p>
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
                <td>HasMore</td>
                <td>bool</td>
                <td>If <code>true</code>, then the request did not return all the <code>Items</code>.</td>
            </tr>
            <tr>
                <td>Backoff</td>
                <td>int?</td>
                <td>If not-<code>null</code>, then it is expected that the caller will not call the same endpoint again until after the provided number of seconds.</td>
            </tr>
            <tr>
                <td>IsError</td>
                <td>bool</td>
                <td>If <code>true</code>, then the response was an error response.</td>
            </tr>
            <tr>
                <td>QuotaMax</td>
                <td>int</td>
                <td>Indicates how many API calls are permitted for the current caller.</td>
            </tr>
            <tr>
                <td>QuotaRemaining</td>
                <td>int</td>
                <td>Indicates how many API calls are remaining for the current caller.</td>
            </tr>
            <tr>
                <td>Items</td>
                <td>List&lt;T&gt;</td>
                <td>A list of all the items returned by the API call. The type of this list depends on the endpoint.</td>
            </tr>
        </tbody>
    </table>
    <h3>Endpoints</h3>
    <asp:ListView runat="server" ID="VersionMethods">
        <LayoutTemplate>
            <div runat="server" id="groupPlaceholder"></div>
        </LayoutTemplate>

        <GroupTemplate>
            <div runat="server" id="itemPlaceholder"></div>
        </GroupTemplate>

        <ItemTemplate>
            <div runat="server">
                <a href='<%#Binder.Eval<string>(Container, "File") %>'>
                    <%#Binder.Eval<string>(Container, "Name") %>
                </a>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
