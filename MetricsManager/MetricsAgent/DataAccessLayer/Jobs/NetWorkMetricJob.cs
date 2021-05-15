using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class NetWorkMetricJob : IJob
    {
        private INetWorkMetricsRepository _repository;

        // счетчик для метрики NetWork
        private PerformanceCounter _netWorkCounter;


        public NetWorkMetricJob(INetWorkMetricsRepository repository)
        {
            _repository = repository;

            PerformanceCounterCategory netWorkCategory = new PerformanceCounterCategory("Network Interface");
            string[] networkInstNames = netWorkCategory.GetInstanceNames();
            _netWorkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkInstNames[0]);
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости NetWork
            var NetWorkUsageInPercents = Convert.ToInt32(_netWorkCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow;

            // теперь можно записать метрику в БД
            _repository.Create(new NetWorkMetric { Time = time, Value = NetWorkUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
