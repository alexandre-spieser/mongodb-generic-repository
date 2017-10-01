using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class ReadTestsPartitionedTKeyDocument : IDocument<Guid>, IPartitionedDocument
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public ReadTestsPartitionedTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
            PartitionKey = "TestPartitionKey";
        }
        public string PartitionKey { get; set; }
        public string SomeContent { get; set; }
    }

    [TestFixture]
    public class ReadPartitionedTKeyTests : BaseMongoDbRepositoryTests<ReadTestsPartitionedTKeyDocument>
    {
        [Test]
        public async Task PartitionedGetByIdAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.GetByIdAsync<ReadTestsPartitionedTKeyDocument, Guid>(document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PartitionedGetById()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = SUT.GetById<ReadTestsPartitionedTKeyDocument, Guid>(document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task PartitionedGetOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.GetOneAsync<ReadTestsPartitionedTKeyDocument, Guid>(x => x.Id == document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PartitionedGetOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = SUT.GetOne<ReadTestsPartitionedTKeyDocument, Guid>(x => x.Id == document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PartitionedGetCursor()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var cursor = SUT.GetCursor<ReadTestsPartitionedTKeyDocument, Guid>(x => x.Id == document.Id, PartitionKey);
            var count = cursor.Count();
            // Assert
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task PartitionedAnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.AnyAsync<ReadTestsPartitionedTKeyDocument, Guid>(x => x.Id == document.Id, PartitionKey);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task PartitionedAnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.AnyAsync<ReadTestsPartitionedTKeyDocument, Guid>(x => x.Id == Guid.NewGuid(), PartitionKey);
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PartitionedAnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = SUT.Any<ReadTestsPartitionedTKeyDocument, Guid>(x => x.Id == document.Id, PartitionKey);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PartitionedAnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = SUT.Any<ReadTestsPartitionedTKeyDocument, Guid>(x => x.Id == Guid.NewGuid(), PartitionKey);
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task PartitionedGetAllAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "GetAllAsyncContent");
            SUT.AddMany<ReadTestsPartitionedTKeyDocument, Guid>(documents);
            // Act
            var result = await SUT.GetAllAsync<ReadTestsPartitionedTKeyDocument, Guid>(x => x.SomeContent == "GetAllAsyncContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void PartitionedGetAll()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "GetAllContent");
            SUT.AddMany<ReadTestsPartitionedTKeyDocument, Guid>(documents);
            // Act
            var result = SUT.GetAll<ReadTestsPartitionedTKeyDocument, Guid>(x => x.SomeContent == "GetAllContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public async Task PartitionedCountAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "CountAsyncContent");
            SUT.AddMany<ReadTestsPartitionedTKeyDocument, Guid>(documents);
            // Act
            var result = await SUT.CountAsync<ReadTestsPartitionedTKeyDocument, Guid>(x => x.SomeContent == "CountAsyncContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void PartitionedCount()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "CountContent");
            SUT.AddMany<ReadTestsPartitionedTKeyDocument, Guid>(documents);
            // Act
            var result = SUT.Count<ReadTestsPartitionedTKeyDocument, Guid>(x => x.SomeContent == "CountContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
        }
    }
}
