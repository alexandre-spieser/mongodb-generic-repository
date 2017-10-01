using IntegrationTests.Infrastructure;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class UpdateTestsDocument : Document
    {
        public UpdateTestsDocument()
        {
            Version = 2;
            Children = new List<Child>();
        }
        public string SomeContent { get; set; }
        public List<Child> Children { get; set; }
    }

    public class UpdateTests : BaseMongoDbRepositoryTests<UpdateTestsDocument>
    {
        [Test]
        public void UpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneContent";
            // Act
            var result = SUT.UpdateOne(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneContent", updatedDocument.SomeContent);
        }

        [Test]
        public async Task UpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneAsyncContent";
            // Act
            var result = await SUT.UpdateOneAsync(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneAsyncContent", updatedDocument.SomeContent);
        }

        [Test]
        public async Task UpdateOneAsyncWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<UpdateTestsDocument>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = await SUT.UpdateOneAsync(document, updateDef);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
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
            SUT.AddOne(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<UpdateTestsDocument>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = SUT.UpdateOne(document, updateDef);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
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
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
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
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
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
            var filter = Builders<UpdateTestsDocument>.Filter.Eq("Id", document.Id);
            var result = SUT.UpdateOne(filter, x => x.Children, childrenToAdd);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
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
            var filter = Builders<UpdateTestsDocument>.Filter.Eq("Id", document.Id);
            var result = await SUT.UpdateOneAsync(filter, x => x.Children, childrenToAdd);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsDocument>(document.Id);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type);
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value);
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type);
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value);
        }
    }
}
