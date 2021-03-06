﻿using Evbpc.Framework.Utilities;
using Stack_Exchange_Statistics_Explorer.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stack_Exchange_Statistics_Explorer
{
    public partial class SystemHealth : System.Web.UI.Page
    {
        protected TableInfo CoreTableSizeTotals { get; private set; }
        protected TableInfo WebsiteTableSizeTotals { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var logResults = new List<ApiBatchLog>();
            var coreTableSizes = new List<TableInfo>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiDataConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                logResults = ApiBatchLog.LoadAllFromDatabase(connection);
                coreTableSizes = TableInfo.LoadAllFromDatabase(connection).Where(x => x.SchemaName == "SE").ToList();
            }

            var websiteTableSizes = new List<TableInfo>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                websiteTableSizes = TableInfo.LoadAllFromDatabase(connection).Where(x => x.SchemaName == "SE").ToList();
            }

            var apiLogResults = new List<ApiBatchLog>();

            foreach (var logResult in logResults.OrderBy(x => x.StartDateTime))
            {
                if (apiLogResults.Where(x => x.StartDateTime.Date == logResult.StartDateTime.Date).Count() == 0)
                {
                    apiLogResults.Add(logResult);
                }
            }

            apiLogResults = apiLogResults.OrderByDescending(x => x.StartDateTime).ToList();

            CoreTableSizeTotals = new TableInfo();
            CoreTableSizeTotals.TableName = "Total";

            foreach (var tableSize in coreTableSizes)
            {
                CoreTableSizeTotals.RowCount += tableSize.RowCount;
                CoreTableSizeTotals.IndexCount += tableSize.IndexCount;
                CoreTableSizeTotals.TotalSpace += tableSize.TotalSpace;
                CoreTableSizeTotals.UsedSpace += tableSize.UsedSpace;
            }

            WebsiteTableSizeTotals = new TableInfo();
            WebsiteTableSizeTotals.TableName = "Total";

            foreach (var tableSize in websiteTableSizes)
            {
                WebsiteTableSizeTotals.RowCount += tableSize.RowCount;
                WebsiteTableSizeTotals.IndexCount += tableSize.IndexCount;
                WebsiteTableSizeTotals.TotalSpace += tableSize.TotalSpace;
                WebsiteTableSizeTotals.UsedSpace += tableSize.UsedSpace;
            }

            ApiLogResults.DataSource = apiLogResults;
            ApiLogResults.DataBind();

            CoreTableSizeResults.DataSource = coreTableSizes;
            CoreTableSizeResults.DataBind();

            WebsiteTableSizeResults.DataSource = websiteTableSizes;
            WebsiteTableSizeResults.DataBind();
        }

        protected class TableInfo
        {
            public string TableName { get; set; }
            public string SchemaName { get; set; }
            public long RowCount { get; set; }
            public DataSize TotalSpace { get; set; }
            public DataSize UsedSpace { get; set; }
            public int IndexCount { get; set; }
            public DateTime Created { get; set; }
            public DateTime Modified { get; set; }

            public string FullTableName => (SchemaName != null ? SchemaName + '.' : "") + TableName;
            public DataSize UnusedSpace => TotalSpace - UsedSpace;
            public DataSize UsedSpacePerRow => RowCount == 0 ? new DataSize(0) : UsedSpace / (double)RowCount;
            public double RowsPerDay => (double)RowCount / (DateTime.UtcNow - Created).Days;

            public static List<TableInfo> LoadAllFromDatabase(SqlConnection connection)
            {
                var results = new List<TableInfo>();

                using (var command = new SqlCommand(@"SELECT 
    CAST(t.name AS VARCHAR(64)) AS TableName,
    CAST(s.name AS VARCHAR(64)) AS SchemaName,
    p.[rows] AS [RowCount],
    SUM(a.total_pages) * 8 AS TotalSpaceKB, 
    SUM(a.used_pages) * 8 AS UsedSpaceKB,
    COUNT(DISTINCT i.index_id) AS IndexCount,
	t.create_date AS Created,
	t.modify_date AS Modified
FROM 
    sys.tables t
INNER JOIN      
    sys.indexes i ON t.[object_id] = i.[object_id]
INNER JOIN 
    sys.partitions p ON i.[object_id] = p.[object_id] AND i.index_id = p.index_id
INNER JOIN 
    sys.allocation_units a ON p.[partition_id] = a.container_id
LEFT OUTER JOIN 
    sys.schemas s ON t.[schema_id] = s.[schema_id]
WHERE 
    s.name = 'SE'
GROUP BY 
    t.name, s.name, p.[rows], t.create_date, t.modify_date
ORDER BY 
    s.name, t.name", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(LoadFromReader(reader));
                    }
                }

                return results;
            }

            public static TableInfo LoadFromReader(SqlDataReader reader) => new TableInfo
            {
                TableName = reader.GetItem<string>(nameof(TableName)),
                SchemaName = reader.GetItem<string>(nameof(SchemaName)),
                RowCount = reader.GetItem<long>(nameof(RowCount)),
                TotalSpace = DataSize.From(reader.GetItem<long>(nameof(TotalSpace) + "KB"), SizeUnit.Kilobytes),
                UsedSpace = DataSize.From(reader.GetItem<long>(nameof(UsedSpace) + "KB"), SizeUnit.Kilobytes),
                IndexCount = reader.GetItem<int>(nameof(IndexCount)),
                Created = reader.GetItem<DateTime>(nameof(Created)),
                Modified = reader.GetItem<DateTime>(nameof(Modified))
            };
        }

        protected class ApiBatchLog
        {
            public Guid Id { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public DateTime AddedDateTime { get; set; }
            public string RequestedBy { get; set; }
            public int RequestCount { get; set; }
            public int BackoffCount { get; set; }
            public int TotalBackoff { get; set; }
            public int HasMoreCount { get; set; }
            public int QuotaMax { get; set; }
            public int StartQuotaRemaining { get; set; }
            public int EndQuotaRemaining { get; set; }
            public int SiteCount { get; set; }

            public double RequestsPerSecond => RequestCount / TimeTaken.TotalSeconds;
            public TimeSpan TimeTaken => EndDateTime - StartDateTime;
            public double MillisecondsPerSite => TimeTaken.TotalMilliseconds / SiteCount;
            public double MillisecondsPerRequest => TimeTaken.TotalMilliseconds / RequestCount;

            public static ApiBatchLog LoadFromReader(SqlDataReader reader) => new ApiBatchLog
            {
                Id = reader.GetItem<Guid>(nameof(Id)),
                StartDateTime = reader.GetItem<DateTime>(nameof(StartDateTime)),
                EndDateTime = reader.GetItem<DateTime>(nameof(EndDateTime)),
                AddedDateTime = reader.GetItem<DateTime>(nameof(AddedDateTime)),
                RequestedBy = reader.GetItem<string>(nameof(RequestedBy)),
                RequestCount = reader.GetItem<int>(nameof(RequestCount)),
                BackoffCount = reader.GetItem<int>(nameof(BackoffCount)),
                TotalBackoff = reader.GetItem<int>(nameof(TotalBackoff)),
                HasMoreCount = reader.GetItem<int>(nameof(HasMoreCount)),
                QuotaMax = reader.GetItem<int>(nameof(QuotaMax)),
                StartQuotaRemaining = reader.GetItem<int>(nameof(StartQuotaRemaining)),
                EndQuotaRemaining = reader.GetItem<int>(nameof(EndQuotaRemaining)),
                SiteCount = reader.GetItem<int>(nameof(SiteCount))
            };

            public static List<ApiBatchLog> LoadAllFromDatabase(SqlConnection connection)
            {
                var results = new List<ApiBatchLog>();

                using (var command = new SqlCommand(@"SELECT
    b.*
    ,[ItemCount] AS[SiteCount]
FROM
    SE.ApiBatchLog b
INNER JOIN
    SE.ApiLog
ON
    Id = BatchId
    AND
    StartDateTime = RequestDateTime
WHERE
    b.RequestedBy <> 'BADBATCH'
    AND
    b.RequestedBy <> 'INCOMPLETEBATCH'
ORDER BY
    EndDateTime DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(LoadFromReader(reader));
                    }
                }

                return results;
            }
        }
    }
}