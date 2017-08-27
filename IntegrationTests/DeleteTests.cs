using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class DeleteTestsDocument : Document
    {
        public DeleteTestsDocument()
        {
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    public class DeleteTests : BaseMongoDbRepositoryTests<DeleteTestsDocument>
    {
        [Test]
        public void DeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsDocument>(e => e.Id == document.Id));
        }

        [Test]
        public void DeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne<DeleteTestsDocument>(e => e.Id == document.Id);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsDocument>(e => e.Id == document.Id));
        }

        [Test]
        public async Task DeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.DeleteOneAsync(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsDocument>(e => e.Id == document.Id));
        }

        [Test]
        public async Task DeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.DeleteOneAsync<DeleteTestsDocument>(e => e.Id == document.Id);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsDocument>(e => e.Id == document.Id));
        }

        [Test]
        public async Task DeleteManyAsyncLinq()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.DeleteManyAsync<DeleteTestsDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent");
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent"));
        }

        [Test]
        public async Task DeleteManyAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.DeleteManyAsync(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent"));
        }

        [Test]
        public void DeleteManyLinq()
        {
            // Arrange
            var content = "DeleteManyLinqContent";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany(documents);
            // Act
            var result = SUT.DeleteMany<DeleteTestsDocument>(e => e.SomeContent == content);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsDocument>(e => e.SomeContent == content));
        }

        [Test]
        public void DeleteMany()
        {
            // Arrange
            var content = "DeleteManyContent";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany(documents);
            // Act
            var result = SUT.DeleteMany(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsDocument>(e => e.SomeContent == content));
        }
    }
}
