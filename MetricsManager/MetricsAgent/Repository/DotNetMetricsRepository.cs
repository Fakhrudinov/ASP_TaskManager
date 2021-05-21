using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric> { }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        public IList<DotNetMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE (time>=@fromTime) AND (time<=@toTime)";
            cmd.Parameters.AddWithValue("@fromTime", fromTime);
            cmd.Parameters.AddWithValue("@toTime", toTime);
            cmd.Prepare();

            var returnList = new List<DotNetMetric>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new DotNetMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)).ToUniversalTime()
                    });
                }
            }

            return returnList;
        }
    }
}