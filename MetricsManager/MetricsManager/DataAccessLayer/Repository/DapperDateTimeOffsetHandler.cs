using Dapper;
using System.Data;
using System;

namespace MetricsManager.DataAccessLayer.Repository
{
    // задаем хэндлер для парсинга значений в TimeSpan если таковые попадутся в наших классах моделей
    public class DapperDateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value)
            => DateTimeOffset.FromUnixTimeSeconds((long)value).ToUniversalTime();

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
            => parameter.Value = value;
    }
}