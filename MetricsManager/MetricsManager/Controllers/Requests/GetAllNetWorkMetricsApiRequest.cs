using System;

namespace MetricsManager.Controllers.Requests
{
    public class GetAllNetWorkMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }
    }
}
