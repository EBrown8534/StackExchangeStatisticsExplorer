using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ApiDescriptionAttribute : Attribute
    {
        public string Description { get; set; }
        public bool Required { get; set; } = false;
        public bool Nullable { get; set; } = false;

        public ApiDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
