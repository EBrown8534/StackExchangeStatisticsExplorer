using Evbpc.Framework.Integrations.StackExchange.API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.Utilities.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static T GetItem<T>(this SqlDataReader reader, string item) => reader[item] == DBNull.Value ? default(T) : (T)reader[item];
    }
}
