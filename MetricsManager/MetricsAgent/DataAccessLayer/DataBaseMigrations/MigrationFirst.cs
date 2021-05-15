using FluentMigrator;

namespace MetricsAgent.DataAccessLayer.DataBaseMigrations
{
    [Migration(1)]
    public class MigrationFirst : Migration
    {        public override void Up()
        {
            Create.Table("cpumetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
        }

        public override void Down()
        {
            // empty, not used        
        }
    }
}
