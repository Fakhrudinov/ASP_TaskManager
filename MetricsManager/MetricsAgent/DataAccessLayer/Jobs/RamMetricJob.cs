using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;

        // счетчик для метрики Ram
        private PerformanceCounter _ramCounter;


        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости Ram
            var RamUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow;

            // теперь можно записать метрику в БД
            _repository.Create(new RamMetric { Time = time, Value = RamUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
