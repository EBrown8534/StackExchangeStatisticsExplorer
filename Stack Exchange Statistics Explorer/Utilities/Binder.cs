using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Stack_Exchange_Statistics_Explorer.Utilities
{
    public class Binder
    {
        public static T Eval<T>(IDataItemContainer container, string property) => (T)DataBinder.Eval(container.DataItem, property);
    }
}
