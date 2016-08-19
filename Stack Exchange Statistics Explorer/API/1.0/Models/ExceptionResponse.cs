using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Models
{
    public class ExceptionResponse : IBaseModel
    {
        public ExceptionResponse(Exception e)
        {
            Message = e.Message;
        }

        public string Message { get; set; }
    }
}
