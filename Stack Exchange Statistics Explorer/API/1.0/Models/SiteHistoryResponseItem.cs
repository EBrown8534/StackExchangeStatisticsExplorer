using Stack_Exchange_Statistics_Explorer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Models
{
    [Serializable]
    public class SiteHistoryResponseItem : SiteStatsCalculated, IBaseModel
    {
        public static new List<SiteHistoryResponseItem> LoadFromDatabase(SqlConnection connection, Site site)
        {
            var sitesStatsCalculated = new List<SiteHistoryResponseItem>();

            using (var command = new SqlCommand("SELECT * FROM SE.SiteStats WHERE SiteId = @SiteId", connection))
            {
                command.Parameters.Add("@SiteId", SqlDbType.UniqueIdentifier).Value = site.Id;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var siteStats = new SiteHistoryResponseItem();
                        LoadFromReader(reader, siteStats);
                        siteStats.Site = site;
                        sitesStatsCalculated.Add(siteStats);
                    }
                }
            }

            return sitesStatsCalculated;
        }

        public static void LinkToNext(List<SiteHistoryResponseItem> stats)
        {
            for (int i = 0; i < stats.Count - 1; i++)
            {
                stats[i].Previous = stats[i + 1];
            }
        }
    }
}
