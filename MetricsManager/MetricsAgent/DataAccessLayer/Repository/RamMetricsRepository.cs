﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace MetricsAgent.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface IRamMetricsRepository : IRepository<RamMetric> { }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        // добавляем парсинг для типа данных DateTimeOffset в возвращаемые значения
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public IList<RamMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime,
                    }).ToList();
            }
        }
        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUniversalTime().ToUnixTimeSeconds()
                    });
            }
        }
    }
}