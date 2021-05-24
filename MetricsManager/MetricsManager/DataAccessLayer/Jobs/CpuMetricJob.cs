using System.Threading.Tasks;
using Quartz;
using System;
using MetricsManager.DataAccessLayer.Repository;
using MetricsManager.Client;
using MetricsManager.Controllers.Requests;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly ILogger<CpuMetricJob> _logger;

        public CpuMetricJob(
            ICpuMetricsRepository repository, 
            IAgentsRepository agentsRepository, 
            IMetricsAgentClient metricsAgentClient, 
            ILogger<CpuMetricJob> logger)
        {
            _repository = repository;
            _agentsRepository = agentsRepository;
            _metricsAgentClient = metricsAgentClient;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: CpuMetricsJob started");
            var allAgentsList = _agentsRepository.GetAllAgentsList();

            foreach (var agent in allAgentsList)
            {
                var fromTime = _repository.GetLastTimeFromAgent(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                try
                {
                    _logger.LogInformation($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: CpuMetricJob try GetCpuMetrics " +
                        $"from {fromTime} to {toTime}, agentAddr {agent.AgentAddress}");

                    var outerMetrics = _metricsAgentClient.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
                    {
                        ClientBaseAddress = agent.AgentAddress,
                        FromTime = fromTime,
                        ToTime = toTime
                    });

                    if (outerMetrics != null)
                    {
                        foreach (var singleMetric in outerMetrics.Metrics)
                        {
                            _logger.LogInformation($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: CpuMetricJob write cpu metric to DB from agentId {agent.AgentId}," +
                                $" {singleMetric.Time} value:{singleMetric.Value}");

                            _repository.Create(new CpuMetric 
                            {
                                AgentId = agent.AgentId,
                                Value = singleMetric.Value,
                                Time = singleMetric.Time
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("CpuMetricJob " + ex.Message);
                }
            }

            return Task.CompletedTask;
        }
    }
}