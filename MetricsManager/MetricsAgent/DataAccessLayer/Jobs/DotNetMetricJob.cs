using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;

        // счетчик для метрики DotNet
        private PerformanceCounter _dotNetCounter;


        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all heaps", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости DotNet
            var DotNetUsageInPercents = Convert.ToInt32(_dotNetCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow;

            // теперь можно записать метрику в БД
            _repository.Create(new DotNetMetric { Time = time, Value = DotNetUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
