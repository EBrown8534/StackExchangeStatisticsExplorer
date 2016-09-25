using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SiteCompareAttribute : Attribute
    {
        public SiteCompareAttribute(string display)
        {
            Display = display;
        }

        public SiteCompareAttribute(string display, string format)
            : this(display)
        {
            Format = format;
        }

        public SiteCompareAttribute(string display, string format, int order)
            : this(display, format)
        {
            Order = order;
        }

        public SiteCompareAttribute(string display, string format, int order, bool traverse)
            : this(display, format, order)
        {
            Traverse = traverse;
        }

        public SiteCompareAttribute(string display, string format, int order, bool traverse, string summary)
            : this(display, format, order, traverse)
        {
            Summary = summary;
        }

        public string Display { get; set; }
        public string Format { get; set; }
        public string Summary { get; set; }
        public bool Traverse { get; set; }
        public int Order { get; set; }
    }
}