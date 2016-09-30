using Evbpc.Framework.Utilities.Serialization.DelimitedSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Models
{
    [Serializable]
    public class LastUpdateItem : IBaseModel
    {
        [DelimitedColumn(Name = "Id", Order = 0)]
        public string Id { get; set; }
        [DelimitedColumn(Name = "Updated", Order = 1)]
        public string Updated { get; set; }
    }
}