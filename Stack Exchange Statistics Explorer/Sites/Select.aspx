<%@ Page Title="Site Selection" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Select.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.Sites.Select" %>

<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Models" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="A list of all sites in the Stack Exchange Statistics Explorer." />
    <link rel="canonical" href="Sites/Select/" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Multiple Site Selection</h2>
    <div class="compare-panel panel radius text-right">
        <span id="site-count">0</span> sites selected<br />
        <asp:Button runat="server" CssClass="button right radius success" Text="Compare Sites &raquo;" OnClick="CompareSites_Click" />
    </div>
    <p>
        Select all the sites you wish to compare together below.
    </p>
    <div class="row collapse">
        <div class="small-12 medium-4 columns right">
            <label class="right inline">
                <span id="display"><%:SiteCount %></span> / <span id="total"><%:SiteCount %></span> sites shown
            </label>
        </div>
        <div class="small-2 medium-1 columns">
            <label for="filterDropDown" class="prefix radius">Filter by</label>
        </div>
        <div class="small-3 medium-2 large-2 columns">
            <select id="filterDropDown" onchange="javascript:filterItems()">
                <option value="name">Name</option>
                <option value="apiParameter">API Parameter</option>
                <option value="state">State</option>
                <option value="type">Type</option>
                <option value="id">ID</option>
            </select>
        </div>
        <div class="small-2 medium-2 large-1 columns">
            <label for="filterTextBox" class="postfix prefix">contains</label>
        </div>
        <div class="small-3 medium-2 large-3 columns">
            <input type="text" id="filterTextBox" name="filterTextBox" onkeyup="javascript:filterItems()" />
        </div>
        <div class="small-2 medium-1 columns">
            <a class="button postfix radius" id="filterButton" href="javascript:filterItems()">Filter</a>
        </div>
    </div>
    <div class="row collapse">
        <div class="small-2 medium-1 columns">
            <label for="sortProperty" class="prefix radius">Sort by</label>
        </div>
        <div class="small-3 medium-2 columns">
            <select id="sortProperty" onchange="javascript:sortItems()">
                <option value="updateTime">Default</option>
                <option value="name">Name</option>
                <option value="apiParameter">API Parameter</option>
                <option value="state">State</option>
                <option value="type">Type</option>
                <option value="id">ID</option>
                <option value="totalQuestions">Total Questions</option>
                <option value="totalAnswers">Total Answers</option>
                <option value="answerRate">Answer Rate</option>
            </select>
        </div>
        <div class="small-1 medium-1 columns">
            <span class="prefix postfix">in</span>
        </div>
        <div class="small-3 medium-2 columns">
            <select id="sortOrder" onchange="javascript:sortItems()" class="prefix postfix">
                <option value="asc">Ascending</option>
                <option value="desc">Descending</option>
            </select>
        </div>
        <div class="small-1 medium-1 columns">
            <label for="sortOrder" class="prefix postfix">order</label>
        </div>
        <div class="small-2 medium-1 columns">
            <a id="sortButton" class="button postfix radius" href="javascript:sortItems()">Sort</a>
        </div>
        <div class="small-12 medium-4 columns">
        </div>
    </div>

    <script type="text/javascript">
        $("#filterTextBox").keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
            }
        });

        function filterItems() {
            var text = $("#filterTextBox").val().toLowerCase();
            var prop = $("#filterDropDown").val();

            $(".site").each(function (el) { $(this).css("display", ""); });

            var d = 0;
            var t = 0;

            $(".site").each(function (el) {
                if ($("." + prop, this).text().toLowerCase().indexOf(text) === -1) {
                    $(this).css("display", "none");
                } else {
                    d++;
                }

                t++;
            });

            $("#display").text(d);
            $("#total").text(t);
        }

        function sortItems() {
            var prop = $("#sortProperty").val();
            var orderVal = $("#sortOrder").val();

            var options = {
                valueNames: ["id", "name", "apiParameter", "totalQuestions", "totalAnswers", "answerRate", "updateTime", "state", "type"]
            };

            var list = new List("sites", options);

            list.sort(prop, { order: orderVal });
        }

        var intlFormat = new Intl.NumberFormat();
    </script>

    <asp:ListView runat="server" ID="SiteDisplay">
        <LayoutTemplate>
            <div class="row sites" id="sites">
                <div class="list">
                    <div runat="server" id="groupPlaceholder"></div>
                </div>
            </div>
        </LayoutTemplate>

        <GroupTemplate>
            <div runat="server" id="itemPlaceholder"></div>
        </GroupTemplate>

        <ItemTemplate>
            <div runat="server" class="large-4 medium-4 site small columns">
                <div class="radius panel">
                    <h3>
                        <small class="switch radius small right">
                            <input id='select-<%#Binder.Eval<Site>(Container, "Site").Id %>' name='select-<%#Binder.Eval<Site>(Container, "Site").Id %>' type="checkbox" />
                            <label for='select-<%#Binder.Eval<Site>(Container, "Site").Id %>'></label>
                        </small>
                        <img class="image-32" src="<%#Binder.Eval<Site>(Container, "Site").HighResolutionIconUrl ?? Binder.Eval<Site>(Container, "Site").IconUrl %>" alt="<%#Binder.Eval<Site>(Container, "Site").Name %>" />
                        <span class="name">
                            <%#Binder.Eval<Site>(Container, "Site").Name.Length > 0 ? Binder.Eval<Site>(Container, "Site").Name : "-" %>
                        </span>
                        <%--<a href='<%#Binder.Eval<Site>(Container, "Site").SiteUrl %>' target="_blank">View</a>--%>
                    </h3>
                    <p>
                        API Site Parameter: <span class="apiParameter"><%#Binder.Eval<Site>(Container, "Site").ApiSiteParameter %></span>
                        <div class="hidden">
                            <span class="updateTime hidden"><%#Binder.Eval<Site>(Container, "Site").FirstUpdate.Ticks %></span>
                            <span class="hidden id"><%#Binder.Eval<Site>(Container, "Site").Id.ToString("d") %></span>
                            Current State: <span class="state"><%#Binder.Eval<Site>(Container, "Site").HumanizeState %></span><br />
                            Type: <span class="type"><%#Binder.Eval<Site>(Container, "Site").HumanizeType %></span><br />
                            <%#Binder.Eval<Site>(Container, "Site").DateTitle %> Date: <%#(Binder.Eval<Site>(Container, "Site")).LastEffectiveDateTime?.ToLongDateString() %><br />
                            Total Questions: <span class="totalQuestions hidden"><%#Binder.Eval<int>(Container, "TotalQuestions")%></span><%#Binder.Eval<int>(Container, "TotalQuestions").ToString("n0") %><br />
                            Total Answers: <span class="totalAnswers hidden"><%#Binder.Eval<int>(Container, "TotalAnswers")%></span><%#Binder.Eval<int>(Container, "TotalAnswers").ToString("n0") %><br />
                            Answer Rate: <span class="answerRate"><%#(((Binder.Eval<int>(Container, "TotalQuestions") - Binder.Eval<int>(Container, "TotalUnanswered")) / (double)Binder.Eval<int>(Container, "TotalQuestions")) * 100).ToString("0.00") %></span>%
                        </div>
                    </p>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>

    <script type="text/javascript">
        $(".site .switch input").change(function (event) {
            if (this.checked) {
                $("#site-count").text(parseInt($("#site-count").text()) + 1);
            }
            else {
                $("#site-count").text(parseInt($("#site-count").text()) - 1);
            }
        });
    </script>
</asp:Content>
