<%@ Page Title="Known Data Issues" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KnownDataIssues.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.KnownDataIssues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Known Data Issues</h2>
    <h3>Inconsistencies</h3>
    <h4>October 5, 2016</h4>
    <h5>Diagnosis</h5>
    <p>
        On 5 October 2016 at approximately 00:00UTC+00:00, the Stack Exchange API threw an error on the <code>badges/name</code> endpoint which caused the Stack Exchange Statistics Explorer Data Aggregator to crash and fail to complete it's assigned duty. The API threw an <code>Error 500: Internal Server Error</code> code and the Data Aggregator was not prepared for that. As a result, data could not be collected at the usual 00:00UTC+00:00 time.
    </p>
    <p>
        The exact request that created the first error was:
        <code class="block">
            https://api.stackexchange.com/2.2/badges/name?order=asc&sort=rank&site=stackoverflow&pagesize=100&page=1
        </code>
    </p>
    <p>
        This is documented on Stack Apps Bug Report 7064: <a href="http://stackapps.com/q/7064/">http://stackapps.com/q/7064/</a>.
    </p>
    <h5>Remediation</h5>
    <p>
        Upon discovery of the issue at approximately 06:00UTC+00:00, the aggregator was modified and re-run to collect data as soon as possible. This new API request was not sent until 06:11:03UTC+00:00.
    </p>
    <h5>Limitations</h5>
    <p>
        Data for 4 October 2016 could not be gathered at the usual 00:00UTC+00:00, and was not gathered later. As a result, all data for this date was unable to be collected, and measures are being taken to make sure that the issues created by this gap can be remedied.
    </p>
    <p>
        Data for 5 October 2016 could not be gathered at the usual 00:00UTC+00:00, but was gathered later. As such any data gathered on this date <span class="italic">may</span> be slightly skewed in relation to 3 October 2016 and 6 October 2016. This will not affect data over a long-period, provided neither endpoint of that period is 5 October 2016, or 4 October 2016. This will affect larger sites much more than smaller ones. Badge stats for this date will not be acquired for any sites.
    </p>
    <h5>Prevention</h5>
    <p>
        This Data Aggregator has been rewritten to accomodate any failures on the <code>badges/name</code> endpoint, and will now simply skip adding badge stats for the site/day when this error occurs.
    </p>
</asp:Content>
