using System.Collections.Generic;

namespace MetricsManager.DAL
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime);
        IList<T> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime);
    }
}

