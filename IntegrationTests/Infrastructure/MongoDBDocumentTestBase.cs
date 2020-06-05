﻿using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IntegrationTests.Infrastructure
{
    [TestFixture]
    public abstract class MongoDbDocumentTestBase<T> 
        where T: TestDoc, new()
    {
        public T CreateTestDocument()
        {
            return new T();
        }

        public abstract string GetClassName();

        public List<T> CreateTestDocuments(int numberOfDocumentsToCreate)
        {
            var docs = new List<T>();
            for (var i = 0; i < numberOfDocumentsToCreate; i++)
            {
                docs.Add(new T());
            }
            return docs;
        }

        /// <summary>
        /// The partition key for the collection, if any
        /// </summary>
        protected string PartitionKey { get; set; }

        /// <summary>
        /// the name of the test class
        /// </summary>
        protected string TestClassName { get; set; }

        /// <summary>
        /// The name of the document used for tests
        /// </summary>
        protected string DocumentTypeName { get; set; }

        /// <summary>
        /// SUT: System Under Test
        /// </summary>
        protected static ITestRepository SUT { get; set; }

        public MongoDbDocumentTestBase()
        {
            var type = CreateTestDocument();
            DocumentTypeName = type.GetType().FullName;
            if (type is IPartitionedDocument)
            {
                PartitionKey = ((IPartitionedDocument)type).PartitionKey;
            }
            TestClassName = GetClassName();
        }

        [OneTimeSetUp]
        public void Init()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDbTests"].ConnectionString;
            SUT = new TestRepository(connectionString, "MongoDbTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            // We drop the collection at the end of each test session.
            if (!string.IsNullOrEmpty(PartitionKey))
            {
                SUT.DropTestCollection<T>(PartitionKey);
            }
            else
            {
                SUT.DropTestCollection<T>();
            }
        }

        #region Add

        [Test]
        public void AddOne()
        {
            // Arrange
            var document = new T();
            // Act
            SUT.AddOne<T>(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(document.Id)) 
                                                            : SUT.Count<T>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.AreEqual(1, count, GetTestName());
        }

        [Test]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new T();
            // Act
            await SUT.AddOneAsync<T>(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(document.Id))
                                                            : SUT.Count<T>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.AreEqual(1, count, GetTestName());
        }

        [Test]
        public void AddMany()
        {
            // Arrange
            var documents = new List<T> { new T(), new T() };
            // Act
            SUT.AddMany<T>(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                   || e.Id.Equals(documents[1].Id))
                                                            : SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                  || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.AreEqual(2, count, GetTestName());
        }

        [Test]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<T> { new T(), new T() };
            // Act
            await SUT.AddManyAsync<T>(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                   || e.Id.Equals(documents[1].Id))
                                                            : SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                  || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.AreEqual(2, count, GetTestName());
        }


        #endregion Add

        #region Read

        [Test]
        public async Task GetByIdAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.GetByIdAsync<T>(document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result, GetTestName());
        }

        [Test]
        public void GetById()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.GetById<T>(document.Id, PartitionKey);
            // Assert
            Assert.IsNotNull(result, GetTestName());
        }

        [Test]
        public async Task GetOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.GetOneAsync<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.IsNotNull(result, GetTestName());
        }

        [Test]
        public void GetOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.GetOne<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.IsNotNull(result, GetTestName());
        }

        [Test]
        public void GetCursor()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var cursor = SUT.GetCursor<T>(x => x.Id.Equals(document.Id), PartitionKey);
            var count = cursor.CountDocuments();
            // Assert
            Assert.AreEqual(1, count, GetTestName());
        }

        [Test]
        public async Task AnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.AnyAsync<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.AreEqual(true, result, GetTestName());
        }

        [Test]
        public async Task AnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.AnyAsync<T>(x => x.Id.Equals(Guid.NewGuid()), PartitionKey);
            // Assert
            Assert.AreEqual(false, result, GetTestName());
        }

        [Test]
        public void AnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.Any<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.AreEqual(true, result, GetTestName());
        }

        [Test]
        public void AnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.Any<T>(x => x.Id.Equals(Guid.NewGuid()), PartitionKey);
            // Assert
            Assert.AreEqual(false, result, GetTestName());
        }

        [Test]
        public async Task GetAllAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<T>(documents);
            // Act
            var result = await SUT.GetAllAsync<T>(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count, GetTestName());
        }

        [Test]
        public void GetAll()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<T>(documents);
            // Act
            var result = SUT.GetAll<T>(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count, GetTestName());
        }

        [Test]
        public async Task CountAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<T>(documents);
            // Act
            var result = await SUT.CountAsync<T>(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.AreEqual(5, result, GetTestName());
        }

        [Test]
        public void Count()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<T>(documents);
            // Act
            var result = SUT.Count<T>(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.AreEqual(5, result, GetTestName());
        }

        #endregion Read

        #region Update

        [Test]
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
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument, GetTestName());
            Assert.AreEqual(content, updatedDocument.SomeContent, GetTestName());
        }

        [Test]
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
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument, GetTestName());
            Assert.AreEqual(content, updatedDocument.SomeContent, GetTestName());
        }

        [Test]
        public void UpdateOneField()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, string>(document, x => x.SomeContent, content);
            // Assert
            Assert.IsTrue(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument, GetTestName());
            Assert.AreEqual(content, updatedDocument.SomeContent, GetTestName());
        }

        [Test]
        public async Task UpdateOneFieldAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, string>(document, x => x.SomeContent, content);
            // Assert
            Assert.IsTrue(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument, GetTestName());
            Assert.AreEqual(content, updatedDocument.SomeContent, GetTestName());
        }

        [Test]
        public void UpdateOneFieldWithFilter()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.IsTrue(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument, GetTestName());
            Assert.AreEqual(content, updatedDocument.SomeContent, GetTestName());
        }

        [Test]
        public async Task UpdateOneFieldWithFilterAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.IsTrue(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument, GetTestName());
            Assert.AreEqual(content, updatedDocument.SomeContent, GetTestName());
        }

        [Test]
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
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type, GetTestName());
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value, GetTestName());
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type, GetTestName());
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value, GetTestName());
        }

        [Test]
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
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual(childrenToAdd[0].Type, updatedDocument.Children[0].Type, GetTestName());
            Assert.AreEqual(childrenToAdd[0].Value, updatedDocument.Children[0].Value, GetTestName());
            Assert.AreEqual(childrenToAdd[1].Type, updatedDocument.Children[1].Type, GetTestName());
            Assert.AreEqual(childrenToAdd[1].Value, updatedDocument.Children[1].Value, GetTestName());
        }

        #endregion Update

        #region Delete

        [Test]
        public void DeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.DeleteOne<T>(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Test]
        public void DeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.DeleteOne<T>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Test]
        public async Task DeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.DeleteOneAsync<T>(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Test]
        public async Task DeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.DeleteOneAsync<T>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Test]
        public async Task DeleteManyAsyncLinq()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany<T>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<T>(e => e.SomeContent == criteria, PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        [Test]
        public async Task DeleteManyAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany<T>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<T>(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        [Test]
        public void DeleteManyLinq()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany<T>(documents);
            // Act
            var result = SUT.DeleteMany<T>(e => e.SomeContent == criteria, PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        [Test]
        public void DeleteMany()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany<T>(documents);
            // Act
            var result = SUT.DeleteMany<T>(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        #endregion Delete

        #region Project

        [Test]
        public async Task ProjectOneAsync()
        {
            // Arrange
            var someContent = GetContent();
            var someDate = DateTime.UtcNow;
            var document = CreateTestDocument();
            document.SomeContent = someContent;
            document.Nested.SomeDate = someDate;
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.ProjectOneAsync<T, MyTestProjection>(
                x => x.Id.Equals(document.Id),
                x => new MyTestProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.IsNotNull(result, GetTestName());
            Assert.AreEqual(someContent, result.SomeContent, GetTestName());
            Assert.AreEqual(someDate.Minute, result.SomeDate.Minute, GetTestName());
            Assert.AreEqual(someDate.Second, result.SomeDate.Second, GetTestName());
        }

        [Test]
        public void ProjectOne()
        {
            // Arrange
            var someContent = GetContent();
            var someDate = DateTime.UtcNow;
            var document = CreateTestDocument();
            document.SomeContent = someContent;
            document.Nested.SomeDate = someDate;
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.ProjectOne<T, MyTestProjection>(
                x => x.Id.Equals(document.Id),
                x => new MyTestProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.IsNotNull(result, GetTestName());
            Assert.AreEqual(someContent, result.SomeContent, GetTestName());
            Assert.AreEqual(someDate.Minute, result.SomeDate.Minute, GetTestName());
            Assert.AreEqual(someDate.Second, result.SomeDate.Second, GetTestName());
        }

        [Test]
        public async Task ProjectManyAsync()
        {
            // Arrange
            var someContent = GetContent();
            var someDate = DateTime.UtcNow;
            var documents = CreateTestDocuments(5);
            documents.ForEach(e =>
            {
                e.SomeContent = someContent;
                e.Nested.SomeDate = someDate;
            });

            SUT.AddMany<T>(documents);
            // Act
            var result = await SUT.ProjectManyAsync<T, MyTestProjection>(
                x => x.SomeContent == someContent,
                x => new MyTestProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count, GetTestName());
            Assert.AreEqual(someContent, result.First().SomeContent, GetTestName());
            Assert.AreEqual(someDate.Minute, result.First().SomeDate.Minute, GetTestName());
            Assert.AreEqual(someDate.Second, result.First().SomeDate.Second, GetTestName());
        }

        [Test]
        public void ProjectMany()
        {
            // Arrange
            var someContent = GetContent();
            var someDate = DateTime.UtcNow;
            var documents = CreateTestDocuments(5);
            documents.ForEach(e =>
            {
                e.SomeContent = someContent;
                e.Nested.SomeDate = someDate;
            });

            SUT.AddMany<T>(documents);
            // Act
            var result = SUT.ProjectMany<T, MyTestProjection>(
                x => x.SomeContent == someContent,
                x => new MyTestProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count, GetTestName());
            Assert.AreEqual(someContent, result.First().SomeContent, GetTestName());
            Assert.AreEqual(someDate.Minute, result.First().SomeDate.Minute, GetTestName());
            Assert.AreEqual(someDate.Second, result.First().SomeDate.Second, GetTestName());
        }

        #endregion Project

        #region Group By

        [Test]
        public void GroupByTProjection()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            for (var i = 0; i < documents.Count - 2; i++)
            {
                documents[i].GroupingKey = 1;
                documents[i].SomeContent = $"{content}-{i}";
            }
            for (var i = 3; i < documents.Count; i++)
            {
                documents[i].GroupingKey = 2;
                documents[i].SomeContent = $"{content}-{i}";
            }
            SUT.AddMany(documents);

            // Act
            var result = SUT.GroupBy<T, int, ProjectedGroup>(
                            e => e.GroupingKey, g => new ProjectedGroup
                            {
                                Key = g.Key,
                                Content = g.Select(doc => doc.SomeContent).ToList()
                            },
                            PartitionKey);

            // Assert
            var key1Group = result.First(e => e.Key == 1);
            Assert.NotNull(key1Group);
            Assert.AreEqual(3, key1Group.Content.Count);
            var key2Group = result.First(e => e.Key == 2);
            Assert.NotNull(key2Group);
            Assert.AreEqual(2, key2Group.Content.Count);
        }

        [Test]
        public void FilteredGroupByTProjection()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            for (var i = 0; i < documents.Count - 2; i++)
            {
                documents[i].GroupingKey = 4;
                documents[i].SomeContent = $"{content}-{i}";
            }
            for (var i = 3; i < documents.Count; i++)
            {
                documents[i].GroupingKey = 5;
                documents[i].SomeContent = $"{content}-{i}";
            }
            var guid1 = Guid.NewGuid().ToString("n");
            var guid2 = Guid.NewGuid().ToString("n");
            for (var i = 0; i < documents.Count - 1; i++)
            {
                documents[i].Children = new List<Child> {
                    new Child(guid1, guid2)
                };
            }

            SUT.AddMany(documents);

            // Act
            var result = SUT.GroupBy<T, int, ProjectedGroup>(
                            e => e.Children.Any(c => c.Type == guid1),
                            e => e.GroupingKey, g => new ProjectedGroup
                            {
                                Key = g.Key,
                                Content = g.Select(doc => doc.SomeContent).ToList()
                            }, PartitionKey);

            // Assert
            var key1Group = result.First(e => e.Key == 4);
            Assert.NotNull(key1Group);
            Assert.AreEqual(3, key1Group.Content.Count);
            var key2Group = result.First(e => e.Key == 5);
            Assert.NotNull(key2Group);
            Assert.AreEqual(1, key2Group.Content.Count);
        }

        #endregion Group By

        #region Test Utils

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetParentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(2);
            var method = sf.GetMethod().DeclaringType.Name;
            return method;
        }

        private string GetTestName()
        {
            return $"{TestClassName}{PartitionKey}.{GetParentMethod()}";
        }

        private string GetContent()
        {
            return $"{TestClassName}{PartitionKey}.{Guid.NewGuid()}.{GetParentMethod()}";
        }

        #endregion Test Utils
    }


}
