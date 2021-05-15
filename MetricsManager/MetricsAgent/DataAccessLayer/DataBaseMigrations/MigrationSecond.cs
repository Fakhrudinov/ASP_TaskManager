using FluentMigrator;

namespace MetricsAgent.DataAccessLayer.DataBaseMigrations
{
    [Migration(2)]
    public class MigrationSecond : Migration
    {
        public override void Up()
        {
            Create.Table("dotnetmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("hddmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("rammetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            ////test data
            //long timeUnix = System.DateTimeOffset.Parse("2021-05-01 00:25:00-00:00").ToUnixTimeSeconds();
            //Insert.IntoTable("cpumetrics").Row(new { id = 1, value = 12, time = timeUnix }) ;
        }

        public override void Down()
        {
            // empty, not used
        }
    }
}
