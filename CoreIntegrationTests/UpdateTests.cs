using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using Xunit;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class UpdateTestsDocument : Document
    {
        public UpdateTestsDocument()
        {
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    public class UpdateTests : BaseMongoDbRepositoryTests<UpdateTestsDocument>
    {
        [Fact]
        public void UpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneContent";
            // Act
            var result = SUT.UpdateOne(document);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
            Assert.NotNull(updatedDocument);
            Assert.Equal("UpdateOneContent", updatedDocument.SomeContent);
        }

        [Fact]
        public async Task UpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneAsyncContent";
            // Act
            var result = await SUT.UpdateOneAsync(document);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
            Assert.NotNull(updatedDocument);
            Assert.Equal("UpdateOneAsyncContent", updatedDocument.SomeContent);
        }
    }
}
