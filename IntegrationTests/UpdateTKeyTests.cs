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

    public class UpdateTestsTKeyDocument : IDocument<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public UpdateTestsTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
            Children = new List<Child>();
        }
        public string SomeContent { get; set; }
        public List<Child> Children { get; set; }
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

        [Test]
        public async Task UpdateOneAsyncWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsTKeyDocument, Guid>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<UpdateTestsTKeyDocument>.Update.AddToSetEach(p => p.Children, childrenToAdd);
            // Act
            var result = await SUT.UpdateOneAsync<UpdateTestsTKeyDocument, Guid>(document, updateDef);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsTKeyDocument, Guid>(document.Id);
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
            SUT.AddOne<UpdateTestsTKeyDocument, Guid>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<UpdateTestsTKeyDocument>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = SUT.UpdateOne<UpdateTestsTKeyDocument, Guid>(document, updateDef);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsTKeyDocument, Guid>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }

        [Test]
        public void UpdateOneWithFieldSelector()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsTKeyDocument, Guid>(document);

            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var filter = Builders<UpdateTestsTKeyDocument>.Filter.Eq("Id", document.Id);

            // Act
            var result = SUT.UpdateOne<UpdateTestsTKeyDocument, Guid, List<Child>>(document, x => x.Children, childrenToAdd);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsTKeyDocument, Guid>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }

        [Test]
        public async Task UpdateOneAsyncWithFieldSelector()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsTKeyDocument, Guid>(document);

            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var filter = Builders<UpdateTestsTKeyDocument>.Filter.Eq("Id", document.Id);

            // Act
            var result = await SUT.UpdateOneAsync<UpdateTestsTKeyDocument, Guid, List<Child>>(document, x => x.Children, childrenToAdd);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsTKeyDocument, Guid>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }

        [Test]
        public void UpdateOneWithFilterAndFieldSelector()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsTKeyDocument, Guid>(document);

            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var filter = Builders<UpdateTestsTKeyDocument>.Filter.Eq("Id", document.Id);

            // Act
            var result = SUT.UpdateOne<UpdateTestsTKeyDocument, Guid, List<Child>>(filter, x => x.Children, childrenToAdd);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsTKeyDocument, Guid>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }
    }
}
