using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MetricsAgent.DAL;
using System.Data.SQLite;
using AutoMapper;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetWorkMetricsRepository, NetWorkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                string[] tableNames = new string[]
                {
                    "cpumetrics",
                    "dotnetmetrics",
                    "hddmetrics",
                    "networkmetrics",
                    "rammetrics"
                };

                // удаляем таблицы с метриками если уже существует в базе данных
                foreach (string name in tableNames)
                {
                    command.CommandText = $"DROP TABLE IF EXISTS {name};";
                    command.ExecuteNonQuery();
                }

                //добавляем новые таблицы
                foreach (string name in tableNames)
                {                    
                    command.CommandText = $"CREATE TABLE {name}(id INTEGER PRIMARY KEY, value INT, time BIGINT);";
                    command.ExecuteNonQuery();
                }

                //добавим тестовые данные в таблицы
                byte valueShifter = 0;//для различия в данных
                foreach (string name in tableNames)
                {
                    for (int i = 0; i < 60; i += 5)
                    {
                        long time = System.DateTimeOffset.Parse("2021-05-01 00:" + i + ":00-00:00").ToUnixTimeSeconds();

                        command.CommandText = $"INSERT INTO {name}(value, time) VALUES({i + valueShifter},{time});";
                        command.ExecuteNonQuery();
                    }
                    valueShifter++;
                }
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
