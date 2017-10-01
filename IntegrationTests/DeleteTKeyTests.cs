using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class DeleteTestsTKeyDocument : IDocument<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DeleteTestsTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    public class DeleteTKeyTests : BaseMongoDbRepositoryTests<DeleteTestsTKeyDocument>
    {
        [Test]
        public void DeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<DeleteTestsTKeyDocument, Guid>(document);
            // Act
            var result = SUT.DeleteOne<DeleteTestsTKeyDocument, Guid>(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsTKeyDocument, Guid>(e => e.Id == document.Id));
        }

        [Test]
        public void DeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<DeleteTestsTKeyDocument, Guid>(document);
            // Act
            var result = SUT.DeleteOne<DeleteTestsTKeyDocument, Guid>(e => e.Id == document.Id);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsTKeyDocument, Guid>(e => e.Id == document.Id));
        }

        [Test]
        public async Task DeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<DeleteTestsTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.DeleteOneAsync<DeleteTestsTKeyDocument, Guid>(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsTKeyDocument, Guid>(e => e.Id == document.Id));
        }

        [Test]
        public async Task DeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<DeleteTestsTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.DeleteOneAsync<DeleteTestsTKeyDocument, Guid>(e => e.Id == document.Id);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsTKeyDocument, Guid>(e => e.Id == document.Id));
        }

        [Test]
        public async Task DeleteManyAsyncLinq()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany<DeleteTestsTKeyDocument, Guid>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<DeleteTestsTKeyDocument, Guid>(e => e.SomeContent == "DeleteManyAsyncLinqContent");
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsTKeyDocument, Guid>(e => e.SomeContent == "DeleteManyAsyncLinqContent"));
        }

        [Test]
        public async Task DeleteManyAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany<DeleteTestsTKeyDocument, Guid>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<DeleteTestsTKeyDocument, Guid>(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsTKeyDocument, Guid>(e => e.SomeContent == "DeleteManyAsyncLinqContent"));
        }

        [Test]
        public void DeleteManyLinq()
        {
            // Arrange
            var content = "DeleteManyLinqContent";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<DeleteTestsTKeyDocument, Guid>(documents);
            // Act
            var result = SUT.DeleteMany<DeleteTestsTKeyDocument, Guid>(e => e.SomeContent == content);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsTKeyDocument, Guid>(e => e.SomeContent == content));
        }

        [Test]
        public void DeleteMany()
        {
            // Arrange
            var content = "DeleteManyContent";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<DeleteTestsTKeyDocument, Guid>(documents);
            // Act
            var result = SUT.DeleteMany<DeleteTestsTKeyDocument, Guid>(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsTKeyDocument, Guid>(e => e.SomeContent == content));
        }
    }
}
