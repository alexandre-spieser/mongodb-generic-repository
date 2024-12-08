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
            SUT.AddOne(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(document.Id)) 
                                                            : SUT.Count<T>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.That(count, Is.EqualTo(1), GetTestName());
        }

        [Test]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new T();
            // Act
            await SUT.AddOneAsync(document);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? await SUT.CountAsync<T>(e => e.Id.Equals(document.Id))
                                                            : await SUT.CountAsync<T>(e => e.Id.Equals(document.Id), PartitionKey);
            Assert.That(count, Is.EqualTo(1), GetTestName());
        }

        [Test]
        public void AddMany()
        {
            // Arrange
            var documents = new List<T> { new T(), new T() };
            // Act
            SUT.AddMany(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                   || e.Id.Equals(documents[1].Id))
                                                            : SUT.Count<T>(e => e.Id.Equals(documents[0].Id)
                                                                                  || e.Id.Equals(documents[1].Id), PartitionKey);
            Assert.That(count, Is.EqualTo(2), GetTestName());
        }

        [Test]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<T> { new T(), new T() };
            // Act
            await SUT.AddManyAsync(documents);
            // Assert
            long count = string.IsNullOrEmpty(PartitionKey) ? await SUT.CountAsync<T>(e => e.Id.Equals(documents[0].Id)
                    || e.Id.Equals(documents[1].Id))
                                                            : await SUT.CountAsync<T>(e => e.Id.Equals(documents[0].Id)
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
            await SUT.AddOneAsync(document);
            // Act
            var result = await SUT.GetByIdAsync<T>(document.Id, PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
        }

        [Test]
        public void GetById()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.GetById<T>(document.Id, PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
        }

        [Test]
        public async Task GetOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            // Act
            var result = await SUT.GetOneAsync<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
        }

        [Test]
        public void GetOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.GetOne<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.Not.Null, GetTestName());
        }

        [Test]
        public void GetCursor()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var cursor = SUT.GetCursor<T>(x => x.Id.Equals(document.Id), PartitionKey);
            var count = cursor.CountDocuments();
            // Assert
            Assert.That(count, Is.EqualTo(1), GetTestName());
        }

        [Test]
        public async Task AnyAsyncReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            // Act
            var result = await SUT.AnyAsync<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(true), GetTestName());
        }

        [Test]
        public async Task AnyAsyncReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            // Act
            var result = await SUT.AnyAsync<T>(x => x.Id.Equals(Guid.NewGuid()), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(false), GetTestName());
        }

        [Test]
        public void AnyReturnsTrue()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.Any<T>(x => x.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(true), GetTestName());
        }

        [Test]
        public void AnyReturnsFalse()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.Any<T>(x => x.Id.Equals(Guid.NewGuid()), PartitionKey);
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
            await SUT.AddManyAsync(documents);
            // Act
            var result = await SUT.GetAllAsync<T>(x => x.SomeContent == content, PartitionKey);
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
            SUT.AddMany(documents);
            // Act
            var result = SUT.GetAll<T>(x => x.SomeContent == content, PartitionKey);
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
            await SUT.AddManyAsync(documents);
            // Act
            var result = await SUT.CountAsync<T>(x => x.SomeContent == content, PartitionKey);
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
            SUT.AddMany(documents);
            // Act
            var result = SUT.Count<T>(x => x.SomeContent == content, PartitionKey);
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
            SUT.AddOne(document);
            var content = GetContent();
            document.SomeContent = content;
            // Act
            var result = SUT.UpdateOne(document);
            // Assert
            Assert.That(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public async Task UpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            var content = GetContent();
            document.SomeContent = content;
            // Act
            var result = await SUT.UpdateOneAsync(document);
            // Assert
            Assert.That(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public void UpdateOneField()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne(document, x => x.SomeContent, content);
            // Assert
            Assert.That(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public async Task UpdateOneFieldAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync(document, x => x.SomeContent, content);
            // Assert
            Assert.That(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public void UpdateOneFieldWithFilter()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            var content = GetContent();
            // Act
            var result = SUT.UpdateOne<T, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.That(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public async Task UpdateOneFieldWithFilterAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            var content = GetContent();
            // Act
            var result = await SUT.UpdateOneAsync<T, string>(x => x.Id.Equals(document.Id), x => x.SomeContent, content, PartitionKey);
            // Assert
            Assert.That(result, GetTestName());
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
            Assert.That(updatedDocument, Is.Not.Null, GetTestName());
            Assert.That(updatedDocument.SomeContent, Is.EqualTo(content), GetTestName());
        }

        [Test]
        public async Task UpdateOneAsyncWithUpdateDefinition()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = MongoDB.Driver.Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = await SUT.UpdateOneAsync(document, updateDef);
            // Assert
            Assert.That(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
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
            SUT.AddOne(document);
            var childrenToAdd = new List<Child>
            {
                new Child("testType1", "testValue1"),
                new Child("testType2", "testValue2")
            };

            var updateDef = MongoDB.Driver.Builders<T>.Update.AddToSetEach(p => p.Children, childrenToAdd);

            // Act
            var result = SUT.UpdateOne(document, updateDef);
            // Assert
            Assert.That(result);
            var updatedDocument = SUT.GetById<T>(document.Id, PartitionKey);
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
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne(document);
            // Assert
            Assert.That(result, Is.EqualTo(1));
            Assert.That(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public void DeleteOneLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            // Act
            var result = SUT.DeleteOne<T>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(1));
            Assert.That(SUT.Any<T>(e => e.Id.Equals(document.Id), PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public async Task DeleteOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            // Act
            var result = await SUT.DeleteOneAsync(document);
            // Assert
            Assert.That(result, Is.EqualTo(1));
            await Assert.ThatAsync(() => SUT.AnyAsync<T>(e => e.Id.Equals(document.Id), PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public async Task DeleteOneAsyncLinq()
        {
            // Arrange
            var document = CreateTestDocument();
            await SUT.AddOneAsync(document);
            // Act
            var result = await SUT.DeleteOneAsync<T>(e => e.Id.Equals(document.Id), PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(1));
            await Assert.ThatAsync(() => SUT.AnyAsync<T>(e => e.Id.Equals(document.Id), PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public async Task DeleteManyAsyncLinq()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            await SUT.AddManyAsync(documents);
            // Act
            var result = await SUT.DeleteManyAsync<T>(e => e.SomeContent == criteria, PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(5));
            await Assert.ThatAsync(() => SUT.AnyAsync<T>(e => e.SomeContent == criteria, PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public async Task DeleteManyAsync()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            await SUT.AddManyAsync(documents);
            // Act
            var result = await SUT.DeleteManyAsync(documents);
            // Assert
            Assert.That(result, Is.EqualTo(5));
            await Assert.ThatAsync(() => SUT.AnyAsync<T>(e => e.SomeContent == criteria, PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public void DeleteManyLinq()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany(documents);
            // Act
            var result = SUT.DeleteMany<T>(e => e.SomeContent == criteria, PartitionKey);
            // Assert
            Assert.That(result, Is.EqualTo(5));
            Assert.That(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), Is.False, GetTestName());
        }

        [Test]
        public void DeleteMany()
        {
            // Arrange
            var criteria = $"{GetTestName()}.{DocumentTypeName}";
            var documents = CreateTestDocuments(5);
            documents.ForEach(e => e.SomeContent = criteria);
            SUT.AddMany(documents);
            // Act
            var result = SUT.DeleteMany(documents);
            // Assert
            Assert.That(result, Is.EqualTo(5));
            Assert.That(SUT.Any<T>(e => e.SomeContent == criteria, PartitionKey), Is.False, GetTestName());
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
            await SUT.AddOneAsync(document);
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
            SUT.AddOne(document);
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

            await SUT.AddManyAsync(documents);
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

            SUT.AddMany(documents);
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
            Assert.That(result.Count, Is.EqualTo(5), GetTestName());
            Assert.That(result.First().SomeContent, Is.EqualTo(someContent), GetTestName());
            Assert.That(result.First().SomeDate.Minute, Is.EqualTo(someDate.Minute), GetTestName());
            Assert.That(result.First().SomeDate.Second, Is.EqualTo(someDate.Second), GetTestName());
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
            Assert.That(key1Group, Is.Not.Null);
            Assert.That(key1Group.Content, Has.Count.EqualTo(3));
            var key2Group = result.First(e => e.Key == 2);
            Assert.That(key2Group, Is.Not.Null);
            Assert.That(key2Group.Content, Has.Count.EqualTo(2));
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
            Assert.That(key1Group, Is.Not.Null);
            Assert.That(key1Group.Content, Has.Count.EqualTo(3));
            var key2Group = result.First(e => e.Key == 5);
            Assert.That(key2Group, Is.Not.Null);
            Assert.That(key2Group.Content, Has.Count.EqualTo(1));
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
