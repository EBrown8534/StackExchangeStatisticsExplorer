﻿using Stack_Exchange_Statistics_Explorer.API._1._0.Models;
using System.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Stack_Exchange_Statistics_Explorer.Utilities.Extensions;
using Stack_Exchange_Statistics_Explorer.Models;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Requests
{
    public class LastUpdateRequest : Request<LastUpdateItem>
    {
        public string Sites { get; }

        public LastUpdateRequest(HttpContext context)
            : base(context)
        {
            Sites = context.Request.QueryString[nameof(Sites)];
        }

        protected override IEnumerable<LastUpdateItem> DoWork()
        {
            var results = new List<LastUpdateItem>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var sites = new List<Guid>();

                if (!string.IsNullOrWhiteSpace(Sites))
                {
                    var siteArray = Sites.Split(',');

                    foreach (var site in siteArray)
                    {
                        var result = new Guid();

                        if (!Guid.TryParse(site, out result))
                        {
                            throw new ArgumentException($"At lease one of the values provided for '{nameof(Sites)}' was not a valid Guid.");
                        }

                        var siteMerge = SiteMerge.LoadWithOriginalId(connection, result);
                        if (siteMerge != null)
                        {
                            result = siteMerge.NewSiteId;
                        }

                        sites.Add(result);
                    }
                }

                using (var command = new SqlCommand("SELECT * FROM SE.vwAllSitesWithLatestStat", connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (sites.Count == 0 || sites.Contains(reader.GetItem<Guid>(nameof(Site.Id))))
                            {
                                results.Add(new LastUpdateItem
                                {
                                    Id = reader.GetItem<Guid>(nameof(Site.Id)).ToString(),
                                    Updated = reader.GetItem<DateTime>(nameof(SiteStats.Gathered)).ToString("yyyy-MM-dd HH:mm:ss.fffffff"),
                                });
                            }
                        }
                    }
                }
            }

            return results;
        }
    }
}