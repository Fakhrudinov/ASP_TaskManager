using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class HddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }

    public class HddMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}