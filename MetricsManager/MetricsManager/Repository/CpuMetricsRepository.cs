using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface ICpuMetricsRepository : IRepository<CpuMetric> { }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        public IList<CpuMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            //using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            //cmd.CommandText = "SELECT * FROM cpumetrics WHERE (agentId=@agentId) and ((time>=@fromTime) AND (time<=@toTime))";
            //cmd.Parameters.AddWithValue("@agentId", agentId);
            //cmd.Parameters.AddWithValue("@fromTime", fromTime);
            //cmd.Parameters.AddWithValue("@toTime", toTime);
            //cmd.Prepare();
            var returnList = new List<CpuMetric>();


            return returnList;
        }

        public IList<CpuMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            //using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            var returnList = new List<CpuMetric>();


            return returnList;
        }
    }
}