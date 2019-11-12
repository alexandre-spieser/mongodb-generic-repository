using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using Xunit;

namespace CoreIntegrationTests.Infrastructure
{
    public abstract class DefaultBaseTKeyMongodbRepositoryTestBase<T, TKey> :
        IClassFixture<MongoDbTestFixture<T, TKey>>
        where T : TestDoc<TKey>, new()
        where TKey : IEquatable<TKey>

    {
        protected DefaultBaseTKeyMongodbRepositoryTestBase(MongoDbTestFixture<T, TKey> fixture)
        {
            _fixture = fixture;
            var type = CreateTestDocument();
            DocumentTypeName = type.GetType().FullName;
            if (type is IPartitionedDocument) PartitionKey = ((IPartitionedDocument) type).PartitionKey;
            _fixture.PartitionKey = PartitionKey;
            TestClassName = GetClassName();
            MongoDbConfig.EnsureConfigured();
            SUT = TestDefaultBaseTKeyMongodbRepository<T, TKey>.Instance;
        }

        public abstract string GetClassName();

        private readonly MongoDbTestFixture<T, TKey> _fixture;


        public T CreateTestDocument()
        {
            return _fixture.CreateTestDocument();
        }

        public List<T> CreateTestDocuments(int numberOfDocumentsToCreate)
        {
            return _fixture.CreateTestDocuments(numberOfDocumentsToCreate);
        }

        /// <summary>
        ///     The partition key for the collection, if any
        /// </summary>
        protected string PartitionKey { get; set; }

        /// <summary>
        ///     the name of the test class
        /// </summary>
        protected string TestClassName { get; set; }

        /// <summary>
        ///     The name of the document used for tests
        /// </summary>
        protected string DocumentTypeName { get; set; }


        /// <summary>
        ///     SUT: System Under Test
        /// </summary>
        protected static TestDefaultBaseTKeyMongodbRepository<T, TKey> SUT { get; set; }

        public static Random Random = new Random();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetParentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(2);
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
            // SUT.DropTestCollection(PartitionKey);
        }

