using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface IHddMetricsRepository : IRepository<HddMetric> { }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        public IList<HddMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE (agent_id=@agentId) and ((time>=@fromTime) AND (time<=@toTime))";

            cmd.Parameters.AddWithValue("@agentId", agentId);
            cmd.Parameters.AddWithValue("@fromTime", fromTime);
            cmd.Parameters.AddWithValue("@toTime", toTime);
            cmd.Prepare();

            var returnList = new List<HddMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new HddMetric
                    {
                        Id = reader.GetInt32(0),
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(3)).ToUniversalTime()
                    });
                }
            }

            return returnList;
        }

        public IList<HddMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE (time>=@fromTime) AND (time<=@toTime)";

            cmd.Parameters.AddWithValue("@fromTime", fromTime);
            cmd.Parameters.AddWithValue("@toTime", toTime);
            cmd.Prepare();

            var returnList = new List<HddMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new HddMetric
                    {
                        Id = reader.GetInt32(0),
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(3)).ToUniversalTime()
                    });
                }
            }

            return returnList;
        }
    }
}