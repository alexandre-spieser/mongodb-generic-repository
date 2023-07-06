using System;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.DataAccess.Create;
using MongoDbGenericRepository.DataAccess.Delete;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.DataAccess.Read;
using MongoDbGenericRepository.DataAccess.Update;

namespace CoreUnitTests.Infrastructure;

public class TestKeyedMongoRepository<TKey> : BaseMongoRepository<TKey>
    where TKey : IEquatable<TKey>
{
    public TestKeyedMongoRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase)
    {
    }

    public void SetIndexHandler(IMongoDbIndexHandler indexHandler) => MongoDbIndexHandler = indexHandler;

    public void SetDbCreator(IMongoDbCreator creator) => MongoDbCreator = creator;

    public void SetReader(IMongoDbReader reader) => MongoDbReader = reader;

    public void SetEraser(IMongoDbEraser eraser) => MongoDbEraser = eraser;

    public void SetUpdater(IMongoDbUpdater updater) => MongoDbUpdater = updater;
}
