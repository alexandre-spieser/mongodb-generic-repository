using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace CoreIntegrationTests.Infrastructure
{
    public abstract class MongoDbDocumentTestBase<T> :
        IClassFixture<MongoDbTestFixture<T, Guid>>
        where T: TestDoc, new()
    {

        private readonly MongoDbTestFixture<T, Guid> _fixture;

        protected MongoDbDocumentTestBase(MongoDbTestFixture<T, Guid> fixture)
        {
            _fixture = fixture;
            var type = CreateTestDocument();
            DocumentTypeName = type.GetType().FullName;
            if (type is IPartitionedDocument)
            {
                PartitionKey = ((IPartitionedDocument)type).PartitionKey;
            }
            _fixture.PartitionKey = PartitionKey;
            TestClassName = GetClassName();
            MongoDbConfig.EnsureConfigured();
            SUT = TestRepository.Instance;
        }

        protected T CreateTestDocument()
        {
            return _fixture.CreateTestDocument();
        }

        public abstract string GetClassName();

        protected List<T> CreateTestDocuments(int numberOfDocumentsToCreate)
        {
            return _fixture.CreateTestDocuments(numberOfDocumentsToCreate);
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

        #region Add

        [Fact]
        public void AddOne()
        {
            // Arrange
            var document = new T();
            // Act
            SUT.AddOne<T>(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(document.Id)) 
                                                            : SUT.Count<T>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.True(1 == count, GetTestName());
        }

        [Fact]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new T();
            // Act
            await SUT.AddOneAsync<T>(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(document.Id))
                                                            : SUT.Count<T>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.True (1 == count, GetTestName());
        }

        [Fact]
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
            Assert.True (2 == count, GetTestName());
        }

        [Fact]
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
            Assert.True (2 == count, GetTestName());
        }


        #endregion Add

        #region Read

        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.GetByIdAsync<T>(document.Id, PartitionKey);
            // Assert
            Assert.True(null != result, GetTestName());
        }

        [Fact]
        public void GetById()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.GetById<T>(document.Id, PartitionKey);
            // Assert
            Assert.True(null != result, GetTestName());
        }

        [Fact]
        public async Task GetOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.GetOneAsync<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True(null != result, GetTestName());
        }

        [Fact]
        public void GetOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.GetOne<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True(null != result, GetTestName());
        }

        [Fact]
        public void GetCursor()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var cursor = SUT.GetCursor<T>(x => x.Id.Equals(document.Id), PartitionKey);
            var count = cursor.Count();
            // Assert
            Assert.True (1 == count, GetTestName());
        }

        [Fact]
        public async Task AnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.AnyAsync<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True(result, GetTestName());
        }

        [Fact]
        public async Task AnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.AnyAsync<T>(x => x.Id.Equals(Guid.NewGuid()), PartitionKey);
            // Assert
            Assert.False(result, GetTestName());
        }

        [Fact]
        public void AnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.Any<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True(result, GetTestName());
        }

        [Fact]
        public void AnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.Any<T>(x => x.Id.Equals(Guid.NewGuid()), PartitionKey);
            // Assert
            Assert.False(result, GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result.Count, GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result.Count, GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result, GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result, GetTestName());
        }

        #endregion Read

        #region Update

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
            Assert.True(childrenToAdd[0].Type== updatedDocument.Children[0].Type, GetTestName());
            Assert.True(childrenToAdd[0].Value== updatedDocument.Children[0].Value, GetTestName());
            Assert.True(childrenToAdd[1].Type== updatedDocument.Children[1].Type, GetTestName());
            Assert.True(childrenToAdd[1].Value== updatedDocument.Children[1].Value, GetTestName());
        }

        #endregion Update

        #region Delete

        [Fact]
        public void DeleteOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.DeleteOne<T>(document);
            // Assert
            Assert.True (1 == result);
            Assert.False(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Fact]
        public void DeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = SUT.DeleteOne<T>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True (1 == result);
            Assert.False(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Fact]
        public async Task DeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.DeleteOneAsync<T>(document);
            // Assert
            Assert.True (1 == result);
            Assert.False(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Fact]
        public async Task DeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T>(document);
            // Act
            var result = await SUT.DeleteOneAsync<T>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True (1 == result);
            Assert.False(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result);
            Assert.False(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result);
            Assert.False(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result);
            Assert.False(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result);
            Assert.False(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        #endregion Delete

        #region Project

        [Fact]
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
            Assert.True(null != result, GetTestName());
            Assert.True(someContent == result.SomeContent, GetTestName());
            Assert.True(someDate.Minute == result.SomeDate.Minute, GetTestName());
            Assert.True(someDate.Second == result.SomeDate.Second, GetTestName());
        }

        [Fact]
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
            Assert.True(null != result, GetTestName());
            Assert.True(someContent == result.SomeContent, GetTestName());
            Assert.True(someDate.Minute == result.SomeDate.Minute, GetTestName());
            Assert.True(someDate.Second == result.SomeDate.Second, GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result.Count, GetTestName());
            Assert.True(someContent == result.First().SomeContent, GetTestName());
            Assert.True(someDate.Minute == result.First().SomeDate.Minute, GetTestName());
            Assert.True(someDate.Second == result.First().SomeDate.Second, GetTestName());
        }

        [Fact]
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
            Assert.True(5 == result.Count, GetTestName());
            Assert.True(someContent == result.First().SomeContent, GetTestName());
            Assert.True(someDate.Minute == result.First().SomeDate.Minute, GetTestName());
            Assert.True(someDate.Second == result.First().SomeDate.Second, GetTestName());
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

        private void Cleanup()
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

        #endregion Test Utils

    }
}
