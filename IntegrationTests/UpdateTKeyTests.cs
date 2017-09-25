using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntegrationTests
{

    public class UpdateTestsTKeyDocument : IDocument<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public UpdateTestsTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    [TestFixture]
    public class UpdateTKeyTests : BaseMongoDbRepositoryTests<UpdateTestsTKeyDocument>
    {
        [Test]
        public void UpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsTKeyDocument, Guid>(document);
            document.SomeContent = "UpdateOneContent";
            // Act
            var result = SUT.UpdateOne<UpdateTestsTKeyDocument, Guid>(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsTKeyDocument, Guid>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneContent", updatedDocument.SomeContent);
        }

        [Test]
        public async Task UpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsTKeyDocument, Guid>(document);
            document.SomeContent = "UpdateOneAsyncContent";
            // Act
            var result = await SUT.UpdateOneAsync<UpdateTestsTKeyDocument, Guid>(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsTKeyDocument, Guid>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneAsyncContent", updatedDocument.SomeContent);
        }
    }
}
