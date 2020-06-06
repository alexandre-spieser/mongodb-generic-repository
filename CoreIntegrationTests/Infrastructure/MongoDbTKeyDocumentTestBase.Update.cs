using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoreIntegrationTests.Infrastructure
{
    public abstract partial class MongoDbTKeyDocumentTestBase<T, TKey> :
        IClassFixture<MongoDbTestFixture<T, TKey>>
        where T : TestDoc<TKey>, new()
        where TKey : IEquatable<TKey>

    {
        #region Update One

        [Fact]
        public void UpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var content = GetContent();
            document.SomeContent = content;
            // Act
            var result = SUT.UpdateOne<T, TKey>(document);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public async Task UpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var content = GetContent();
            document.SomeContent = content;
            // Act
            var result = await SUT.UpdateOneAsync<T, TKey>(document);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public void UpdateOneField()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, TKey, string>(document, x => x.SomeContent, content);
            // Assert
            Assert.True(result, GetTestName());
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public async Task UpdateOneFieldAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, TKey, string>(document, x => x.SomeContent, content);
            // Assert
            Assert.True(result, GetTestName());
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public void UpdateOneFieldWithFilter()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, TKey, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result, GetTestName());
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public async Task UpdateOneFieldWithFilterAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, TKey, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result, GetTestName());
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public async Task UpdateOneAsyncWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = MongoDB.Driver.Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = await SUT.UpdateOneAsync<T, TKey>(document, updateDef);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument);
            Assert.True(childrenToAdd[0].Type == updatedDocument.Children[0].Type, GetTestName());
            Assert.True(childrenToAdd[0].Value == updatedDocument.Children[0].Value, GetTestName());
            Assert.True(childrenToAdd[1].Type == updatedDocument.Children[1].Type, GetTestName());
            Assert.True(childrenToAdd[1].Value == updatedDocument.Children[1].Value, GetTestName());
        }

        [Fact]
        public void UpdateOneWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = MongoDB.Driver.Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = SUT.UpdateOne<T, TKey>(document, updateDef);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument);
            Assert.True(childrenToAdd[0].Type == updatedDocument.Children[0].Type, GetTestName());
            Assert.True(childrenToAdd[0].Value == updatedDocument.Children[0].Value, GetTestName());
            Assert.True(childrenToAdd[1].Type == updatedDocument.Children[1].Type, GetTestName());
            Assert.True(childrenToAdd[1].Value == updatedDocument.Children[1].Value, GetTestName());
        }

        #endregion Update One

        #region Update Many

        [Fact]
        public async Task UpdateManyWithLinqFilterAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T, TKey>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var content = GetContent();
            // Act
            var result = await SUT.UpdateManyAsync<T, TKey, string>(x => docIds.Contains(x.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocument = SUT.GetAll<T, TKey>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocument.Count == 2);
            Assert.True(updatedDocument.All(u => u.SomeContent == content), GetTestName());
        }

        [Fact]
        public async Task UpdateManyWithFilterDefinitionAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T, TKey>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var filterDefinition = Builders<T>.Filter.Where(x => docIds.Contains(x.Id));
            var content = GetContent();
            // Act
            var result = await SUT.UpdateManyAsync<T, TKey, string>(filterDefinition, x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocument = SUT.GetAll<T, TKey>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocument.Count == 2);
            Assert.True(updatedDocument.All(u => u.SomeContent == content), GetTestName());
        }

        [Fact]
        public async Task UpdateManyWithLinqFilterAndUpdateDefinitionAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T, TKey>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateManyAsync<T, TKey>(x => docIds.Contains(x.Id), updateDef, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocuments = SUT.GetAll<T, TKey>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocuments.Count == 2);
            updatedDocuments.ForEach(updatedDocument =>
            {
                Assert.True(childrenToAdd[0].Type == updatedDocument.Children[0].Type, GetTestName());
                Assert.True(childrenToAdd[0].Value == updatedDocument.Children[0].Value, GetTestName());
                Assert.True(childrenToAdd[1].Type == updatedDocument.Children[1].Type, GetTestName());
                Assert.True(childrenToAdd[1].Value == updatedDocument.Children[1].Value, GetTestName());
            });
        }

        [Fact]
        public async Task UpdateManyWithFilterAndUpdateDefinitionsAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T, TKey>(documents);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            var docIds = documents.Select(u => u.Id).ToArray();
            var filterDefinition = Builders<T>.Filter.Where(x => docIds.Contains(x.Id));
            var content = GetContent();
            // Act
            var result = await SUT.UpdateManyAsync<T, TKey>(filterDefinition, updateDef, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocuments = SUT.GetAll<T, TKey>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocuments.Count == 2);
            updatedDocuments.ForEach(updatedDocument =>
            {
                Assert.True(childrenToAdd[0].Type == updatedDocument.Children[0].Type, GetTestName());
                Assert.True(childrenToAdd[0].Value == updatedDocument.Children[0].Value, GetTestName());
                Assert.True(childrenToAdd[1].Type == updatedDocument.Children[1].Type, GetTestName());
                Assert.True(childrenToAdd[1].Value == updatedDocument.Children[1].Value, GetTestName());
            });
        }


        [Fact]
        public void UpdateManyWithLinqFilter()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T, TKey>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var content = GetContent();
            // Act
            var result = SUT.UpdateMany<T, TKey, string>(x => docIds.Contains(x.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocument = SUT.GetAll<T, TKey>(x => docIds.Contains(x.Id), PartitionKey);
            Assert.True(updatedDocument.Count == 2);
            Assert.True(updatedDocument.All(u => u.SomeContent == content), GetTestName());
        }

        [Fact]
        public void UpdateManyWithFilterDefinition()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T, TKey>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var filterDefinition = Builders<T>.Filter.Where(x => docIds.Contains(x.Id));
            var content = GetContent();
            // Act
            var result = SUT.UpdateMany<T, TKey, string>(filterDefinition, x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocument = SUT.GetAll<T, TKey>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocument.Count == 2);
            Assert.True(updatedDocument.All(u => u.SomeContent == content), GetTestName());
        }

        [Fact]
        public void UpdateManyWithLinqFilterAndUpdateDefinition()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T, TKey>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);
            var content = GetContent();
            // Act
            var result = SUT.UpdateMany<T, TKey>(x => docIds.Contains(x.Id), updateDef, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocuments = SUT.GetAll<T, TKey>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocuments.Count == 2);
            updatedDocuments.ForEach(updatedDocument =>
            {
                Assert.True(childrenToAdd[0].Type == updatedDocument.Children[0].Type, GetTestName());
                Assert.True(childrenToAdd[0].Value == updatedDocument.Children[0].Value, GetTestName());
                Assert.True(childrenToAdd[1].Type == updatedDocument.Children[1].Type, GetTestName());
                Assert.True(childrenToAdd[1].Value == updatedDocument.Children[1].Value, GetTestName());
            });
        }

        [Fact]
        public void UpdateManyWithFilterAndUpdateDefinitions()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T, TKey>(documents);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            var docIds = documents.Select(u => u.Id).ToArray();
            var filterDefinition = Builders<T>.Filter.Where(x => docIds.Contains(x.Id));
            var content = GetContent();
            // Act
            var result = SUT.UpdateMany<T, TKey>(filterDefinition, updateDef, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocuments = SUT.GetAll<T, TKey>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocuments.Count == 2);
            updatedDocuments.ForEach(updatedDocument =>
            {
                Assert.True(childrenToAdd[0].Type == updatedDocument.Children[0].Type, GetTestName());
                Assert.True(childrenToAdd[0].Value == updatedDocument.Children[0].Value, GetTestName());
                Assert.True(childrenToAdd[1].Type == updatedDocument.Children[1].Type, GetTestName());
                Assert.True(childrenToAdd[1].Value == updatedDocument.Children[1].Value, GetTestName());
            });
        }


        #endregion Update Many
    }
}
