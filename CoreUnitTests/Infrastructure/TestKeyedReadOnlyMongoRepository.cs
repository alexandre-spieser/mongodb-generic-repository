using System;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.DataAccess.Read;

namespace CoreUnitTests.Infrastructure;

public class TestKeyedReadOnlyMongoRepository<TKey> : ReadOnlyMongoRepository<TKey>
    where TKey : IEquatable<TKey>
{
    /// <inheritdoc />
    public TestKeyedReadOnlyMongoRepository(string connectionString, string databaseName = null)
        : base(connectionString, databaseName)
    {
    }

    /// <inheritdoc />
    public TestKeyedReadOnlyMongoRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase)
    {
    }

    /// <inheritdoc />
    public TestKeyedReadOnlyMongoRepository(IMongoDbContext mongoDbContext)
        : base(mongoDbContext)
    {
    }

    public void SetReader(IMongoDbReader reader) => MongoDbReader = reader;
}
