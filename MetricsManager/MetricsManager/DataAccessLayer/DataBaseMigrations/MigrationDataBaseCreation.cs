using FluentMigrator;

namespace MetricsManager.DataAccessLayer.DataBaseMigrations
{
    [Migration(1)]
    public class MigrationFirst : Migration
    {
        public override void Up()
        {
            string[] tableNames = new string[]
            {
                "cpumetrics",
                "dotnetmetrics",
                "hddmetrics",
                "networkmetrics",
                "rammetrics"
            };

            //добавляем новые таблицы
            foreach (string name in tableNames)
            {
                Create.Table(name)
                    .WithColumn("id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("AgentId").AsInt32()
                    .WithColumn("Value").AsInt32()
                    .WithColumn("Time").AsInt64();
            }

            Create.Table("agents")
                .WithColumn("AgentId").AsInt32().PrimaryKey().Identity()
                .WithColumn("AgentAddress").AsString().Unique();
        }

        public override void Down()
        {
            // empty, not used        
        }
    }
}