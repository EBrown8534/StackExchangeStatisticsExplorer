using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.SQL_Scripts
{
    [Flags]
    public enum ScriptType
    {
        None = 0x00,
        Create = 0x01,
        Insert = 0x02,
        Alter = 0x04,
        All = 0xFF,
    }
}
