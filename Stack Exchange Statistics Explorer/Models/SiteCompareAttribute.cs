using Evbpc.Framework.Utilities.Serialization.DelimitedSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SiteCompareAttribute : DelimitedColumnAttribute
    {
        public SiteCompareAttribute(string name)
        {
            Name = name;
        }

        public SiteCompareAttribute(string name, string format)
            : this(name)
        {
            Format = format;
        }

        public SiteCompareAttribute(string name, string format, int order)
            : this(name, format)
        {
            Order = order;
        }

        public SiteCompareAttribute(string name, string format, int order, bool traverse)
            : this(name, format, order)
        {
            Traverse = traverse;
        }

        public SiteCompareAttribute(string name, string format, int order, bool traverse, string summary)
            : this(name, format, order, traverse)
        {
            Summary = summary;
        }
        
        public string Summary { get; set; }
    }
}