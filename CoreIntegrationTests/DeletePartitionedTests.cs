using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using Xunit;
using System.Threading.Tasks;

namespace CoreIntegrationTests
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
        [Fact]
        public void PartitionedDeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne(document);
            // Assert
            Assert.Equal(1, result);
            Assert.False(SUT.Any<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey));
        }

        [Fact]
        public void PartitionedDeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey);
            // Assert
            Assert.Equal(1, result);
            Assert.False(SUT.Any<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey));
        }

        [Fact]
        public async Task PartitionedDeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.DeleteOneAsync(document);
            // Assert
            Assert.Equal(1, result);
            Assert.False(SUT.Any<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey));
        }

        [Fact]
        public async Task PartitionedDeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.DeleteOneAsync<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey);
            // Assert
            Assert.Equal(1, result);
            Assert.False(SUT.Any<DeleteTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey));
        }

        [Fact]
        public async Task PartitionedDeleteManyAsyncLinq()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.DeleteManyAsync<DeleteTestsPartitionedDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey);
            // Assert
            Assert.Equal(5, result);
            Assert.False(SUT.Any<DeleteTestsPartitionedDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey));
        }

        [Fact]
        public async Task PartitionedDeleteManyAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.DeleteManyAsync(documents);
            // Assert
            Assert.Equal(5, result);
            Assert.False(SUT.Any<DeleteTestsPartitionedDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent", PartitionKey));
        }

        [Fact]
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
            Assert.Equal(5, result);
            Assert.False(SUT.Any<DeleteTestsPartitionedDocument>(e => e.SomeContent == content, PartitionKey));
        }

        [Fact]
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
            Assert.Equal(5, result);
            Assert.False(SUT.Any<DeleteTestsPartitionedDocument>(e => e.SomeContent == content, PartitionKey));
        }
    }
}
