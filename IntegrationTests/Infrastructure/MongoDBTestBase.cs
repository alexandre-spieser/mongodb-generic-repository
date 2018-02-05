using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IntegrationTests.Infrastructure
{
    public class TestDoc<TKey> : IDocument<TKey>
                where TKey : IEquatable<TKey>
    {
        [BsonId]
        public TKey Id { get; set; }
        public int Version { get; set; }

        public TestDoc()
        {
            InitializeFields();
            Version = 2;
            Nested = new Nested
            {
                SomeDate = DateTime.UtcNow
            };
        }

        public string SomeContent { get; set; }

        public Nested Nested { get; set; }

        private void InitializeFields()
        {
            var idTypeName = typeof(TKey).Name;
            switch (idTypeName)
            {
                case "Guid":
                    Id = (TKey)(object)Guid.NewGuid();
                    break;
                case "Int16":
                    Id = (TKey)(object)GlobalVariables.Random.Next(1, short.MaxValue);
                    break;
                case "Int32":
                    Id = (TKey)(object)GlobalVariables.Random.Next(1, int.MaxValue);
                    break;
                case "Int64":
                    Id = (TKey)(object)(GlobalVariables.Random.NextLong(1, long.MaxValue));
                    break;
                case "String":
                    Id = (TKey)(object)Guid.NewGuid().ToString();
                    break;
            }

        }
    }


    [TestFixture]
    public abstract class MongoDBTestBase<T, TKey> 
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

        public MongoDBTestBase()
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
            Assert.AreEqual(1, count, GetTestName());
        }

        [Test]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new T();
            // Act
            await SUT.AddOneAsync<T, TKey>(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T, TKey>(e => e.Id.Equals(document.Id))
                                                            : SUT.Count<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.AreEqual(1, count, GetTestName());
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
            Assert.AreEqual(2, count, GetTestName());
        }

        [Test]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<T> { new T(), new T() };
            // Act
            await SUT.AddManyAsync<T, TKey>(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T, TKey>(e => e.Id.Equals(documents[0].Id)
                                                                                   || e.Id.Equals(documents[1].Id))
                                                            : SUT.Count<T, TKey>(e => e.Id.Equals(documents[0].Id)
                                                                                  || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.AreEqual(2, count, GetTestName());
        }


        #endregion Add

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
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
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
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Test]
        public async Task DeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = await SUT.DeleteOneAsync<T, TKey>(document);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Test]
        public async Task DeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne<T, TKey>(document);
            // Act
            var result = await SUT.DeleteOneAsync<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(SUT.Any<T, TKey>(e => e.Id.Equals(document.Id), PartitionKey), GetTestName());
        }

        [Test]
        public async Task DeleteManyAsyncLinq()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany<T, TKey>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<T, TKey>(e => e.SomeContent == criteria, PartitionKey);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<T, TKey>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        [Test]
        public async Task DeleteManyAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany<T, TKey>(documents);
            // Act
            var result = await SUT.DeleteManyAsync<T, TKey>(documents);
            // Assert
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<T, TKey>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
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
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<T, TKey>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
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
            Assert.AreEqual(5, result);
            Assert.IsFalse(SUT.Any<T, TKey>(e => e.SomeContent == criteria, PartitionKey), GetTestName());
        }

        #endregion Delete

        #region Project



        #endregion Project


        #region Test Utils
        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        private string GetTestName()
        {
            return $"{TestClassName}.{GetCurrentMethod()}";
        }

        #endregion Test Utils
    }
}
