using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_MetricsVisualisation
{
    public static class Actions
    {
        public static string RemoveSlash(string str)
        {
            if (str.Length > 0)
            {
                if (str.Substring(str.Length - 1).Equals("/"))
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }

            if (str.Length > 0)
            {
                if (str.Substring(0, 1).Equals("/"))
                {
                    str = str.Substring(1);
                }
            }

            return str;
        }

        internal static string GetConnectionAddress(string name, string[] myUrl, string fromTime)
        {
            string toTime = DateTimeOffset.UtcNow.AddMinutes(+1).ToString("O");

            string address = myUrl[0] + "/" + myUrl[1]      // http://localhost:5080/api
            + "/" + name + myUrl[2] + "/" + myUrl[3] + "/"  // /cpumetrics/agent/1/
            + myUrl[4] + "/" + fromTime + "/"               // from/2021-05-29T06:15:56.0000000+00:00/
            + myUrl[5] + "/" + toTime;                      // to/2021-05-29T06:17:06.7984366+00:00
            Console.WriteLine(address);

            return address;
        }
    }
}
