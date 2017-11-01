using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using Xunit;
using System.Threading.Tasks;

namespace CoreIntegrationTests
{
    public class UpdateTestsPartitionedDocument : PartitionedDocument
    {
        public UpdateTestsPartitionedDocument() : base("TestPartitionKey")
        {
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    public class UpdatePartitionedTests : BaseMongoDbRepositoryTests<UpdateTestsPartitionedDocument>
    {
        [Fact]
        public void PartitionedUpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneContent";
            // Act
            var result = SUT.UpdateOne(document);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, PartitionKey);
            Assert.NotNull(updatedDocument);
            Assert.Equal("UpdateOneContent", updatedDocument.SomeContent);
        }

        [Fact]
        public async Task PartitionedUpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneAsyncContent";
            // Act
            var result = await SUT.UpdateOneAsync(document);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, PartitionKey);
            Assert.NotNull(updatedDocument);
            Assert.Equal("UpdateOneAsyncContent", updatedDocument.SomeContent);
        }
    }
}
