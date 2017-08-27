using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;

namespace IntegrationTests
{

    public class InsertTestsRepository : BaseMongoRepository
    {
        /// <inheritdoc />
        public InsertTestsRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        public void DropTestCollection<TDocument>()
        {
            _mongoDbContext
        }
    }

    public class InsertTests
    {

        private class InsertTestsDocument : Document
        {
            public InsertTestsDocument()
            {
                Version = 2;
            }
            public string SomeContent { get; set; }
        }


        private void Cleanup(InsertTestsDocument document)
        {
            SUT.DeleteOne(document);
            Assert.AreEqual(0, SUT.Count<InsertTestsDocument>(e => e.Id == document.Id));
        }

        private void Cleanup(List<InsertTestsDocument> documents)
        {
            SUT.DeleteMany(documents);
            SUT.Count<InsertTestsDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
        }

        /// <summary>
        /// SUT: System Under Test
        /// </summary>
        private static InsertTestsRepository SUT { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDbTests"].ConnectionString;
            SUT = new InsertTestsRepository(connectionString, "MongoDbTests");
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
            // Cleanup
            Cleanup(document);
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
            // Cleanup
            Cleanup(documents);
        }
    }
}
