using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using Xunit;
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
        [Fact]
        public void DeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne(document);
            // Assert
            Assert.Equal(1, result);
            Assert.False(SUT.Any<DeleteTestsDocument>(e => e.Id == document.Id));
        }

        [Fact]
        public void DeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne<DeleteTestsDocument>(e => e.Id == document.Id);
            // Assert
            Assert.Equal(1, result);
            Assert.False(SUT.Any<DeleteTestsDocument>(e => e.Id == document.Id));
        }

        [Fact]
        public async Task DeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.DeleteOneAsync(document);
            // Assert
            Assert.Equal(1, result);
            Assert.False(SUT.Any<DeleteTestsDocument>(e => e.Id == document.Id));
        }

        [Fact]
        public async Task DeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = await SUT.DeleteOneAsync<DeleteTestsDocument>(e => e.Id == document.Id);
            // Assert
            Assert.Equal(1, result);
            Assert.False(SUT.Any<DeleteTestsDocument>(e => e.Id == document.Id));
        }

        [Fact]
        public async Task DeleteManyAsyncLinq()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.DeleteManyAsync<DeleteTestsDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent");
            // Assert
            Assert.Equal(5, result);
            Assert.False(SUT.Any<DeleteTestsDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent"));
        }

        [Fact]
        public async Task DeleteManyAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = "DeleteManyAsyncLinqContent");
            SUT.AddMany(documents);
            // Act
            var result = await SUT.DeleteManyAsync(documents);
            // Assert
            Assert.Equal(5, result);
            Assert.False(SUT.Any<DeleteTestsDocument>(e => e.SomeContent == "DeleteManyAsyncLinqContent"));
        }

        [Fact]
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
            Assert.Equal(5, result);
            Assert.False(SUT.Any<DeleteTestsDocument>(e => e.SomeContent == content));
        }

        [Fact]
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
            Assert.Equal(5, result);
            Assert.False(SUT.Any<DeleteTestsDocument>(e => e.SomeContent == content));
        }
    }
}
