using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;

        // счетчик для метрики Hdd
        private PerformanceCounter _hddCounter;


        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "0 C:");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости Hdd
            var HddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow;

            // теперь можно записать метрику в БД
            _repository.Create(new HddMetric { Time = time, Value = HddUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
