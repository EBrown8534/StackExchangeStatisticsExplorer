using Stack_Exchange_Statistics_Explorer.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Models
{
    [Serializable]
    public class ApiSite : Stack_Exchange_Statistics_Explorer.Models.Site, IBaseModel
    {
        public static new List<ApiSite> LoadAllFromDatabase(SqlConnection connection)
        {
            var sites = new List<ApiSite>();

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

        public static new ApiSite LoadFromReader(SqlDataReader reader, bool includeStyling = false)
        {
            var site = new ApiSite();

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

            if (includeStyling)
            {
                site.Styling = new Evbpc.Framework.Integrations.StackExchange.API.Models.Styling();
                site.Styling.LinkColor = reader.GetItem<string>(nameof(site.Styling.LinkColor));
                site.Styling.TagBackgroundColor = reader.GetItem<string>(nameof(site.Styling.TagBackgroundColor));
                site.Styling.TagForegroundColor = reader.GetItem<string>(nameof(site.Styling.TagForegroundColor));
            }

            return site;
        }
    }
}
