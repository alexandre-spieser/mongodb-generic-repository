using System;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.DataAccess.Read;

namespace CoreUnitTests.Infrastructure;

public class TestReadOnlyMongoRepository : ReadOnlyMongoRepository
{
    /// <inheritdoc />
    public TestReadOnlyMongoRepository(string connectionString, string databaseName = null)
        : base(connectionString, databaseName)
    {
    }

    /// <inheritdoc />
    public TestReadOnlyMongoRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase)
    {
    }

    /// <inheritdoc />
    public TestReadOnlyMongoRepository(IMongoDbContext mongoDbContext)
        : base(mongoDbContext)
    {
    }

    public void SetReader(IMongoDbReader reader) => MongoDbReader = reader;
}
