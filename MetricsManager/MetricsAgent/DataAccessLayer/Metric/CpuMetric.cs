using System;

namespace MetricsAgent
{
    //служит как объект для общения с БД
    public class CpuMetric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
