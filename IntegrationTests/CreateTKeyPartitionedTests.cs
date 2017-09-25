using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class CreateTestsPartitionedTKeyDocument : IDocument<Guid>, IPartitionedDocument
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public CreateTestsPartitionedTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
            PartitionKey = "TestPartitionKey";
        }
        public string PartitionKey { get; set; }
        public string SomeContent { get; set; }
    }

    public class CreatePartitionedTKeyTests : BaseMongoDbRepositoryTests<CreateTestsPartitionedTKeyDocument>
    {
        [Test]
        public void PartitionedAddOne()
        {
            // Arrange
            var document = new CreateTestsPartitionedTKeyDocument();
            // Act
            SUT.AddOne<CreateTestsPartitionedTKeyDocument, Guid>(document);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedTKeyDocument, Guid>(e => e.Id == document.Id, PartitionKey);
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task PartitionedAddOneAsync()
        {
            // Arrange
            var document = new CreateTestsPartitionedTKeyDocument();
            // Act
            await SUT.AddOneAsync<CreateTestsPartitionedTKeyDocument, Guid>(document);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedTKeyDocument, Guid>(e => e.Id == document.Id, PartitionKey);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void PartitionedAddMany()
        {
            // Arrange
            var documents = new List<CreateTestsPartitionedTKeyDocument> { new CreateTestsPartitionedTKeyDocument(), new CreateTestsPartitionedTKeyDocument() };
            // Act
            SUT.AddMany<CreateTestsPartitionedTKeyDocument, Guid>(documents);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedTKeyDocument, Guid>(e => e.Id == documents[0].Id || e.Id == documents[1].Id, PartitionKey);
            Assert.AreEqual(2, count);
        }

        [Test]
        public async Task PartitionedAddManyAsync()
        {
            // Arrange
            var documents = new List<CreateTestsPartitionedTKeyDocument> { new CreateTestsPartitionedTKeyDocument(), new CreateTestsPartitionedTKeyDocument() };
            // Act
            await SUT.AddManyAsync<CreateTestsPartitionedTKeyDocument, Guid>(documents);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedTKeyDocument, Guid>(e => e.Id == documents[0].Id || e.Id == documents[1].Id, PartitionKey);
            Assert.AreEqual(2, count);
        }
    }
}
