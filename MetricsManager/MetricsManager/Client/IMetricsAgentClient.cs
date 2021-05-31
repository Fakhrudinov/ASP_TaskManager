using MetricsManager.Controllers.Requests;
using MetricsManager.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);
        DotNetMetricsResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request);
        HddMetricsResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);
        NetWorkMetricsResponse GetAllNetWorkMetrics(GetAllNetWorkMetricsApiRequest request);
        RamMetricsResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);
    }
}
