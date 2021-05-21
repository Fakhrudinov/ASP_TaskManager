using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface ICpuMetricsRepository : IRepository<CpuMetric> { }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        public IList<CpuMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM cpumetrics WHERE (time>=@fromTime) AND (time<=@toTime)";
            cmd.Parameters.AddWithValue("@fromTime", fromTime);
            cmd.Parameters.AddWithValue("@toTime", toTime);
            cmd.Prepare();

            var returnList = new List<CpuMetric>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new CpuMetric
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

        // just for hystory

        //public void Create(CpuMetric item)
        //{
        //    using var connection = new SQLiteConnection(ConnectionString);
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
