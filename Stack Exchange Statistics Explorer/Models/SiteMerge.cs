using Stack_Exchange_Statistics_Explorer.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    public class SiteMerge
    {
        public Guid OriginalSiteId { get; set; }
        public Guid NewSiteId { get; set; }
        public DateTime DateMerged { get; set; }
        public Site OriginalSite { get; set; }
        public Site NewSite { get; set; }

        public static List<SiteMerge> LoadAll(SqlConnection connection)
        {
            var results = new List<SiteMerge>();

            using (var command = new SqlCommand("SELECT * FROM SE.SiteMerges", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(LoadFromReader(reader));
                }
            }

            return results;
        }

        public static List<SiteMerge> LoadAllWithId(SqlConnection connection, Guid id)
        {
            var results = new List<SiteMerge>();

            using (var command = new SqlCommand("SELECT * FROM SE.SiteMerges WHERE OriginalSiteId = @Id OR NewSiteId = @Id", connection))
            {
                command.Parameters.Add("@Id", System.Data.SqlDbType.UniqueIdentifier).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(LoadFromReader(reader));
                    }
                }
            }

            return results;
        }

        public static SiteMerge LoadWithOriginalId(SqlConnection connection, Guid id)
        {
            using (var command = new SqlCommand("SELECT * FROM SE.SiteMerges WHERE OriginalSiteId = @Id", connection))
            {
                command.Parameters.Add("@Id", System.Data.SqlDbType.UniqueIdentifier).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return LoadFromReader(reader);
                        }
                    }

                    return null;
                }
            }
        }

        public static SiteMerge LoadWithBothIds(SqlConnection connection, Guid originalId, Guid newId)
        {
            using (var command = new SqlCommand("SELECT * FROM SE.SiteMerges WHERE OriginalSiteId = @OriginalId AND NewSiteId = @NewId", connection))
            {
                command.Parameters.Add("@OriginalId", System.Data.SqlDbType.UniqueIdentifier).Value = originalId;
                command.Parameters.Add("@NewId", System.Data.SqlDbType.UniqueIdentifier).Value = newId;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return LoadFromReader(reader);
                        }
                    }

                    return null;
                }
            }
        }

        public static List<SiteMerge> LoadWithNewId(SqlConnection connection, Guid id)
        {
            var results = new List<SiteMerge>();

            using (var command = new SqlCommand("SELECT * FROM SE.SiteMerges WHERE NewSiteId = @Id", connection))
            {
                command.Parameters.Add("@Id", System.Data.SqlDbType.UniqueIdentifier).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(LoadFromReader(reader));
                    }
                }
            }

            return results;
        }

        private static SiteMerge LoadFromReader(SqlDataReader reader) => new SiteMerge
        {
            DateMerged = reader.GetItem<DateTime>(nameof(DateMerged)),
            OriginalSiteId = reader.GetItem<Guid>(nameof(OriginalSiteId)),
            NewSiteId = reader.GetItem<Guid>(nameof(NewSiteId))
        };
    }
}