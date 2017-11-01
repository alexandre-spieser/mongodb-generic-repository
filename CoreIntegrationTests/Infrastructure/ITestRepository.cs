using MongoDbGenericRepository;

namespace CoreIntegrationTests
{
    public interface ITestRepository : IBaseMongoRepository
    {
        void DropTestCollection<TDocument>();
        void DropTestCollection<TDocument>(string partitionKey);
    }
}