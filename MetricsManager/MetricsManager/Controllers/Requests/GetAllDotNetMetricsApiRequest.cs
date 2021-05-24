using System;

namespace MetricsManager.Controllers.Requests
{
    public class GetAllDotNetMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }
    }
}
