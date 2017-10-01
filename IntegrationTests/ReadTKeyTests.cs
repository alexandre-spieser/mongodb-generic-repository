using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class ReadTestsTKeyDocument : IDocument<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public ReadTestsTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    [TestFixture]
    public class ReadTKeyTests : BaseMongoDbRepositoryTests<ReadTestsTKeyDocument>
    {
        [Test]
        public async Task GetByIdAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.GetByIdAsync<ReadTestsTKeyDocument, Guid>(document.Id);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetById()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var result = SUT.GetById<ReadTestsTKeyDocument, Guid>(document.Id);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.GetOneAsync<ReadTestsTKeyDocument, Guid>(x => x.Id == document.Id);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var result = SUT.GetOne<ReadTestsTKeyDocument, Guid>(x => x.Id == document.Id);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetCursor()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var cursor = SUT.GetCursor<ReadTestsTKeyDocument, Guid>(x => x.Id == document.Id);
            var count = cursor.Count();
            // Assert
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task AnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.AnyAsync<ReadTestsTKeyDocument, Guid>(x => x.Id == document.Id);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.AnyAsync<ReadTestsTKeyDocument, Guid>(x => x.Id == Guid.NewGuid());
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var result = SUT.Any<ReadTestsTKeyDocument, Guid>(x => x.Id == document.Id);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<ReadTestsTKeyDocument, Guid>(document);
            // Act
            var result = SUT.Any<ReadTestsTKeyDocument, Guid>(x => x.Id == Guid.NewGuid());
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task GetAllAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "GetAllAsyncContent");
            SUT.AddMany<ReadTestsTKeyDocument, Guid>(documents);
            // Act
            var result = await SUT.GetAllAsync<ReadTestsTKeyDocument, Guid>(x => x.SomeContent == "GetAllAsyncContent");
            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void GetAll()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "GetAllContent");
            SUT.AddMany<ReadTestsTKeyDocument, Guid>(documents);
            // Act
            var result = SUT.GetAll<ReadTestsTKeyDocument, Guid>(x => x.SomeContent == "GetAllContent");
            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public async Task CountAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "CountAsyncContent");
            SUT.AddMany<ReadTestsTKeyDocument, Guid>(documents);
            // Act
            var result = await SUT.CountAsync<ReadTestsTKeyDocument, Guid>(x => x.SomeContent == "CountAsyncContent");
            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Count()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "CountContent");
            SUT.AddMany<ReadTestsTKeyDocument, Guid>(documents);
            // Act
            var result = SUT.Count<ReadTestsTKeyDocument, Guid>(x => x.SomeContent == "CountContent");
            // Assert
            Assert.AreEqual(5, result);
        }
    }
}
