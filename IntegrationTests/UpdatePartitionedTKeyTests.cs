using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{

    public class UpdateTestsPartitionedTKeyDocument : IDocument<Guid>, IPartitionedDocument
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public UpdateTestsPartitionedTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
            PartitionKey = "TestPartitionKey";
        }
        public string PartitionKey { get; set; }
        public string SomeContent { get; set; }
    }

    [TestFixture]
    public class UpdatePartitionedTKeyTests : BaseMongoDbRepositoryTests<UpdateTestsPartitionedTKeyDocument>
    {
        [Test]
        public void PartitionedUpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsPartitionedTKeyDocument, Guid>(document);
            document.SomeContent = "UpdateOneContent";
            // Act
            var result = SUT.UpdateOne<UpdateTestsPartitionedTKeyDocument, Guid>(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedTKeyDocument, Guid>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneContent", updatedDocument.SomeContent);
        }

        [Test]
        public async Task PartitionedUpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsPartitionedTKeyDocument, Guid>(document);
            document.SomeContent = "UpdateOneAsyncContent";
            // Act
            var result = await SUT.UpdateOneAsync<UpdateTestsPartitionedTKeyDocument, Guid>(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedTKeyDocument, Guid>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneAsyncContent", updatedDocument.SomeContent);
        }
    }
}
