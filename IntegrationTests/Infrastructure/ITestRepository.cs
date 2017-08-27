using MongoDbGenericRepository;

namespace IntegrationTests
{
    public interface ITestRepository : IBaseMongoRepository
    {
        void DropTestCollection<TDocument>();
    }
}