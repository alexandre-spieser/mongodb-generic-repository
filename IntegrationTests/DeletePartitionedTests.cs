using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class DeleteTestsPartitionedDocument : PartitionedDocument
    {
        public DeleteTestsPartitionedDocument() : base("TestPartitionKey")
        {
            Version = 1;
        }
        public string SomeContent { get; set; }
    }

    public class DeletePartitionedTests : BaseMongoDbRepositoryTests<DeleteTestsPartitionedDocument>
    {
        [Test]
        public void PartitionedDeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey));
        }

        [Test]
        public void PartitionedDeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey));
        }

        [Test]
        public async Task PartitionedDeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.DeleteOneAsync(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey));
        }

        [Test]
        public async Task PartitionedDeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.DeleteOneAsync<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey));
        }

        [Test]
        public async Task PartitionedDeleteManyAsyncLinq()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.DeleteManyAsync<DeleteTestsPartitionedDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey));
        }

        [Test]
        public async Task PartitionedDeleteManyAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.DeleteManyAsync(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey));
        }

        [Test]
        public void PartitionedDeleteManyLinq()
        {
            // Arrange
            var content = "DeleteManyLinqContent";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany(documents);
            // Act
            var result = SUT.DeleteMany<DeleteTestsPartitionedDocument>(e => e.SomeContent == content, PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedDocument>(e => e.SomeContent == content, PartitionKey));
        }

        [Test]
        public void PartitionedDeleteMany()
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
            Assert.IsFalse(SUT.Any<DeleteTestsPartitionedDocument>(e => e.SomeContent == content, PartitionKey));
        }
    }
}
