using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class CreateTestsTKeyDocument : IDocument<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public CreateTestsTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    [TestFixture]
    public class CreateTKeyTests : BaseMongoDbRepositoryTests<CreateTestsTKeyDocument>
    {
        [Test]
        public void TKeyAddOne()
        {
            // Arrange
            var document = new CreateTestsTKeyDocument();
            // Act
            SUT.AddOne<CreateTestsTKeyDocument, Guid>(document);
            // Assert
            long count = SUT.Count<CreateTestsTKeyDocument, Guid>(e => e.Id == document.Id);
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task TKeyAddOneAsync()
        {
            // Arrange
            var document = new CreateTestsTKeyDocument();
            // Act
            await SUT.AddOneAsync<CreateTestsTKeyDocument, Guid>(document);
            // Assert
            long count = SUT.Count<CreateTestsTKeyDocument, Guid>(e => e.Id == document.Id);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void TKeyAddMany()
        {
            // Arrange
            var documents = new List<CreateTestsTKeyDocument> { new CreateTestsTKeyDocument(), new CreateTestsTKeyDocument() };
            // Act
            SUT.AddMany<CreateTestsTKeyDocument, Guid>(documents);
            // Assert
            long count = SUT.Count<CreateTestsTKeyDocument, Guid>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.AreEqual(2, count);
        }

        [Test]
        public async Task TKeyAddManyAsync()
        {
            // Arrange
            var documents = new List<CreateTestsTKeyDocument> { new CreateTestsTKeyDocument(), new CreateTestsTKeyDocument() };
            // Act
            await SUT.AddManyAsync<CreateTestsTKeyDocument, Guid>(documents);
            // Assert
            long count = SUT.Count<CreateTestsTKeyDocument, Guid>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.AreEqual(2, count);
        }
    }
}
