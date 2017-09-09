using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class CreateTestsPartitionedDocument : PartitionedDocument
    {
        public CreateTestsPartitionedDocument() : base("TestPartitionKey")
        {
            Version = 1;
        }
        public string SomeContent { get; set; }
    }

    public class CreatePartitionedTests : BaseMongoDbRepositoryTests<CreateTestsPartitionedDocument>
    {
        private void PartitionedAddOne()
        {
            // Arrange
            var document = new CreateTestsPartitionedDocument();
            // Act
            SUT.AddOne(document);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey);
            Xunit.Assert.Equal(1, count);
        }

        [Fact]
        public async Task PartitionedAddOneAsync()
        {
            // Arrange
            var document = new CreateTestsPartitionedDocument();
            // Act
            await SUT.AddOneAsync(document);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedDocument>(e => e.Id == document.Id, PartitionKey);
            Assert.Equal(1, count);
        }

        [Fact]
        public void PartitionedAddMany()
        {
            // Arrange
            var documents = new List<CreateTestsPartitionedDocument> { new CreateTestsPartitionedDocument(), new CreateTestsPartitionedDocument() };
            // Act
            SUT.AddMany(documents);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id, PartitionKey);
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task PartitionedAddManyAsync()
        {
            // Arrange
            var documents = new List<CreateTestsPartitionedDocument> { new CreateTestsPartitionedDocument(), new CreateTestsPartitionedDocument() };
            // Act
            await SUT.AddManyAsync(documents);
            // Assert
            long count = SUT.Count<CreateTestsPartitionedDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id, PartitionKey);
            Assert.Equal(2, count);
        }
    }
}
