using IntegrationTests.Infrastructure;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class UpdateTestsPartitionedDocument : PartitionedDocument
    {
        public UpdateTestsPartitionedDocument() : base("TestPartitionKey")
        {
            Version = 2;
            Children = new List<Child>();
        }
        public string SomeContent { get; set; }
        public List<Child> Children { get; set; }
    }

    public class UpdatePartitionedTests : BaseMongoDbRepositoryTests<UpdateTestsPartitionedDocument>
    {
        [Test]
        public void PartitionedUpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneContent";
            // Act
            var result = SUT.UpdateOne(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneContent", updatedDocument.SomeContent);
        }

        [Test]
        public async Task PartitionedUpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneAsyncContent";
            // Act
            var result = await SUT.UpdateOneAsync(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneAsyncContent", updatedDocument.SomeContent);
        }

        [Test]
        public async Task UpdateOneAsyncWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<UpdateTestsPartitionedDocument>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<UpdateTestsPartitionedDocument>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = await SUT.UpdateOneAsync<UpdateTestsPartitionedDocument>(document, updateDef);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, document.PartitionKey);
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
            SUT.AddOne<UpdateTestsPartitionedDocument>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<UpdateTestsPartitionedDocument>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = SUT.UpdateOne<UpdateTestsPartitionedDocument>(document, updateDef);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, document.PartitionKey);
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
            SUT.AddOne(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            // Act
            var result = SUT.UpdateOne(document, x => x.Children, childrenToAdd);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, document.PartitionKey);
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
            SUT.AddOne(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            // Act
            var result = await SUT.UpdateOneAsync(document, x => x.Children, childrenToAdd);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, document.PartitionKey);
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
            SUT.AddOne(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            // Act
            var filter = Builders<UpdateTestsPartitionedDocument>.Filter.Eq("Id", document.Id);
            var result = SUT.UpdateOne(filter, x => x.Children, childrenToAdd, document.PartitionKey);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, document.PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }

        [Test]
        public async Task UpdateOneAsyncWithFilterAndFieldSelector()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            // Act
            var filter = Builders<UpdateTestsPartitionedDocument>.Filter.Eq("Id", document.Id);
            var result = await SUT.UpdateOneAsync(filter, x => x.Children, childrenToAdd, document.PartitionKey);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, document.PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }
    }
}
