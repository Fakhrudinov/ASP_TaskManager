using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class CpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }

    public class CpuMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
