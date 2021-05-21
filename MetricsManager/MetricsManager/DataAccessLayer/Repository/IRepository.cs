using System;
using System.Collections.Generic;

namespace MetricsManager
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime);
        IList<T> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime);
        DateTimeOffset GetLastTimeFromAgent(int agent_id);
        void Create(T item);
    }
}

