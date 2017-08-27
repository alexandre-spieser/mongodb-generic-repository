using MongoDbGenericRepository;

namespace IntegrationTests
{
    public interface ITestsRepository : IBaseMongoRepository
    {
        void DropTestCollection<TDocument>();
    }
}