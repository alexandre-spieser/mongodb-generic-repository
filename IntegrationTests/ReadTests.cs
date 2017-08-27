using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class ReadTestsDocument : Document
    {
        public ReadTestsDocument()
        {
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    [TestFixture]
    public class ReadTests : BaseMongoDbRepositoryTests<ReadTestsDocument>
    {
        [Test]
        public async Task GetById()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.GetByIdAsync<ReadTestsDocument>(document.Id);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.GetOneAsync<ReadTestsDocument>(x => x.Id == document.Id);
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.GetOne<ReadTestsDocument>(x => x.Id == document.Id);
            // Assert
            Assert.IsNotNull(result);
        }

        private class ProjectClass
        {
            public string Content { get; set; }
        }

        [Test]
        public void GetCursor()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var cursor = SUT.GetCursor<ReadTestsDocument>(x => x.Id == document.Id);
            var count = cursor.Count();
            // Assert
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task AnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.AnyAsync<ReadTestsDocument>(x => x.Id == document.Id);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.AnyAsync<ReadTestsDocument>(x => x.Id == Guid.NewGuid());
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.Any<ReadTestsDocument>(x => x.Id == document.Id);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.Any<ReadTestsDocument>(x => x.Id == Guid.NewGuid());
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task GetAllAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "GetAllAsyncContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.GetAllAsync<ReadTestsDocument>(x => x.SomeContent == "GetAllAsyncContent");
            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void GetAll()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "GetAllContent");
            SUT.AddMany(documents);
            // Act
            var result = SUT.GetAll<ReadTestsDocument>(x => x.SomeContent == "GetAllContent");
            // Assert
            Assert.AreEqual(5, result.Count);
        }
    }
}
