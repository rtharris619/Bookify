using System.Data;

namespace Bookify.Application.Abstractions.Data;

internal interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
