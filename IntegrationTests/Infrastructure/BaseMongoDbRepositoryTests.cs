using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Configuration;

namespace IntegrationTests.Infrastructure
{
    public class BaseMongoDbRepositoryTests<T> where T : Document, new()
    {
        public T GetDocumentInstance()
        {
            return new T();
        }

        public BaseMongoDbRepositoryTests()
        {
            var type = GetDocumentInstance();
            if (type is IPartitionedDocument)
            {
                PartitionKey = ((IPartitionedDocument)type).PartitionKey;
            }
        }

        public string PartitionKey { get; set; }

        /// <summary>
        /// SUT: System Under Test
        /// </summary>
        protected static ITestRepository SUT { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDbTests"].ConnectionString;
            SUT = new TestRepository(connectionString, "MongoDbTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            // We drop the collection at the end of each test session.
            if (!string.IsNullOrEmpty(PartitionKey))
            {
                SUT.DropTestCollection<T>(PartitionKey);
            }
            else
            {
                SUT.DropTestCollection<T>();
            }
        }
    }
}
