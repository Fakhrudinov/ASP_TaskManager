namespace MetricsAgent
{
    //объект, который возвращается в ответ по запросу. Для работы с внешними сущностями, не с БД
    public class CpuMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
