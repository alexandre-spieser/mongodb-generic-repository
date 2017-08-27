using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class CreateTestsPartitionedDocument : PartitionedDocument
    {
        public CreateTestsPartitionedDocument() : base("TestPartitionKey")
        {
            Version = 1;
        }
        public string SomeContent { get; set; }
    }

    public class CreatePartitionedTests : BaseMongoDbRepositoryTests<CreateTestsPartitionedDocument>
    {
        [Test]
        public void AddOne()
        {
            // Arrange
            var document = new CreateTestsPartitionedDocument();
            // Act
            SUT.AddOne(document);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey);
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new CreateTestsPartitionedDocument();
            // Act
            await SUT.AddOneAsync(document);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void AddMany()
        {
            // Arrange
            var documents = new List<CreateTestsPartitionedDocument> { new CreateTestsPartitionedDocument(), new CreateTestsPartitionedDocument() };
            // Act
            SUT.AddMany(documents);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id, PartitionKey);
            Assert.AreEqual(2, count);
        }

        [Test]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<CreateTestsPartitionedDocument> { new CreateTestsPartitionedDocument(), new CreateTestsPartitionedDocument() };
            // Act
            await SUT.AddManyAsync(documents);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id, PartitionKey);
            Assert.AreEqual(2, count);
        }
    }
}
