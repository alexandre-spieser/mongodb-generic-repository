using System.IO;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests;

public static class ConnectionHelper
{
    public static string GetTestDatabaseConnectionString() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("MongoDbTests");
}