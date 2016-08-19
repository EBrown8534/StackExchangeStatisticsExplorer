using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Models
{
    public class ApiResponseWrapper<T>
        where T : IBaseModel
    {
        public List<T> Items { get; set; } = new List<T>();

        public bool HasMore { get; set; } = false;

        public int QuotaMax { get; set; } = 0;

        public int QuotaRemaining { get; set; } = 0;

        public int? Backoff { get; set; } = null;

        public bool IsError { get; set; } = false;
    }
}
