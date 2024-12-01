using MongoDbGenericRepository.Models;
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
    public abstract class MongoDbTKeyDocumentTestBase<T, TKey> 
        where T: TestDoc<TKey>, new()
        where TKey : IEquatable<TKey>
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

        public MongoDbTKeyDocumentTestBase()
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
            SUT.AddOne<T, TKey>(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T, TKey>(e => e.Id.Equals(document.Id)) 
                                                            : SUT.Count<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.That(count, Is.EqualTo(1), GetTestName());
        }

        [Test]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new T();
            // Act
            await SUT.AddOneAsync<T, TKey>(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? await SUT.CountAsync<T, TKey>(e => e.Id.Equals(document.Id))
                                                            : await SUT.CountAsync<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.That(count, Is.EqualTo(1), GetTestName());
        }

        [Test]
        public void AddMany()
        {
            // Arrange
            var documents = new List<T> { new T(), new T() };
            // Act
            SUT.AddMany<T, TKey>(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T, TKey>(e => e.Id.Equals(documents[0].Id)
                                                                                   || e.Id.Equals(documents[1].Id))
                                                            : SUT.Count<T, TKey>(e => e.Id.Equals(documents[0].Id)
                                                                                  || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.That(count, Is.EqualTo(2), GetTestName());
        }

        [Test]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<T> { new T(), new T() };
            // Act
            await SUT.AddManyAsync<T, TKey>(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? await SUT.CountAsync<T, TKey>(e => e.Id.Equals(documents[0].Id)
                    || e.Id.Equals(documents[1].Id))
                                                            : await SUT.CountAsync<T, TKey>(e => e.Id.Equals(documents[0].Id)
                                                                || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.That(count, Is.EqualTo(2), GetTestName());
        }


        #endregion Add

        #region Read

        [Test]
        public async Task GetByIdAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            // Act
            var result = await SUT.GetByIdAsync<T, TKey>(document.Id, PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
        }

        [Test]
        public void GetById()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
        }

        [Test]
        public async Task GetOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            // Act
            var result = await SUT.GetOneAsync<T, TKey>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
        }

        [Test]
        public void GetOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = SUT.GetOne<T, TKey>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
        }

        [Test]
        public void GetCursor()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var cursor = SUT.GetCursor<T, TKey>(x => x.Id.Equals(document.Id), PartitionKey);
            var count = cursor.CountDocuments();
            // Assert
            Assert.That(count, Is.EqualTo(1), GetTestName());
        }

        [Test]
        public async Task AnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            // Act
            var result = await SUT.AnyAsync<T, TKey>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(true), GetTestName());
        }

        [Test]
        public async Task AnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            // Act
            var result = await SUT.AnyAsync<T, TKey>(x => x.Id.Equals(document.Init<TKey>()), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(false), GetTestName());
        }

        [Test]
        public void AnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = SUT.Any<T, TKey>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(true), GetTestName());
        }

        [Test]
        public void AnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = SUT.Any<T, TKey>(x => x.Id.Equals(document.Init<TKey>()), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(false), GetTestName());
        }

        [Test]
        public async Task GetAllAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            await SUT.AddManyAsync<T, TKey>(documents);
            // Act
            var result = await SUT.GetAllAsync<T, TKey>(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.That(result.Count, Is.EqualTo(5), GetTestName());
        }

        [Test]
        public void GetAll()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<T, TKey>(documents);
            // Act
            var result = SUT.GetAll<T, TKey>(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.That(result.Count, Is.EqualTo(5), GetTestName());
        }

        [Test]
        public async Task CountAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            await SUT.AddManyAsync<T, TKey>(documents);
            // Act
            var result = await SUT.CountAsync<T, TKey>(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(5), GetTestName());
        }

        [Test]
        public void Count()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.AddMany<T, TKey>(documents);
            // Act
            var result = SUT.Count<T, TKey>(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(5), GetTestName());
        }

        #endregion Read

        #region Update

        [Test]
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
            Assert.That(result);
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public async Task UpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            var content = GetContent();
            document.SomeContent = content;
            // Act
            var result = await SUT.UpdateOneAsync<T, TKey>(document);
            // Assert
            Assert.That(result);
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public void UpdateOneField()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, TKey, string>(document, x => x.SomeContent, content);
            // Assert
            Assert.That(result, GetTestName());
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public async Task UpdateOneFieldAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, TKey, string>(document, x => x.SomeContent, content);
            // Assert
            Assert.That(result, GetTestName());
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public void UpdateOneFieldWithFilter()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, TKey, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.That(result, GetTestName());
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public async Task UpdateOneFieldWithFilterAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, TKey, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.That(result, GetTestName());
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public async Task UpdateOneAsyncWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = MongoDB.Driver.Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = await SUT.UpdateOneAsync<T, TKey>(document, updateDef);
            // Assert
            Assert.That(result);
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null);
            Assert.That(updatedDocument.Children[0].Type, Is.EqualTo(childrenToAdd[0].Type), GetTestName());
            Assert.That(updatedDocument.Children[0].Value, Is.EqualTo(childrenToAdd[0].Value), GetTestName());
            Assert.That(updatedDocument.Children[1].Type, Is.EqualTo(childrenToAdd[1].Type), GetTestName());
            Assert.That(updatedDocument.Children[1].Value, Is.EqualTo(childrenToAdd[1].Value), GetTestName());
        }

        [Test]
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
            Assert.That(result);
            var updatedDocument = SUT.GetById<T, TKey>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null);
            Assert.That(updatedDocument.Children[0].Type, Is.EqualTo(childrenToAdd[0].Type), GetTestName());
            Assert.That(updatedDocument.Children[0].Value, Is.EqualTo(childrenToAdd[0].Value), GetTestName());
            Assert.That(updatedDocument.Children[1].Type, Is.EqualTo(childrenToAdd[1].Type), GetTestName());
            Assert.That(updatedDocument.Children[1].Value, Is.EqualTo(childrenToAdd[1].Value), GetTestName());
        }

        #endregion Update

        #region Delete

        [Test]
        public void DeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = SUT.DeleteOne<T, TKey>(document);
            // Assert
            Assert.That(result, Is.EqualTo(1));
            Assert.That(SUT.Any<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public void DeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = SUT.DeleteOne<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(1));
            Assert.That(SUT.Any<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public async Task DeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            // Act
            var result = await SUT.DeleteOneAsync<T, TKey>(document);
            // Assert
            Assert.That(result, Is.EqualTo(1));
            await Assert.ThatAsync(() => SUT.AnyAsync<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public async Task DeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync<T, TKey>(document);
            // Act
            var result = await SUT.DeleteOneAsync<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(1));
            await Assert.ThatAsync(() => SUT.AnyAsync<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public async Task DeleteManyAsyncLinq()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            await SUT.AddManyAsync<T, TKey>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<T, TKey>(e => e.SomeContent == criteria, PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(5));
            await Assert.ThatAsync(() => SUT.AnyAsync<T, TKey>(e => e.SomeContent == criteria, PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public async Task DeleteManyAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            await SUT.AddManyAsync<T, TKey>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<T, TKey>(documents);
            // Assert
            Assert.That(result, Is.EqualTo(5));
            await Assert.ThatAsync(() => SUT.AnyAsync<T, TKey>(e => e.SomeContent == criteria, PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public void DeleteManyLinq()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany<T, TKey>(documents);
            // Act
            var result = SUT.DeleteMany<T, TKey>(e => e.SomeContent == criteria, PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(5));
            Assert.That(SUT.Any<T, TKey>(e => e.SomeContent == criteria, PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public void DeleteMany()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany<T, TKey>(documents);
            // Act
            var result = SUT.DeleteMany<T, TKey>(documents);
            // Assert
            Assert.That(result, Is.EqualTo(5));
            Assert.That(SUT.Any<T, TKey>(e => e.SomeContent == criteria, PartitionKey), Is.False, GetTestName());
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
            await SUT.AddOneAsync<T, TKey>(document);
            // Act
            var result = await SUT.ProjectOneAsync<T, MyTestProjection, TKey>(
                x => x.Id.Equals(document.Id),
                x => new MyTestProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
            Assert.That(result.SomeContent, Is.EqualTo(someContent), GetTestName());
            Assert.That(result.SomeDate.Minute, Is.EqualTo(someDate.Minute), GetTestName());
            Assert.That(result.SomeDate.Second, Is.EqualTo(someDate.Second), GetTestName());
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
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = SUT.ProjectOne<T, MyTestProjection, TKey>(
                x => x.Id.Equals(document.Id),
                x => new MyTestProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
            Assert.That(result.SomeContent, Is.EqualTo(someContent), GetTestName());
            Assert.That(result.SomeDate.Minute, Is.EqualTo(someDate.Minute), GetTestName());
            Assert.That(result.SomeDate.Second, Is.EqualTo(someDate.Second), GetTestName());
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

            await SUT.AddManyAsync<T, TKey>(documents);
            // Act
            var result = await SUT.ProjectManyAsync<T, MyTestProjection, TKey>(
                x => x.SomeContent == someContent,
                x => new MyTestProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.That(result.Count, Is.EqualTo(5), GetTestName());
            Assert.That(result.First().SomeContent, Is.EqualTo(someContent), GetTestName());
            Assert.That(result.First().SomeDate.Minute, Is.EqualTo(someDate.Minute), GetTestName());
            Assert.That(result.First().SomeDate.Second, Is.EqualTo(someDate.Second), GetTestName());
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

            SUT.AddMany<T, TKey>(documents);
            // Act
            var result = SUT.ProjectMany<T, MyTestProjection, TKey>(
                x => x.SomeContent == someContent,
                x => new MyTestProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.That(result.Count, Is.EqualTo(5), GetTestName());
            Assert.That(result.First().SomeContent, Is.EqualTo(someContent), GetTestName());
            Assert.That(result.First().SomeDate.Minute, Is.EqualTo(someDate.Minute), GetTestName());
            Assert.That(result.First().SomeDate.Second, Is.EqualTo(someDate.Second), GetTestName());
        }

        #endregion Project

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
