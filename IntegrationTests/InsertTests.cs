using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;

namespace IntegrationTests
{

    public class TestsRepository : BaseMongoRepository, ITestsRepository
    {
        /// <inheritdoc />
        public TestsRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        public void DropTestCollection<TDocument>()
        {
            _mongoDbContext.DropCollection<TDocument>();
        }
    }

    public class InsertTests
    {
        /// <summary>
        /// SUT: System Under Test
        /// </summary>
        private static ITestsRepository SUT { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDbTests"].ConnectionString;
            SUT = new TestsRepository(connectionString, "MongoDbTests");
        }

        [Test]
        public void InsertOne()
        {
            // Arrange
            var document = new InsertTestsDocument();
            // Act
            SUT.AddOne(document);
            // Assert
            long count = SUT.Count<InsertTestsDocument>(e => e.Id == document.Id);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void InsertOneAsync()
        {
            // Arrange
            var document = new InsertTestsDocument();
            // Act
            SUT.AddOne(document);
            // Assert
            long count = SUT.Count<InsertTestsDocument>(e => e.Id == document.Id);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void InsertMany()
        {
            // Arrange
            var documents = new List<InsertTestsDocument> { new InsertTestsDocument(), new InsertTestsDocument() };
            // Act
            SUT.AddMany(documents);
            // Assert
            long count = SUT.Count<InsertTestsDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.AreEqual(2, count);
        }


        #region Utils

        private class InsertTestsDocument : Document
        {
            public InsertTestsDocument()
            {
                Version = 2;
            }
            public string SomeContent { get; set; }
        }

        #endregion
    }
}
