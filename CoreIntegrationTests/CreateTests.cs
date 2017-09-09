using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class CreateTestsDocument : Document
    {
        public CreateTestsDocument()
        {
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    public class CreateTests : BaseMongoDbRepositoryTests<CreateTestsDocument>
    {
        [Fact]
        public void AddOne()
        {
            // Arrange
            var document = new CreateTestsDocument();
            // Act
            SUT.AddOne(document);
            // Assert
            long count = SUT.Count<CreateTestsDocument>(e => e.Id == document.Id);
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new CreateTestsDocument();
            // Act
            await SUT.AddOneAsync(document);
            // Assert
            long count = SUT.Count<CreateTestsDocument>(e => e.Id == document.Id);
            Assert.Equal(1, count);
        }

        [Fact]
        public void AddMany()
        {
            // Arrange
            var documents = new List<CreateTestsDocument> { new CreateTestsDocument(), new CreateTestsDocument() };
            // Act
            SUT.AddMany(documents);
            // Assert
            long count = SUT.Count<CreateTestsDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<CreateTestsDocument> { new CreateTestsDocument(), new CreateTestsDocument() };
            // Act
            await SUT.AddManyAsync(documents);
            // Assert
            long count = SUT.Count<CreateTestsDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.Equal(2, count);
        }
    }
}
