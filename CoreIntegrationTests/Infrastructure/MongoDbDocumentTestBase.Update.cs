using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CoreIntegrationTests.Infrastructure
{
    public abstract partial class MongoDbDocumentTestBase<T> :
        IClassFixture<MongoDbTestFixture<T, Guid>>
        where T : TestDoc, new()
    {
        #region Update One

        [Fact]
        public void UpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            document.SomeContent = content;
            // Act
            var result = SUT.UpdateOne<T>(document);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public async Task UpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            document.SomeContent = content;
            // Act
            var result = await SUT.UpdateOneAsync<T>(document);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public void UpdateOneField()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, string>(document, x => x.SomeContent, content);
            // Assert
            Assert.True(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public async Task UpdateOneFieldAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, string>(document, x => x.SomeContent, content);
            // Assert
            Assert.True(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public void UpdateOneFieldWithFilter()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public async Task UpdateOneFieldWithFilterAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.True(null != updatedDocument, GetTestName());
            Assert.True(content == updatedDocument.SomeContent, GetTestName());
        }

        [Fact]
        public async Task UpdateOneAsyncWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = MongoDB.Driver.Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = await SUT.UpdateOneAsync<T>(document, updateDef);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
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
            SUT.AddOne<T>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = MongoDB.Driver.Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = SUT.UpdateOne<T>(document, updateDef);
            // Assert
            Assert.True(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
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
            SUT.AddMany<T>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var content = GetContent();
            // Act
            var result = await SUT.UpdateManyAsync<T, string>(x => docIds.Contains(x.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocument = SUT.GetAll<T>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocument.Count == 2);
            Assert.True(updatedDocument.All(u => u.SomeContent == content), GetTestName());
        }

        [Fact]
        public async Task UpdateManyWithFilterDefinitionAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var filterDefinition = Builders<T>.Filter.Where(x => docIds.Contains(x.Id));
            var content = GetContent();
            // Act
            var result = await SUT.UpdateManyAsync<T, string>(filterDefinition, x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocument = SUT.GetAll<T>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocument.Count == 2);
            Assert.True(updatedDocument.All(u => u.SomeContent == content), GetTestName());
        }

        [Fact]
        public async Task UpdateManyWithLinqFilterAndUpdateDefinitionAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateManyAsync<T>(x => docIds.Contains(x.Id), updateDef, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocuments = SUT.GetAll<T>(x => docIds.Contains(x.Id), PartitionKey);

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
            SUT.AddMany<T>(documents);
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
            var result = await SUT.UpdateManyAsync<T>(filterDefinition, updateDef, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocuments = SUT.GetAll<T>(x => docIds.Contains(x.Id), PartitionKey);

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
            SUT.AddMany<T>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var content = GetContent();
            // Act
            var result = SUT.UpdateMany<T, string>(x => docIds.Contains(x.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocument = SUT.GetAll<T>(x => docIds.Contains(x.Id), PartitionKey);
            Assert.True(updatedDocument.Count == 2);
            Assert.True(updatedDocument.All(u => u.SomeContent == content), GetTestName());
        }

        [Fact]
        public void UpdateManyWithFilterDefinition()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var filterDefinition = Builders<T>.Filter.Where(x => docIds.Contains(x.Id));
            var content = GetContent();
            // Act
            var result = SUT.UpdateMany<T, string>(filterDefinition, x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocument = SUT.GetAll<T>(x => docIds.Contains(x.Id), PartitionKey);

            Assert.True(updatedDocument.Count == 2);
            Assert.True(updatedDocument.All(u => u.SomeContent == content), GetTestName());
        }

        [Fact]
        public void UpdateManyWithLinqFilterAndUpdateDefinition()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            SUT.AddMany<T>(documents);
            var docIds = documents.Select(u => u.Id).ToArray();
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);
            var content = GetContent();
            // Act
            var result = SUT.UpdateMany<T>(x => docIds.Contains(x.Id), updateDef, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocuments = SUT.GetAll<T>(x => docIds.Contains(x.Id), PartitionKey);

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
            SUT.AddMany<T>(documents);
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
            var result = SUT.UpdateMany<T>(filterDefinition, updateDef, PartitionKey);
            // Assert
            Assert.True(result == 2, GetTestName());
            var updatedDocuments = SUT.GetAll<T>(x => docIds.Contains(x.Id), PartitionKey);

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