        [Fact]
        public async Task AnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var result = await SUT.AnyAsync(x => x.Id.Equals(Guid.NewGuid()), PartitionKey);
            // Assert
            Assert.False(result, GetTestName());
        }

        [Fact]
        public async Task AnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var result = await SUT.AnyAsync(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True(result, GetTestName());
        }

        [Fact]
        public void AnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var result = SUT.Any(x => x.Id.Equals(Guid.NewGuid()), PartitionKey);
            // Assert
            Assert.False(result, GetTestName());
        }

        [Fact]
        public void AnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var result = SUT.Any(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True(result, GetTestName());
        }

        [Fact]
        public async Task CountDocumentsAsyncTest()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.InsertMany(documents);
            // Act
            var result = await SUT.CountDocumentsAsync(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.True(5 == result, GetTestName());
        }

        [Fact]
        public void CountDocumentsTest()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.InsertMany(documents);
            // Act
            var result = SUT.CountDocuments(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.True(5 == result, GetTestName());
        }

        [Fact]
        public async Task FirstOrDefaultAsyncTest()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var result = await SUT.FirstOrDefaultAsync(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True(null != result, GetTestName());
        }

        [Fact]
        public void FirstOrDefaultTest()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var result = SUT.FirstOrDefault(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.True(null != result, GetTestName());
        }

        [Fact]
        public async Task GetByIdAsyncTest()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var result = await SUT.GetByIdAsync(document.Id, PartitionKey);
            // Assert
            Assert.True(null != result, GetTestName());
        }

        [Fact]
        public void GetByIdTest()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var result = SUT.GetById(document.Id, PartitionKey);
            // Assert
            Assert.True(null != result, GetTestName());
        }

        [Fact]
        public void GetCursorTest()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.InsertOne(document);
            // Act
            var cursor = SUT.GetCursor<T>(x => x.Id.Equals(document.Id), PartitionKey);
            var count = cursor.CountDocuments();
            // Assert
            Assert.True(1 == count, GetTestName());
        }

        [Fact]
        public async Task InsertManyAsyncTest()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            // Act
            await SUT.InsertManyAsync(documents);
            // Assert
            var count = string.IsNullOrEmpty(PartitionKey)
                ? SUT.CountDocuments(e => e.Id.Equals(documents[0].Id)
                                          || e.Id.Equals(documents[1].Id))
                : SUT.CountDocuments(e => e.Id.Equals(documents[0].Id)
                                          || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.True(2 == count, GetTestName());
        }

        [Fact]
        public async Task InsertManyAsyncWithDifferentPartitionKey()
        {
            // only run this test for tests on documents with partition key
            if (!string.IsNullOrEmpty(PartitionKey))
            {
                // Arrange
                var documents = CreateTestDocuments(4);
                if (documents.Any(e => e is IPartitionedDocument))
                {
                    var secondPartitionKey = $"{PartitionKey}-2";
                    ((IPartitionedDocument) documents[2]).PartitionKey = secondPartitionKey;
                    ((IPartitionedDocument) documents[3]).PartitionKey = secondPartitionKey;
                    // Act
                    await SUT.InsertManyAsync(documents);
                    // Assert
                    var count = SUT.CountDocuments(e => e.Id.Equals(documents[0].Id) || e.Id.Equals(documents[1].Id),
                        PartitionKey);
                    var secondPartitionCount =
                        SUT.CountDocuments(e => e.Id.Equals(documents[2].Id) || e.Id.Equals(documents[3].Id),
                            secondPartitionKey);
                    // Cleanup second partition
                    SUT.DeleteMany<T>(e => e.Id.Equals(documents[2].Id) || e.Id.Equals(documents[3].Id),
                        secondPartitionKey);
                    Assert.True(2 == count, GetTestName());
                    Assert.True(2 == secondPartitionCount, GetTestName());
                }
            }
        }

        [Fact]
        public void InsertManyTest()
        {
            // Arrange
            var documents = CreateTestDocuments(2);
            // Act
            SUT.InsertMany(documents);
            // Assert
            var count = string.IsNullOrEmpty(PartitionKey)
                ? SUT.CountDocuments(e => e.Id.Equals(documents[0].Id)
                                          || e.Id.Equals(documents[1].Id))
                : SUT.CountDocuments(e => e.Id.Equals(documents[0].Id)
                                          || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.True(2 == count, GetTestName());
        }

        [Fact]
        public void InsertManyWithDifferentPartitionKey()
        {
            // only run this test for tests on documents with partition key
            if (!string.IsNullOrEmpty(PartitionKey))
            {
                // Arrange
                var documents = CreateTestDocuments(4);
                if (documents.Any(e => e is IPartitionedDocument))
                {
                    var secondPartitionKey = $"{PartitionKey}-2";
                    ((IPartitionedDocument) documents[2]).PartitionKey = secondPartitionKey;
                    ((IPartitionedDocument) documents[3]).PartitionKey = secondPartitionKey;
                    // Act
                    SUT.InsertMany(documents);
                    // Assert
                    var count = SUT.CountDocuments(e => e.Id.Equals(documents[0].Id) || e.Id.Equals(documents[1].Id),
                        PartitionKey);
                    var secondPartitionCount =
                        SUT.CountDocuments(e => e.Id.Equals(documents[2].Id) || e.Id.Equals(documents[3].Id),
                            secondPartitionKey);
                    // Cleanup second partition
                    SUT.DeleteMany<T>(e => e.Id.Equals(documents[2].Id) || e.Id.Equals(documents[3].Id),
                        secondPartitionKey);
                    Assert.True(2 == count, GetTestName());
                    Assert.True(2 == secondPartitionCount, GetTestName());
                }
            }
        }

        [Fact]
        public async Task InsertOneAsyncTest()
        {
            // Arrange
            var document = CreateTestDocument();
            // Act
            await SUT.InsertOneAsync(document);
            // Assert
            var count = string.IsNullOrEmpty(PartitionKey)
                ? SUT.CountDocuments(e => e.Id.Equals(document.Id))
                : SUT.CountDocuments(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.True(1 == count, GetTestName());
        }

        [Fact]
        public void InsertOneTest()
        {
            // Arrange
            var document = CreateTestDocument();
            // Act
            SUT.InsertOne(document);
            // Assert
            var count = string.IsNullOrEmpty(PartitionKey)
                ? SUT.CountDocuments(e => e.Id.Equals(document.Id))
                : SUT.CountDocuments(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.True(1 == count, GetTestName());
        }

        [Fact]
        public async Task QueryListAsyncTest()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.InsertMany(documents);
            // Act
            var result = await SUT.QueryListAsync(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.True(5 == result.Count, GetTestName());
        }

        [Fact]
        public void QueryListTest()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            var content = GetContent();
            documents.ForEach(e => e.SomeContent = content);
            SUT.InsertMany(documents);
            // Act
            var result = SUT.QueryList(x => x.SomeContent == content, PartitionKey);
            // Assert
            Assert.True(5 == result.Count, GetTestName());
        }
    }
}