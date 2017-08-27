using MongoDbGenericRepository;

namespace IntegrationTests.Infrastructure
{
    public class TestRepository : BaseMongoRepository, ITestRepository
    {
        /// <inheritdoc />
        public TestRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        public void DropTestCollection<TDocument>()
        {
            _mongoDbContext.DropCollection<TDocument>();
        }
    }
}
