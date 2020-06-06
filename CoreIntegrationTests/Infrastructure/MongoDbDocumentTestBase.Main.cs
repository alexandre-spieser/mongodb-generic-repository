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
            SUT = TestTKeyRepository<Guid>.Instance;
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
        protected static ITestRepository<Guid> SUT { get; set; }

        #region Add

        [Fact]
        public void AddOne()
        {
            // Arrange
            var document = CreateTestDocument();
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
            var document = CreateTestDocument();
            // Act
            await SUT.AddOneAsync<T>(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(document.Id))
                                                            : SUT.Count<T>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.True(1 == count, GetTestName());
        }

        [Fact]
        public void AddMany()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            // Act
            SUT.AddMany<T>(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                   || e.Id.Equals(documents[1].Id))
                                                            : SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                  || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.True(2 == count, GetTestName());
        }

        [Fact]
        public void AddManyWithDifferentPartitionKey()
        {
            // only run this test for tests on documents with partition key
            if (!string.IsNullOrEmpty(PartitionKey))
            {
                // Arrange
                var documents = CreateTestDocuments(4);
                if (documents.Any(e => e is IPartitionedDocument))
                {
                    var secondPartitionKey = $"{PartitionKey}-2";
                    ((IPartitionedDocument)documents[2]).PartitionKey = secondPartitionKey;
                    ((IPartitionedDocument)documents[3]).PartitionKey = secondPartitionKey;
                    // Act
                    SUT.AddMany<T>(documents);
                    // Assert
                    long count = SUT.Count<T>(e => e.Id.Equals(documents[0].Id) || e.Id.Equals(documents[1].Id), PartitionKey);
                    long secondPartitionCount = SUT.Count<T>(e => e.Id.Equals(documents[2].Id) || e.Id.Equals(documents[3].Id), secondPartitionKey);
                    // Cleanup second partition
                    SUT.DeleteMany<T>(e => e.Id.Equals(documents[2].Id) || e.Id.Equals(documents[3].Id), secondPartitionKey);
                    Assert.True(2 == count, GetTestName());
                    Assert.True(2 == secondPartitionCount, GetTestName());
                }
            }
        }

        [Fact]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            // Act
            await SUT.AddManyAsync<T>(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                   || e.Id.Equals(documents[1].Id))
                                                            : SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                  || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.True(2 == count, GetTestName());
        }

        [Fact]
        public async Task AddManyAsyncWithDifferentPartitionKey()
        {
            // only run this test for tests on documents with partition key
            if (!string.IsNullOrEmpty(PartitionKey))
            {
                // Arrange
                var documents = CreateTestDocuments(4);
                if (documents.Any(e => e is IPartitionedDocument))
                {
                    var secondPartitionKey = $"{PartitionKey}-2";
                    ((IPartitionedDocument)documents[2]).PartitionKey = secondPartitionKey;
                    ((IPartitionedDocument)documents[3]).PartitionKey = secondPartitionKey;
                    // Act
                    await SUT.AddManyAsync<T>(documents);
                    // Assert
                    long count = SUT.Count<T>(e => e.Id.Equals(documents[0].Id) || e.Id.Equals(documents[1].Id), PartitionKey);
                    long secondPartitionCount = SUT.Count<T>(e => e.Id.Equals(documents[2].Id) || e.Id.Equals(documents[3].Id), secondPartitionKey);
                    // Cleanup second partition
                    SUT.DeleteMany<T>(e => e.Id.Equals(documents[2].Id) || e.Id.Equals(documents[3].Id), secondPartitionKey);
                    Assert.True(2 == count, GetTestName());
                    Assert.True(2 == secondPartitionCount, GetTestName());
                }
            }
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
            var count = cursor.CountDocuments();
            // Assert
            Assert.True(1 == count, GetTestName());
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
            Assert.True(1 == result);
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
            Assert.True(1 == result);
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
            Assert.True(1 == result);
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
            Assert.True(1 == result);
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
            var canPartition = !string.IsNullOrEmpty(PartitionKey) && documents.Any(e => e is IPartitionedDocument);
            string secondKey = null;
            if (canPartition)
            {
                secondKey = $"{PartitionKey}-2";
                ((IPartitionedDocument)documents[3]).PartitionKey = secondKey;
                ((IPartitionedDocument)documents[4]).PartitionKey = secondKey;
            }

            SUT.AddMany<T>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<T>(documents);
            // Assert
            Assert.True(5 == result);
            Assert.False(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
            if (canPartition)
            {
                Assert.False(SUT.Any<T>(e => e.SomeContent == criteria, secondKey), GetTestName());
            }
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
            var canPartition = !string.IsNullOrEmpty(PartitionKey) && documents.Any(e => e is IPartitionedDocument);
            string secondKey = null;
            if (canPartition)
            {
                secondKey = $"{PartitionKey}-2";
                ((IPartitionedDocument)documents[3]).PartitionKey = secondKey;
                ((IPartitionedDocument)documents[4]).PartitionKey = secondKey;
            }

            SUT.AddMany<T>(documents);
            // Act
            var result = SUT.DeleteMany<T>(documents);
            // Assert
            Assert.True(5 == result);
            Assert.False(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
            if (canPartition)
            {
                Assert.False(SUT.Any<T>(e => e.SomeContent == criteria, secondKey), GetTestName());
            }
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

        #region Max / Min Queries

        [Fact]
        public async Task GetByMaxAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedMax = documents.OrderByDescending(e => e.Nested.SomeDate).First();

            // Act
            var result = await SUT.GetByMaxAsync<T>(e => e.SomeContent == criteria, e => e.Nested.SomeDate, PartitionKey);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedMax.Id, result.Id);
        }

        [Fact]
        public void GetByMax()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedMax = documents.OrderByDescending(e => e.Nested.SomeDate).First();

            // Act
            var result = SUT.GetByMax<T>(e => e.SomeContent == criteria, e => e.Nested.SomeDate, PartitionKey);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedMax.Id, result.Id);
        }

        [Fact]
        public async Task GetByMinAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedMin = documents.OrderBy(e => e.Nested.SomeDate).First();

            // Act
            var result = await SUT.GetByMinAsync<T>(e => e.SomeContent == criteria, e => e.Nested.SomeDate, PartitionKey);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedMin.Id, result.Id);
        }

        [Fact]
        public void GetByMin()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedMin = documents.OrderBy(e => e.Nested.SomeDate).First();

            // Act
            var result = SUT.GetByMin<T>(e => e.SomeContent == criteria, e => e.Nested.SomeDate, PartitionKey);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedMin.Id, result.Id);
        }

        [Fact]
        public void GetMinValue()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedMin = documents.OrderBy(e => e.Nested.SomeDate).First();

            // Act
            var result = SUT.GetMinValue<T, DateTime>(e => e.SomeContent == criteria, e => e.Nested.SomeDate, PartitionKey);

            // Assert
            Assert.True(result != default(DateTime));
            Assert.Equal(expectedMin.Nested.SomeDate.Date, result.Date);
        }

        [Fact]
        public async Task GetMinValueAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedMin = documents.OrderBy(e => e.Nested.SomeDate).First();

            // Act
            var result = await SUT.GetMinValueAsync<T, DateTime>(e => e.SomeContent == criteria, e => e.Nested.SomeDate, PartitionKey);

            // Assert
            Assert.True(result != default(DateTime));
            Assert.Equal(expectedMin.Nested.SomeDate.Date, result.Date);
        }

        [Fact]
        public void GetMaxValue()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedMax = documents.OrderByDescending(e => e.Nested.SomeDate).First();

            // Act
            var result = SUT.GetMaxValue<T, DateTime>(e => e.SomeContent == criteria, e => e.Nested.SomeDate, PartitionKey);

            // Assert
            Assert.True(result != default(DateTime));
            Assert.Equal(expectedMax.Nested.SomeDate.Date, result.Date);
        }

        [Fact]
        public async Task GetMaxValueAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedMin = documents.OrderByDescending(e => e.Nested.SomeDate).First();

            // Act
            var result = await SUT.GetMaxValueAsync<T, DateTime>(e => e.SomeContent == criteria, e => e.Nested.SomeDate, PartitionKey);

            // Assert
            Assert.True(result != default(DateTime));
            Assert.Equal(expectedMin.Nested.SomeDate.Date, result.Date);
        }

        #endregion Max / Min Queries

        #region Index Management

        static SemaphoreSlim textIndexSemaphore = new SemaphoreSlim(1, 1);

        [Fact]
        public async Task CreateTextIndexNoOptionAsync()
        {
            // Arrange 
            const string expectedIndexName = "SomeContent_text";

            // Act
            await textIndexSemaphore.WaitAsync();
            try
            {
                var result = await SUT.CreateTextIndexAsync<T>(x => x.SomeContent, null, PartitionKey);

                // Assert
                var listOfIndexNames = await SUT.GetIndexesNamesAsync<T>(PartitionKey);
                Assert.Contains(expectedIndexName, listOfIndexNames);

                // Cleanup 
                await SUT.DropIndexAsync<T>(expectedIndexName, PartitionKey);
            }
            finally
            {
                textIndexSemaphore.Release();
            }
        }

        [Fact]
        public async Task CreateTextIndexWithOptionAsync()
        {
            // Arrange
            string expectedIndexName = $"{Guid.NewGuid()}";
            var option = new IndexCreationOptions
            {
                Name = expectedIndexName
            };
            await textIndexSemaphore.WaitAsync();
            try
            {
                // Act
                var result = await SUT.CreateTextIndexAsync<T>(x => x.AddedAtUtc, option, PartitionKey);

                // Assert
                var listOfIndexNames = await SUT.GetIndexesNamesAsync<T>(PartitionKey);
                Assert.Contains(expectedIndexName, listOfIndexNames);

                // Cleanup
                await SUT.DropIndexAsync<T>(expectedIndexName, PartitionKey);
            }
            finally
            {
                textIndexSemaphore.Release();
            }
        }

        [Fact]
        public async Task CreateAscendingIndexAsync()
        {
            // Arrange 
            const string expectedIndexName = "SomeContent_1";

            // Act
            var result = await SUT.CreateAscendingIndexAsync<T>(x => x.SomeContent, null, PartitionKey);

            // Assert
            var listOfIndexNames = await SUT.GetIndexesNamesAsync<T>(PartitionKey);
            Assert.Contains(expectedIndexName, listOfIndexNames);

            // Cleanup 
            await SUT.DropIndexAsync<T>(expectedIndexName, PartitionKey);
        }

        [Fact]
        public async Task CreateDescendingIndexAsync()
        {
            // Arrange 
            const string expectedIndexName = "SomeContent_-1";

            // Act
            var result = await SUT.CreateDescendingIndexAsync<T>(x => x.SomeContent, null, PartitionKey);

            // Assert
            var listOfIndexNames = await SUT.GetIndexesNamesAsync<T>(PartitionKey);
            Assert.Contains(expectedIndexName, listOfIndexNames);

            // Cleanup 
            await SUT.DropIndexAsync<T>(expectedIndexName, PartitionKey);
        }

        [Fact]
        public async Task CreateHashedIndexAsync()
        {
            // Arrange 
            const string expectedIndexName = "SomeContent_hashed";

            // Act
            var result = await SUT.CreateHashedIndexAsync<T>(x => x.SomeContent, null, PartitionKey);

            // Assert
            var listOfIndexNames = await SUT.GetIndexesNamesAsync<T>(PartitionKey);
            Assert.Contains(expectedIndexName, listOfIndexNames);

            // Cleanup 
            await SUT.DropIndexAsync<T>(expectedIndexName, PartitionKey);
        }

        [Fact]
        public async Task CreateCombinedTextIndexAsync()
        {
            // Arrange 
            const string expectedIndexName = "SomeContent2_text_SomeContent3_text";

            // Act
            Expression <Func<T, object>> ex = x => x.SomeContent2;
            Expression <Func<T, object>> ex2 = x => x.SomeContent3;
            var result = await SUT.CreateCombinedTextIndexAsync<T>(new[] { ex, ex2 }, null, PartitionKey);

            // Assert
            var listOfIndexNames = await SUT.GetIndexesNamesAsync<T>(PartitionKey);
            Assert.Contains(expectedIndexName, listOfIndexNames);

            // Cleanup 
            await SUT.DropIndexAsync<T>(expectedIndexName, PartitionKey);
        }

        #endregion Index Management

        #region Math

        [Fact]
        public async Task SumByDecimalAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.Nested.SomeAmount = 5m;
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedSum = documents.Sum(e => e.Nested.SomeAmount);

            // Act
            var result = await SUT.SumByAsync<T>(e => e.SomeContent == criteria, e => e.Nested.SomeAmount, PartitionKey);

            // Assert
            Assert.Equal(expectedSum, result);
        }

        [Fact]
        public void SumByDecimal()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}.{Guid.NewGuid()}";
            var documents = CreateTestDocuments(5);
            var i = 1;
            documents.ForEach(e =>
            {
                e.Nested.SomeDate = e.Nested.SomeDate.AddDays(i++);
                e.Nested.SomeAmount = 5m;
                e.SomeContent = criteria;
            });
            SUT.AddMany<T>(documents);
            var expectedSum = documents.Sum(e => e.Nested.SomeAmount);

            // Act
            var result = SUT.SumBy<T>(e => e.SomeContent == criteria, e => e.Nested.SomeAmount, PartitionKey);

            // Assert
            Assert.Equal(expectedSum, result);
        }

        #endregion Math

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
