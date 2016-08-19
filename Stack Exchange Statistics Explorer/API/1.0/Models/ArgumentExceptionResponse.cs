using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Models
{
    public class ArgumentExceptionResponse : ExceptionResponse
    {
        public ArgumentExceptionResponse(ArgumentException e)
            : base(e)
        {
            ViolatingParameter = e.ParamName;
        }

        public string ViolatingParameter { get; set; }
    }
}
