using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class NetWorkMetricsResponse
    {
        public List<NetWorkMetricDto> Metrics { get; set; }
    }

    public class NetWorkMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
