using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            Children = new List<Child>();
        }
        public string PartitionKey { get; set; }
        public string SomeContent { get; set; }
        public List<Child> Children { get; set; }
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

        [Test]
        public async Task UpdateOneAsyncWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsPartitionedTKeyDocument, Guid>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<UpdateTestsPartitionedTKeyDocument>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = await SUT.UpdateOneAsync<UpdateTestsPartitionedTKeyDocument, Guid>(document, updateDef);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedTKeyDocument, Guid>(document.Id, document.PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }

        [Test]
        public void UpdateOneWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsPartitionedTKeyDocument, Guid>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<UpdateTestsPartitionedTKeyDocument>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = SUT.UpdateOne<UpdateTestsPartitionedTKeyDocument, Guid>(document, updateDef);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedTKeyDocument, Guid>(document.Id, document.PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }
    }
}
