using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;


namespace MetricsAgent.Controllers
{
    [Route("api/sql")]
    [ApiController]
    public class SQLController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        public SQLController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в SQLController");
        }

        [HttpGet("version")]
        public IActionResult TryToSqlLite()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();

                _logger.LogInformation($"{System.DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent / TryToSqlLite " +
                    $"api/sql/version {version}");

                return Ok(version);
            }
        }

        [HttpGet("read-write-test")]
        public IActionResult TryToInsertAndRead()
        {
            // Создаем строку подключения в виде базы данных в оперативной памяти
            string connectionString = "Data Source=:memory:";

            // создаем соединение с базой данных
            using (var connection = new SQLiteConnection(connectionString))
            {
                // открываем соединение
                connection.Open();

                // создаем объект через который будут выполняться команды к базе данных
                using (var command = new SQLiteCommand(connection))
                {
                    // задаем новый текст команды для выполнения
                    // удаляем таблицу с метриками если она существует в базе данных
                    command.CommandText = "DROP TABLE IF EXISTS cpumetrics";                    
                    command.ExecuteNonQuery();// отправляем запрос в базу данных

                    // создаем таблицу с метриками
                    command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();

                    // создаем запрос на вставку данных
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(11,222)";
                    command.ExecuteNonQuery();
                    
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(55,4444)";
                    command.ExecuteNonQuery();
                    
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(66,6666)";
                    command.ExecuteNonQuery();
                    
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(77,7777)";
                    command.ExecuteNonQuery();

                    // создаем строку для выборки данных из базы
                    string readQuery = "SELECT * FROM cpumetrics";

                    // создаем массив, в который запишем объекты с данными из базы данных
                    var returnArray = new CpuMetricDto[4];
                    // изменяем текст команды на наш запрос чтения
                    command.CommandText = readQuery;

                    // создаем читалку из базы данных
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // счетчик для того, чтобы записать объект в правильное место в массиве
                        var counter = 0;
                        // цикл будет выполняться до тех пор, пока есть что читать из базы данных
                        while (reader.Read())
                        {
                            // создаем объект и записываем его в массив
                            returnArray[counter] = new CpuMetricDto
                            {
                                Id = reader.GetInt32(0), // читаем данные полученные из базы данных
                                Value = reader.GetInt32(1), // преобразуя к целочисленному типу
                                Time = (int)reader.GetInt64(2)
                            };
                            // увеличиваем значение счетчика
                            counter++;
                        }
                    }

                    string resultStr = "";
                    foreach (var item in returnArray)
                    {
                        resultStr = resultStr + item.Time + " " + item.Id + " " + item.Value + " \r\n";
                    }
                    _logger.LogInformation($"{System.DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent / TryToInsertAndRead " +
                        $"api/sql/read-write-test result: {resultStr}");

                    // оборачиваем массив с данными в объект ответа и возвращаем пользователю 
                    return Ok(returnArray);
                }
            }
        }
    }
}

