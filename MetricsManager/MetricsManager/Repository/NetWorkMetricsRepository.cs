using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface INetWorkMetricsRepository : IRepository<NetWorkMetric> { }

    public class NetWorkMetricsRepository : INetWorkMetricsRepository
    {
        public IList<NetWorkMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            //using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            //cmd.CommandText = "SELECT * FROM NetWorkmetrics WHERE (agentId=@agentId) and ((time>=@fromTime) AND (time<=@toTime))";
            //cmd.Parameters.AddWithValue("@agentId", agentId);
            //cmd.Parameters.AddWithValue("@fromTime", fromTime);
            //cmd.Parameters.AddWithValue("@toTime", toTime);
            //cmd.Prepare();
            var returnList = new List<NetWorkMetric>();


            return returnList;
        }

        public IList<NetWorkMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            //using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            var returnList = new List<NetWorkMetric>();


            return returnList;
        }
    }
}