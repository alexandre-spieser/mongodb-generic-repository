using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class DeleteTestsPartitionedTKeyDocument : IPartitionedDocument, IDocument<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DeleteTestsPartitionedTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
            PartitionKey = "TestPartitionKey";
        }
        public string PartitionKey { get; set; }
        public string SomeContent { get; set; }
    }

    public class DeletePartitionedTKeyTests : BaseMongoDbRepositoryTests<DeleteTestsPartitionedTKeyDocument>
    {
        [Test]
        public void PartitionedDeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<DeleteTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = SUT.DeleteOne<DeleteTestsPartitionedTKeyDocument, Guid>(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.Id == document.Id, PartitionKey));
        }

        [Test]
        public void PartitionedDeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<DeleteTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = SUT.DeleteOne<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.Id == document.Id, PartitionKey);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.Id == document.Id, PartitionKey));
        }

        [Test]
        public async Task PartitionedDeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<DeleteTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.DeleteOneAsync<DeleteTestsPartitionedTKeyDocument, Guid>(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.Id == document.Id, PartitionKey));
        }

        [Test]
        public async Task PartitionedDeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<DeleteTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.DeleteOneAsync<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.Id == document.Id, PartitionKey);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.Id == document.Id, PartitionKey));
        }

        [Test]
        public async Task PartitionedDeleteManyAsyncLinq()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany<DeleteTestsPartitionedTKeyDocument, Guid>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey));
        }

        [Test]
        public async Task PartitionedDeleteManyAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany<DeleteTestsPartitionedTKeyDocument, Guid>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<DeleteTestsPartitionedTKeyDocument, Guid>(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey));
        }

        [Test]
        public void PartitionedDeleteManyLinq()
        {
            // Arrange
            var content = "DeleteManyLinqContent";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<DeleteTestsPartitionedTKeyDocument, Guid>(documents);
            // Act
            var result = SUT.DeleteMany<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.SomeContent == content, PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.SomeContent == content, PartitionKey));
        }

        [Test]
        public void PartitionedDeleteMany()
        {
            // Arrange
            var content = "DeleteManyContent";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<DeleteTestsPartitionedTKeyDocument, Guid>(documents);
            // Act
            var result = SUT.DeleteMany<DeleteTestsPartitionedTKeyDocument, Guid>(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedTKeyDocument, Guid>(e => e.SomeContent == content, PartitionKey));
        }
    }
}
