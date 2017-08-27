using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
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

    [TestFixture]
    public class CreateTests : BaseMongoDbRepositoryTests<CreateTestsDocument>
    {
        [Test]
        public void AddOne()
        {
            // Arrange
            var document = new CreateTestsDocument();
            // Act
            SUT.AddOne(document);
            // Assert
            long count = SUT.Count<CreateTestsDocument>(e => e.Id == document.Id);
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new CreateTestsDocument();
            // Act
            await SUT.AddOneAsync(document);
            // Assert
            long count = SUT.Count<CreateTestsDocument>(e => e.Id == document.Id);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void AddMany()
        {
            // Arrange
            var documents = new List<CreateTestsDocument> { new CreateTestsDocument(), new CreateTestsDocument() };
            // Act
            SUT.AddMany(documents);
            // Assert
            long count = SUT.Count<CreateTestsDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.AreEqual(2, count);
        }

        [Test]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<CreateTestsDocument> { new CreateTestsDocument(), new CreateTestsDocument() };
            // Act
            await SUT.AddManyAsync(documents);
            // Assert
            long count = SUT.Count<CreateTestsDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.AreEqual(2, count);
        }
    }
}
