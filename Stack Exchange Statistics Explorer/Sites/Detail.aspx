<%@ Page Title="Site Stats" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Stack_Exchange_Statistics_Explorer.Sites.Detail" %>

<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities" %>
<%@ Import Namespace="Stack_Exchange_Statistics_Explorer.Utilities.Extensions" %>
<%@ Import Namespace="Evbpc.Framework.Utilities.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="detail-header">
        <h2 class="medium-6 small-12 columns">
            <img src="<%:CurrentSite.HighResolutionIconUrl ?? CurrentSite.IconUrl %>" width="48" height="48" />
            <%:HttpUtility.HtmlDecode(CurrentSite.Name) %>
        </h2>
        <div class="medium-3 small-6 columns">
            <asp:HyperLink runat="server" ID="MetaSite" Visible="false" CssClass="button success radius right medium-12" Text="Meta Site &raquo;"></asp:HyperLink>
            <asp:HyperLink runat="server" ID="MainSite" Visible="false" CssClass="button success radius right medium-12" Text="Main Site &raquo;"></asp:HyperLink>
        </div>
        <div class="medium-3 small-6 columns">
            <a href="<%:CurrentSite.SiteUrl%>" class="button radius right medium-12" target="_blank">View on SE &raquo;</a>
        </div>
    </div>
    <div class="medium-12 columns">
        <% if (Merge != null)
            { %>
        <div class="alert-box warning radius">
            You were sent here as a result of attempting to load a site which has been merged to a new site. On <%:Merge.DateMerged.ToString("dddd, MMMM d, yyyy") %> the site you tried to view (<%:Merge.OriginalSite.ApiSiteParameter%>/<%:HttpUtility.HtmlDecode(Merge.OriginalSite.Name)%>) was merged to this site (<%:CurrentSite.ApiSiteParameter%>/<%:HttpUtility.HtmlDecode(CurrentSite.Name)%>) by Stack Exchange. All data within our system has already been merged, and any API requests to the original site (<%:Merge.OriginalSite.ApiSiteParameter%>) will no longer return any results.
        </div>
        <% } %>
        <div class="medium-7 small-12 columns">
            <h3>General</h3>
            <table class="detail-table medium-12">
                <tbody>
                    <tr>
                        <th>Site ID</th>
                        <td><%:CurrentSite.Id.ToString("d") %></td>
                    </tr>
                    <tr>
                        <th>API Parameter</th>
                        <td><%:CurrentSite.ApiSiteParameter %></td>
                    </tr>
                    <tr>
                        <th>Audience</th>
                        <td><%:CurrentSite.Audience %></td>
                    </tr>
                    <tr>
                        <th>Date of Closed Beta</th>
                        <td><%:CurrentSite.ClosedBetaDateTime?.ToLongDateString() ?? "N/A" %></td>
                    </tr>
                    <tr>
                        <th>Date of Open Beta</th>
                        <td><%:CurrentSite.OpenBetaDateTime?.ToLongDateString() ?? "N/A" %></td>
                    </tr>
                    <tr>
                        <th>Date of Launch</th>
                        <td><%:CurrentSite.LaunchDateTime?.ToLongDateString() ?? "N/A" %></td>
                    </tr>
                    <tr>
                        <th>State</th>
                        <td><%:CurrentSite.HumanizeState %></td>
                    </tr>
                    <tr>
                        <th>Type</th>
                        <td><%:CurrentSite.HumanizeType %></td>
                    </tr>
                    <tr>
                        <th>URL</th>
                        <td><%:CurrentSite.SiteUrl %></td>
                    </tr>
                    <tr>
                        <th>Twitter Account</th>
                        <td><a class='<%:CurrentSite.TwitterAccount?.Length > 0 ? "" : "hidden" %>' href="https://twitter.com/<%:CurrentSite.TwitterAccount %>"><%:"@" + CurrentSite.TwitterAccount %></a></td>
                    </tr>
                    <tr>
                        <th>Logo</th>
                        <td>
                            <img src="<%:CurrentSite.LogoUrl %>" height="48" class="logo" /></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="medium-5 small-12 columns">
            <h3>Health</h3>
            <table class="detail-table">
                <tbody>
                    <tr>
                        <th><span data-tooltip class="has-tip" title="10 questions per day on average is a healthy beta, 5 questions or fewer per day needs some work. A healthy site generates lots of good content to make sure users keep coming back. This uses a moving average over the last 30 days of questions asked.">Questions Per Day</span></th>
                        <td class='<%:QuestionsPerDay.GetClassOption(x => x >= 10, x => x < 5) %>'>
                            <%:QuestionsPerDay.ToString("0.00") %>
                        </td>
                        <td class='show-for-large-up <%:LatestStats.QuestionsPerDay >= 10 ? "hidden" : "" %>'>Needs work
                        </td>
                    </tr>
                    <tr>
                        <th>Answers Per Day</th>
                        <td class='<%:AnswersPerDay.GetClassOption(x => x >= 20, x => x < 10) %>'>
                            <%:AnswersPerDay.ToString("0.00") %>
                        </td>
                        <td class='show-for-large-up <%:LatestStats.AnswersPerDay >= 20 ? "hidden" : "" %>'>Needs work
                        </td>
                    </tr>
                    <tr>
                        <th><span data-tooltip class="has-tip" title="90% answered is a healthy beta, 80% answered needs some work. In the beta it's especially important that when new visitors ask questions they usually get a good answer.">Answer Rate</span></th>
                        <td class='<%:LatestStats.AnsweredRate.GetClassOption(x => x >= .9, x => x < .8) %>'>
                            <%:LatestStats.AnsweredRate.ToString("0.00%") %>
                        </td>
                        <td class='show-for-large-up <%:LatestStats.AnsweredRate >= .9 ? "hidden" : "" %>'>Needs work
                        </td>
                    </tr>
                    <tr>
                        <th><span data-tooltip class="has-tip" title="Every site needs a solid group of core users to assist in moderating the site. We recommend 150 users with 200+ rep.">Avid Users</span></th>
                        <td class='<%:LatestStats.UsersAbove200Rep.GetClassOption(x => x >= 150, x => x < 125) %>'>
                            <%:LatestStats.UsersAbove200Rep?.ToString("n0") ?? "N/A" %>
                        </td>
                        <td class='show-for-large-up <%:LatestStats.UsersAbove200Rep >= 150 ? "hidden" : "" %>'>Needs work
                        </td>
                    </tr>
                    <tr>
                        <th><span data-tooltip class="has-tip" title="2.5 answers per question is good, only 1 answer per question needs somework. On a healthy site, questions recieve multiple answers and the best answer is voted to the top.">Answer Ratio</span></th>
                        <td class='<%:LatestStats.AnswerRatio.GetClassOption(x => x >= 2.5, x => x < 1) %>'>
                            <%:LatestStats.AnswerRatio.ToString("0.00") %>
                        </td>
                        <td class='show-for-large-up <%:LatestStats.AnswerRatio >= 2.5 ? "hidden" : "" %>'>Needs work
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="small-12 medium-5 columns">
            <h3><span data-tooltip class="has-tip" title="Rankings are based on other sites of the same type. Main sites are only ranked among Main sites, and Meta sites only ranked among Meta sites.">Rankings</span></h3>
            <table class="detail-table">
                <tbody>
                    <tr>
                        <th>Questions</th>
                        <td>
                            <%:GetRank(x => x.TotalQuestions) %> / <%:AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() %>
                            (<%:GetPercentile(x => x.TotalQuestions).ToString().AddIth()%> percentile)
                        </td>
                    </tr>
                    <tr>
                        <th>Answers</th>
                        <td>
                            <%:GetRank(x => x.TotalAnswers) %> / <%:AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() %> 
                            (<%:GetPercentile(x => x.TotalAnswers).ToString().AddIth()%> percentile)
                        </td>
                    </tr>
                    <tr>
                        <th>Answered Rate</th>
                        <td>
                            <%:GetRank(x => x.AnsweredRate) %> / <%:AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() %>
                            (<%:GetPercentile(x => x.AnsweredRate).ToString().AddIth()%> percentile)
                        </td>
                    </tr>
                    <tr>
                        <th>Answer Ratio</th>
                        <td>
                            <%:GetRank(x => x.AnswerRatio) %> / <%:AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() %>
                            (<%:GetPercentile(x => x.AnswerRatio).ToString().AddIth()%> percentile)
                        </td>
                    </tr>
                    <tr>
                        <th>Percent Users > 150 Rep</th>
                        <td>
                            <%:GetRank(x => (double)(x.UsersAbove150Rep ?? 0) / x.TotalUsers) %> / <%:AllLatestStats.Where(x => x.Site.SiteType == CurrentSite.SiteType).Count() %>
                            (<%:GetPercentile(x => (double)(x.UsersAbove150Rep ?? 0) / x.TotalUsers).ToString().AddIth()%> percentile)
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="medium-12 columns graphs">
            <h3>Graphs</h3>
            <ul class="tabs" data-tab>
                <li class="tab-title active"><a href="#graphSet1">General</a></li>
                <li class="tab-title"><a href="#graphSet2">User</a></li>
                <li class="tab-title"><a href="#graphSet5">Voting</a></li>
                <li class="tab-title"><a href="#graphSet3">Question</a></li>
                <li class="tab-title"><a href="#graphSet4">Answer</a></li>
            </ul>
            <div class="tabs-content">
                <div class="content active" id="graphSet1">
                    <svg class="zombieChart"></svg>
                    <svg class="questionAcceptRateChart"></svg>
                    <svg class="answerAcceptRateChart"></svg>
                </div>
                <div class="content" id="graphSet2">
                    <svg class="usersChart"></svg>
                    <svg class="usersOver150RepChart"></svg>
                    <svg class="percentUsersOver150RepChart"></svg>
                    <svg class="usersOver200RepChart"></svg>
                    <svg class="percentUsersOver200RepChart"></svg>
                    <svg class="usersOver1000RepChart"></svg>
                    <svg class="percentUsersOver1000RepChart"></svg>
                    <svg class="usersOver10000RepChart"></svg>
                    <svg class="percentUsersOver10000RepChart"></svg>
                    <svg class="usersOver20000RepChart"></svg>
                    <svg class="percentUsersOver20000RepChart"></svg>
                </div>
                <div class="content" id="graphSet5">
                    <svg class="totalVotes"></svg>
                    <svg class="votesPerDay"></svg>
                    <svg class="votesPerUserAbove150Rep"></svg>
                </div>
                <div class="content" id="graphSet3">
                    <svg class="questionsPerDayChart"></svg>
                    <svg class="questionsChart"></svg>
                    <svg class="questionsChangeChart"></svg>
                </div>
                <div class="content" id="graphSet4">
                    <svg class="answersPerDayChart"></svg>
                    <svg class="answersChart"></svg>
                    <svg class="answersChangeChart"></svg>
                </div>
            </div>
            <script type="text/javascript">
                var fW = 960;
                var fH = 450;
                var hW = fW / 2;
                var hH = fH / 2;
                var m = { top: 5, right: 3, bottom: 60, left: 70 }
                var siteId = "<%:LatestStats.Site.Id%>";
                var lTC = 60;
                var sTC = 15;

                buildBasicChart(m, fW, fH, siteId, ".zombieChart", "AnsweredRate", "0.000%", "Answered Rate (%)", lTC);
                buildBasicChart(m, hW, hH, siteId, ".questionAcceptRateChart", "QuestionAnswerAcceptRate", "0.000%", "Answered Accept Rate (%)", sTC);
                buildBasicChart(m, hW, hH, siteId, ".answerAcceptRateChart", "AnswerAcceptRate", "0.000%", "Answer Accept Rate (%)", sTC);

                buildBasicChart(m, fW, fH, siteId, ".usersChart", "TotalUsers", "0", "Total Users", lTC);
                buildBasicChart(m, hW, hH, siteId, ".usersOver150RepChart", "UsersAbove150Rep", "0", "Users > 150 Rep", sTC);
                buildBasicChart(m, hW, hH, siteId, ".percentUsersOver150RepChart", "PercentUsersAbove150Rep", "0.000%", "Users > 150 Rep (%)", sTC);
                buildBasicChart(m, hW, hH, siteId, ".usersOver200RepChart", "UsersAbove200Rep", "0", "Users > 200 Rep", sTC);
                buildBasicChart(m, hW, hH, siteId, ".percentUsersOver200RepChart", "PercentUsersAbove200Rep", "0.000%", "Users > 200 Rep (%)", sTC);
                buildBasicChart(m, hW, hH, siteId, ".usersOver1000RepChart", "UsersAbove1000Rep", "0", "Users > 1k Rep", sTC);
                buildBasicChart(m, hW, hH, siteId, ".percentUsersOver1000RepChart", "PercentUsersAbove1000Rep", "0.000%", "Users > 1k Rep (%)", sTC);
                buildBasicChart(m, hW, hH, siteId, ".usersOver10000RepChart", "UsersAbove10000Rep", "0", "Users > 10k Rep", sTC);
                buildBasicChart(m, hW, hH, siteId, ".percentUsersOver10000RepChart", "PercentUsersAbove10000Rep", "0.000%", "Users > 10k Rep (%)", sTC);
                buildBasicChart(m, hW, hH, siteId, ".usersOver20000RepChart", "UsersAbove20000Rep", "0", "Users > 20k Rep", sTC);
                buildBasicChart(m, hW, hH, siteId, ".percentUsersOver20000RepChart", "PercentUsersAbove20000Rep", "0.000%", "Users > 20k Rep (%)", sTC);

                buildBasicChart(m, fW, fH, siteId, ".totalVotes", "TotalVotes", "0", "Total Votes", lTC);
                buildBasicChart(m, hW, hH, siteId, ".votesPerDay", "VotesPerDay", "0.000", "Votes Per Day", sTC);
                buildBasicChart(m, hW, hH, siteId, ".votesPerUserAbove150Rep", "VotesPerUserAbove150Rep", "0.000", "Votes Per User > 150 Rep", sTC);

                buildBasicChart(m, fW, fH, siteId, ".questionsPerDayChart", "QuestionsPerDay", "0.000", "Questions Per Day", lTC);
                buildBasicChart(m, hW, hH, siteId, ".questionsChart", "TotalQuestions", "0", "Total Questions", sTC);
                buildBasicChart(m, hW, hH, siteId, ".questionsChangeChart", "TotalQuestionsChange", "0", "Questions Change", sTC);

                buildBasicChart(m, fW, fH, siteId, ".answersPerDayChart", "AnswersPerDay", "0.000", "Answers Per Day", lTC);
                buildBasicChart(m, hW, hH, siteId, ".answersChart", "TotalAnswers", "0", "Total Answers", sTC);
                buildBasicChart(m, hW, hH, siteId, ".answersChangeChart", "TotalAnswersChange", "0", "Answers Change", sTC);
            </script>
            <h3>Raw Stats Data (Newest to oldest)</h3>
            <script type="text/javascript">
                document.getElementById("toggleDeltas").addEventListener('change', function (event) {
                    if (document.getElementById("toggleDeltas").checked) {
                        $(".delta").removeClass("hidden");
                    } else {
                        $(".delta").addClass("hidden");
                    }
                });
            </script>
            <div class="row collapse">
                <div class="medium-1 small-2 columns">
                    <label for="StartDate" class="prefix radius">Start date</label>
                </div>
                <div class="medium-2 small-2 columns">
                    <asp:TextBox runat="server" ID="StartDate" CssClass="postfix prefix"></asp:TextBox>
                </div>
                <div class="medium-1 small-1 columns">
                    <label for="EndDate" class="postfix prefix">End</label>
                </div>
                <div class="medium-2 small-2 columns">
                    <asp:TextBox runat="server" ID="EndDate" CssClass="postfix prefix"></asp:TextBox>
                </div>
                <div class="medium-1 small-2 columns">
                    <label for="Interval" class="postfix prefix">Interval</label>
                </div>
                <div class="medium-2 small-2 columns">
                    <asp:DropDownList runat="server" ID="Interval" CssClass="postfix prefix">
                        <asp:ListItem Value="1d">Daily</asp:ListItem>
                        <asp:ListItem Value="1w">Weekly</asp:ListItem>
                        <asp:ListItem Value="1m">Monthly</asp:ListItem>
                        <asp:ListItem Value="3m">Quartery</asp:ListItem>
                        <asp:ListItem Value="1y">Yearly</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="medium-1 small-1 columns">
                    <asp:Button runat="server" ID="FilterTable" Text="Filter" CssClass="button postfix radius" OnClick="FilterTable_Click" />
                </div>
                <span class="right">
                    <span class="show-for-medium-up">
                        <span class="switch radius has-tip" data-tooltip aria-haspopup="true" title="Toggle deltas">
                            <input id="toggleDeltas" type="checkbox" checked />
                            <label for="toggleDeltas"></label>
                        </span>
                    </span>
                </span>
            </div>
            <asp:Literal runat="server" ID="ErrorPanel" Visible="false">
                <p class="alert-box alert radius">
                    There was an error processing one or more of the dates you provided.
                </p>
            </asp:Literal>
            <table>
                <thead>
                    <tr>
                        <th>Gathered</th>
                        <th class="show-for-medium-up">Accepted</th>
                        <th>Answered</th>
                        <th>Answers</th>
                        <th>Questions</th>
                        <th class="show-for-large-up">Unanswered</th>
                        <th class="show-for-medium-up">Users</th>
                        <th class="show-for-medium-up">Votes</th>
                        <th class="show-for-medium-up">Users > 150 Rep</th>
                        <th>Users > 200 Rep</th>
                        <th>Answered Rate</th>
                        <th class="show-for-large-up">Unanswered Rate</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:ListView runat="server" ID="SiteStats">
                        <LayoutTemplate>
                            <tr runat="server" id="groupPlaceholder"></tr>
                        </LayoutTemplate>

                        <GroupTemplate>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </GroupTemplate>

                        <ItemTemplate>
                            <tr runat="server">
                                <td>
                                    <span class="show-for-large-up">
                                        <%#Binder.Eval<DateTime>(Container, "Gathered").ToString("ddd d MMM yy") %>
                                    </span>
                                    <span class="hide-for-large-up">
                                        <%#Binder.Eval<DateTime>(Container, "Gathered").ToString("d-M-yy") %>
                                    </span>
                                </td>
                                <td class="show-for-medium-up">
                                    <%#Binder.Eval<int>(Container, "TotalAccepted") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "TotalAcceptedChange").GetClassOption(x => x > 0, x => x < 0) %>'>
                                        <%#Binder.Eval<int?>(Container, "TotalAcceptedChange")?.ToString().IncludeSign() %>
                                    </span>
                                </td>
                                <td class="show-for-large-up">
                                    <%#Binder.Eval<int>(Container, "TotalAnswered") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "TotalAnsweredChange").GetClassOption(x => x > 0, x => x < 0) %>'>
                                        <%#Binder.Eval<int?>(Container, "TotalAnsweredChange")?.ToString().IncludeSign() %>
                                    </span>
                                </td>
                                <td>
                                    <%#Binder.Eval<int>(Container, "TotalAnswers") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "TotalAnswersChange").GetClassOption(x => x > 0, x => x < 0) %>'>
                                        <%#Binder.Eval<int?>(Container, "TotalAnswersChange")?.ToString().IncludeSign() %>
                                    </span>
                                </td>
                                <td>
                                    <%#Binder.Eval<int>(Container, "TotalQuestions") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "TotalQuestionsChange").GetClassOption(x => x > 0, x => x < 0) %>'>
                                        <%#Binder.Eval<int?>(Container, "TotalQuestionsChange")?.ToString().IncludeSign() %>
                                    </span>
                                </td>
                                <td>
                                    <%#Binder.Eval<int>(Container, "TotalUnanswered") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "TotalUnansweredChange").GetClassOption(x => x < 0, x => x > 0) %>'>
                                        <%#Binder.Eval<int?>(Container, "TotalUnansweredChange")?.ToString().IncludeSign() %>
                                    </span>
                                </td>
                                <td class="show-for-medium-up">
                                    <%#Binder.Eval<int>(Container, "TotalUsers") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "TotalUsersChange").GetClassOption(x => x > 0, x => x < 0) %>'>
                                        <%#Binder.Eval<int?>(Container, "TotalUsersChange")?.ToString().IncludeSign() %>
                                    </span>
                                </td>
                                <td class="show-for-medium-up">
                                    <%#Binder.Eval<int>(Container, "TotalVotes") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "TotalVotesChange").GetClassOption(x => x > 0, x => x < 0) %>'>
                                        <%#Binder.Eval<int?>(Container, "TotalVotesChange")?.ToString().IncludeSign() %>
                                    </span>
                                </td>
                                <td class="show-for-medium-up">
                                    <span class='<%#Binder.Eval<int?>(Container, "UsersAbove150Rep") == null ? "grey" : "" %>'>
                                        <%#Binder.Eval<int?>(Container, "UsersAbove150Rep")?.ToString() ?? "N/A" %><br />
                                        <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "UsersAbove150RepChange").GetClassOption(x => x > 0, x => x < 0) %>'>
                                            <%#Binder.Eval<int?>(Container, "UsersAbove150RepChange")?.ToString().IncludeSign() %>
                                        </span>
                                    </span>
                                </td>
                                <td>
                                    <span class='<%#Binder.Eval<int?>(Container, "UsersAbove200Rep") == null ? "grey" : "" %>'>
                                        <%#Binder.Eval<int?>(Container, "UsersAbove200Rep")?.ToString() ?? "N/A" %><br />
                                        <span class='show-for-medium-up delta <%#Binder.Eval<int?>(Container, "UsersAbove200RepChange").GetClassOption(x => x > 0, x => x < 0) %>'>
                                            <%#Binder.Eval<int?>(Container, "UsersAbove200RepChange")?.ToString().IncludeSign() %>
                                        </span>
                                    </span>
                                </td>
                                <td class="show-for-large-up">
                                    <%#Binder.Eval<double>(Container, "AnsweredRate").ToString("0.00%") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<double?>(Container, "AnsweredRateChange").GetClassOption(x => x > 0.00005, x => x < -0.00005) %>'>
                                        <%#Binder.Eval<double?>(Container, "AnsweredRateChange")?.ToString("0.00%").IncludeSign() %>
                                    </span>
                                </td>
                                <td>
                                    <%#Binder.Eval<double>(Container, "UnansweredRate").ToString("0.00%") %><br />
                                    <span class='show-for-medium-up delta <%#Binder.Eval<double?>(Container, "UnansweredRateChange").GetClassOption(x => x < -0.00005, x => x > 0.00005) %>'>
                                        <%#Binder.Eval<double?>(Container, "UnansweredRateChange")?.ToString("0.00%").IncludeSign() %>
                                    </span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
