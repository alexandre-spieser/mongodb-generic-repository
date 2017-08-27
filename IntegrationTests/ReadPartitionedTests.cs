using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class ReadTestsPartitionedDocument : PartitionedDocument
    {
        public ReadTestsPartitionedDocument() : base("TestPartitionKey")
        {
            Version = 1;
        }
        public string SomeContent { get; set; }
    }

    public class ReadPartitionedTests : BaseMongoDbRepositoryTests<ReadTestsPartitionedDocument>
    {
        [Test]
        public async Task PartitionedGetByIdAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.GetByIdAsync<ReadTestsPartitionedDocument>(document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PartitionedGetById()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.GetById<ReadTestsPartitionedDocument>(document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task PartitionedGetOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.GetOneAsync<ReadTestsPartitionedDocument>(x => x.Id == document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PartitionedGetOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.GetOne<ReadTestsPartitionedDocument>(x => x.Id == document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PartitionedGetCursor()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var cursor = SUT.GetCursor<ReadTestsPartitionedDocument>(x => x.Id == document.Id, PartitionKey);
            var count = cursor.Count();
            // Assert
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task PartitionedAnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.AnyAsync<ReadTestsPartitionedDocument>(x => x.Id == document.Id, PartitionKey);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task PartitionedAnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.AnyAsync<ReadTestsPartitionedDocument>(x => x.Id == Guid.NewGuid(), PartitionKey);
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PartitionedAnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.Any<ReadTestsPartitionedDocument>(x => x.Id == document.Id, PartitionKey);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PartitionedAnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.Any<ReadTestsPartitionedDocument>(x => x.Id == Guid.NewGuid(), PartitionKey);
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task PartitionedGetAllAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "GetAllAsyncContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.GetAllAsync<ReadTestsPartitionedDocument>(x => x.SomeContent == "GetAllAsyncContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void PartitionedGetAll()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "GetAllContent");
            SUT.AddMany(documents);
            // Act
            var result = SUT.GetAll<ReadTestsPartitionedDocument>(x => x.SomeContent == "GetAllContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public async Task PartitionedCountAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "CountAsyncContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.CountAsync<ReadTestsPartitionedDocument>(x => x.SomeContent == "CountAsyncContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void PartitionedCount()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "CountContent");
            SUT.AddMany(documents);
            // Act
            var result = SUT.Count<ReadTestsPartitionedDocument>(x => x.SomeContent == "CountContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
        }
    }
}
