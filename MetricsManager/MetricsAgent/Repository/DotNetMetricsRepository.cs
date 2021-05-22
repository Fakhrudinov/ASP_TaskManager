using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace MetricsAgent.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric> { }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        // добавляем парсинг для типа данных DateTimeOffset в возвращаемые значения
        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public IList<DotNetMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime,
                    }).ToList();
            }
        }
    }
}