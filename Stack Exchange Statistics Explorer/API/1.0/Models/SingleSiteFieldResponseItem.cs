using Evbpc.Framework.Utilities.Serialization.DelimitedSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Models
{
    [Serializable]
    public class SingleSiteFieldResponseItem : IBaseModel
    {
        [DelimitedColumn(Name = "Gathered", Order = 0)]
        public string Gathered { get; set; }
        [DelimitedColumn(Name = "FieldValue", Order = 1)]
        public string FieldValue { get; set; }
    }
}
