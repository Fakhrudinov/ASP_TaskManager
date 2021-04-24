using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class ValuesHolder
    {
        public List<DataAndTemp> Values { get; set; }
        public ValuesHolder()
        {
            Values = new List<DataAndTemp>();
        }
    }
}
