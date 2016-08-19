using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stack_Exchange_Statistics_Explorer.Utilities.Extensions.SqlDataReaderExtensions;
using static Stack_Exchange_Statistics_Explorer.Utilities.Extensions.DateTimeExtensions;
using Evbpc.Framework.Utilities.Extensions;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    public class Site : Evbpc.Framework.Integrations.StackExchange.API.Models.Site
    {
        public Guid Id { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime FirstUpdate { get; set; }
        
        public long LastUpdateEpoch => DateTimeExtensions.ToEpoch(LastUpdate.ConvertTimeToUtc());
        public long FirstUpdateEpoch => DateTimeExtensions.ToEpoch(FirstUpdate.ConvertTimeToUtc());
        public DateTime? LastEffectiveDateTime => LaunchDateTime ?? OpenBetaDateTime ?? ClosedBetaDateTime;
        public DateTime? FirstEffectiveDateTime => ClosedBetaDateTime ?? OpenBetaDateTime ?? LaunchDateTime;

        public static List<Site> LoadAllFromDatabase(SqlConnection connection)
        {
            var sites = new List<Site>();

            using (var command = new SqlCommand("SELECT * FROM SE.vwSitesWithRelationsShared", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    sites.Add(LoadFromReader(reader));
                }
            }

            return sites;
        }

        public static Site LoadFromDatabase(SqlConnection connection, Guid id)
        {
            var site = new Site();

            using (var command = new SqlCommand("SELECT S.*, SS.Id AS [StyleId], SS.LinkColor, SS.TagBackgroundColor, SS.TagForegroundColor FROM SE.vwSitesWithRelationsShared AS S INNER JOIN SE.SiteStyling AS SS ON SS.Id = S.StylingId WHERE S.Id = @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();
                    site = LoadFromReader(reader);
                }
            }

            return site;
        }

        public static Site LoadFromDatabase(SqlConnection connection, string apiSiteParameter)
        {
            var site = new Site();

            using (var command = new SqlCommand("SELECT S.*, SS.Id AS [StyleId], SS.LinkColor, SS.TagBackgroundColor, SS.TagForegroundColor FROM SE.vwSitesWithRelationsShared AS S INNER JOIN SE.SiteStyling AS SS ON SS.Id = S.StylingId WHERE ApiSiteParameter = @ApiSiteParameter", connection))
            {
                command.Parameters.Add("@ApiSiteParameter", SqlDbType.VarChar, 256).Value = apiSiteParameter;

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();
                    site = LoadFromReader(reader);
                }
            }

            return site;
        }

        public static List<SiteStatsCalculated> LoadAllWithStatsFromDatabase(SqlConnection connection)
        {
            var sitesStats = new List<SiteStatsCalculated>();

            using (var command = new SqlCommand("SELECT S.*, SS.Id AS [StyleId], SS.LinkColor, SS.TagBackgroundColor, SS.TagForegroundColor FROM SE.vwAllSitesWithLatestStat AS S INNER JOIN SE.SiteStyling AS SS ON SS.Id = S.StylingId ORDER BY S.Id ASC", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var site = LoadFromReader(reader);
                    var siteStats = new SiteStatsCalculated();
                    SiteStats.LoadFromReader(reader, siteStats);
                    siteStats.Site = site;
                    sitesStats.Add(siteStats);
                }
            }

            return sitesStats;
        }

        public static Site LoadFromReader(SqlDataReader reader)
        {
            var site = new Site();

            site.Aliases = reader.GetItem<string>(nameof(Aliases))?.Split(',').ToList();
            site.ApiSiteParameter = reader.GetItem<string>(nameof(ApiSiteParameter));
            site.Audience = reader.GetItem<string>(nameof(Audience));
            site.ClosedBetaDateTime = reader.GetItem<DateTime?>(nameof(ClosedBetaDate))?.ConvertTimeToUtc();
            site.FaviconUrl = reader.GetItem<string>(nameof(FaviconUrl));
            site.HighResolutionIconUrl = reader.GetItem<string>(nameof(HighResolutionIconUrl));
            site.IconUrl = reader.GetItem<string>(nameof(IconUrl));
            site.Id = reader.GetItem<Guid>(nameof(Id));
            site.LaunchDateTime = reader.GetItem<DateTime?>(nameof(LaunchDate))?.ConvertTimeToUtc();
            site.LogoUrl = reader.GetItem<string>(nameof(LogoUrl));
            site.MarkdownExtensions = reader.GetItem<string>(nameof(MarkdownExtensions))?.Split(',').ToList();
            site.Name = reader.GetItem<string>(nameof(Name));
            site.OpenBetaDateTime = reader.GetItem<DateTime?>(nameof(OpenBetaDate))?.ConvertTimeToUtc();
            site.SiteState = reader.GetItem<string>(nameof(SiteState));
            site.SiteType = reader.GetItem<string>(nameof(SiteType));
            site.SiteUrl = reader.GetItem<string>(nameof(SiteUrl));
            site.TwitterAccount = reader.GetItem<string>(nameof(TwitterAccount));
            site.LastUpdate = reader.GetItem<DateTime>(nameof(LastUpdate));
            site.FirstUpdate = reader.GetItem<DateTime>(nameof(FirstUpdate));

            site.Styling = new Evbpc.Framework.Integrations.StackExchange.API.Models.Styling();
            site.Styling.LinkColor = reader.GetItem<string>(nameof(site.Styling.LinkColor));
            site.Styling.TagBackgroundColor = reader.GetItem<string>(nameof(site.Styling.TagBackgroundColor));
            site.Styling.TagForegroundColor = reader.GetItem<string>(nameof(site.Styling.TagForegroundColor));

            return site;
        }

        public string HumanizeState
        {
            get
            {
                switch (SiteState)
                {
                    case "normal":
                        return "Graduated";
                    case "linked_meta":
                        return "Meta";
                    case "open_beta":
                        return "Open Beta";
                    case "closed_beta":
                        return "Closed Beta";
                    default:
                        return "N/A";
                }
            }
        }

        public string HumanizeType
        {
            get
            {
                switch (SiteType)
                {
                    case "main_site":
                        return "Main";
                    case "meta_site":
                        return "Meta";
                    default:
                        return "N/A";
                }
            }
        }

        public string DateTitle => HumanizeState.Replace("Graduated", "Graduation").Replace("Meta", "Creation");
    }
}
