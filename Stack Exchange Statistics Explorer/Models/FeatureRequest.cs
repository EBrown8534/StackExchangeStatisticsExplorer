using Stack_Exchange_Statistics_Explorer.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    public class FeatureRequest
    {
        public Guid Id { get; set; }
        public DateTime ProposedOn { get; set; }
        public bool Display { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public string ProposedBy { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }

        public string PriorityString
        {
            get
            {
                switch (Priority)
                {
                    case 0:
                        return "Lowest";
                    case 1:
                        return "Low";
                    case 2:
                        return "Medium";
                    case 3:
                        return "High";
                    case 4:
                        return "Highest";
                    default:
                        return "None";
                }
            }
        }

        public static List<FeatureRequest> LoadAllFromDatabase(SqlConnection connection)
        {
            var results = new List<FeatureRequest>();

            using (var command = new SqlCommand("SELECT * FROM SE.FeatureRequests", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(LoadFromReader(reader));
                }
            }

            return results;
        }

        public static FeatureRequest LoadFromReader(SqlDataReader reader)
        {
            var result = new FeatureRequest();

            result.Id = reader.GetItem<Guid>(nameof(Id));
            result.ProposedOn = reader.GetItem<DateTime>(nameof(ProposedOn));
            result.Display = reader.GetItem<bool>(nameof(Display));
            result.Priority = reader.GetItem<int>(nameof(Priority));
            result.Status = reader.GetItem<string>(nameof(Status));
            result.ProposedBy = reader.GetItem<string>(nameof(ProposedBy));
            result.Area = reader.GetItem<string>(nameof(Area));
            result.Description = reader.GetItem<string>(nameof(Description));

            return result;
        }
    }
}
