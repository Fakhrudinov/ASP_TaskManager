using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace MetricsAgent.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface ICpuMetricsRepository : IRepository<CpuMetric> { }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        // добавляем парсинг для типа данных DateTimeOffset в возвращаемые значения
        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public IList<CpuMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString))
            {
                // читаем при помощи Query и в шаблон подставляем тип данных
                // объект которого Dapper сам и заполнит его поля
                // в соответсвии с названиями колонок
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime,
                    }).ToList();
            }
        }

            // just for hystory

            // в случае необходимости добавить свою обработку данных
            //public CpuMetricsRepository()
            //{
            //    // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            //    SqlMapper.AddTypeHandler(new TimeSpanHandler());
            //}
            //public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
            //{
            //    public override TimeSpan Parse(object value)
            //        => TimeSpan.FromSeconds((long)value);

            //    public override void SetValue(System.Data.IDbDataParameter parameter, TimeSpan value)
            //        => parameter.Value = value;
            //}
            //// with dapper
            //public void Create(CpuMetric item)
            //{

            //    using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            //    {
            //        //  запрос на вставку данных с плейсхолдерами для параметров
            //        connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
            //            // анонимный объект с параметрами запроса
            //            new
            //            {
            //                // value подставится на место "@value" в строке запроса
            //                // значение запишется из поля Value объекта item
            //                value = item.Value,
            //                // записываем в поле time количество секунд
            //                time = item.Time.ToUniversalTime().ToUnixTimeSeconds()
            //            });
            //    }
            //}

            //old type without dapper
            //public void Create(CpuMetric item)
            //{
            //    using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            //    connection.Open();

            //    // создаем команду
            //    using var cmd = new SQLiteCommand(connection);

            //    // прописываем в команду SQL запрос на вставку данных
            //    cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";

            //    // добавляем параметры в запрос из нашего объекта
            //    cmd.Parameters.AddWithValue("@value", item.Value);

            //    // в таблице будем хранить время в секундах, потому преобразуем перед записью в секунды
            //    // через свойство
            //    cmd.Parameters.AddWithValue("@time", item.Time.ToUniversalTime().ToUnixTimeSeconds());
            //    // подготовка команды к выполнению
            //    cmd.Prepare();

            //    // выполнение команды
            //    cmd.ExecuteNonQuery();
            //}


            //public IList<CpuMetric> GetAll()
            //{
            //    using var connection = new SQLiteConnection(ConnectionString);
            //    connection.Open();

            //    using var cmd = new SQLiteCommand(connection);

            //    // прописываем в команду SQL запрос на получение всех данных из таблицы
            //    cmd.CommandText = "SELECT * FROM cpumetrics";

            //    var returnList = new List<CpuMetric>();
            //    using (SQLiteDataReader reader = cmd.ExecuteReader())
            //    {
            //        // пока есть что читать -- читаем
            //        while (reader.Read())
            //        {
            //            // добавляем объект в список возврата
            //            returnList.Add(new CpuMetric
            //            {
            //                Id = reader.GetInt32(0),
            //                Value = reader.GetInt32(1),
            //                // налету преобразуем прочитанные секунды в метку времени
            //                Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)).ToUniversalTime()
            //            });
            //        }
            //    }

            //    return returnList;
            //}

            //public void Delete(int id)
            //{
            //    using var connection = new SQLiteConnection(ConnectionString);
            //    connection.Open();

            //    using var cmd = new SQLiteCommand(connection);

            //    // прописываем в команду SQL запрос на удаление данных
            //    cmd.CommandText = "DELETE FROM cpumetrics WHERE id=@id";

            //    cmd.Parameters.AddWithValue("@id", id);
            //    cmd.Prepare();
            //    cmd.ExecuteNonQuery();
            //}

            //public void Update(CpuMetric item)
            //{
            //    using var connection = new SQLiteConnection(ConnectionString);
            //    connection.Open();

            //    using var cmd = new SQLiteCommand(connection);
            //    // прописываем в команду SQL запрос на обновление данных
            //    cmd.CommandText = "UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id;";
            //    cmd.Parameters.AddWithValue("@id", item.Id);
            //    cmd.Parameters.AddWithValue("@value", item.Value);
            //    cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            //    cmd.Prepare();

            //    cmd.ExecuteNonQuery();
            //}


            //public CpuMetric GetById(int id)
            //{
            //    using var connection = new SQLiteConnection(ConnectionString);
            //    connection.Open();

            //    using var cmd = new SQLiteCommand(connection);
            //    cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
            //    using (SQLiteDataReader reader = cmd.ExecuteReader())
            //    {
            //        // если удалось что то прочитать
            //        if (reader.Read())
            //        {
            //            // возвращаем прочитанное
            //            return new CpuMetric
            //            {
            //                Id = reader.GetInt32(0),
            //                Value = reader.GetInt32(1),
            //                Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)).ToUniversalTime()
            //            };
            //        }
            //        else
            //        {
            //            // не нашлось запись по идентификатору, не делаем ничего
            //            return null;
            //        }
            //    }
            //}
        
    }
}
