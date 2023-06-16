using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.DataAccess.Create;
using MongoDbGenericRepository.DataAccess.Delete;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.DataAccess.Read;

namespace CoreUnitTests.Infrastructure;

public class TestMongoRepository : BaseMongoRepository
{
    public TestMongoRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase)
    {
    }

    public void SetIndexHandler(IMongoDbIndexHandler indexHandler)
    {
        MongoDbIndexHandler = indexHandler;
    }

    public void SetDbCreator(IMongoDbCreator creator)
    {
        MongoDbCreator = creator;
    }

    public void SetReader(IMongoDbReader reader)
    {
        MongoDbReader = reader;
    }

    public void SetEraser(IMongoDbEraser eraser)
    {
        MongoDbEraser = eraser;
    }
}