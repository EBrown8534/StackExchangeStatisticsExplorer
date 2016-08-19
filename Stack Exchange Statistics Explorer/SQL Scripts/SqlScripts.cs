using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.SQL_Scripts
{
    public class SqlScripts
    {
        public HttpServerUtility Server { get; set; }

        public SqlScripts(HttpServerUtility server)
        {
            Server = server;
        }

        public void RunScripts(SqlConnection connection, ScriptType type)
        {
            var files = GetScripts();

            foreach (var file in files.OrderBy(x => x))
            {
                if (file.Contains("Create") && (type & ScriptType.Create) > 0
                    || file.Contains("Insert") && (type & ScriptType.Insert) > 0
                    || file.Contains("Alter") && (type & ScriptType.Alter) > 0)
                {
                    using (var command = new SqlCommand(File.ReadAllText(file), connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public IEnumerable<string> GetScripts() => Directory.GetFiles(Server.MapPath("~/SQL Scripts"), "*.sql");
    }
}
