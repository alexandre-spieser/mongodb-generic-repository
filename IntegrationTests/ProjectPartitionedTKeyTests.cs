using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTests
{

    public class ProjectTestsPartitionedTKeyDocument : IDocument<Guid>, IPartitionedDocument
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public ProjectTestsPartitionedTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
            PartitionKey = "TestPartitionKey";
            Nested = new NestedTKey();
        }
        public string PartitionKey { get; set; }
        public NestedTKey Nested { get; set; }
        public string SomeContent { get; set; }
    }

    public class ProjectPartitionedTKeyTests : BaseMongoDbRepositoryTests<ProjectTestsPartitionedTKeyDocument>
    {
        [Test]
        public async Task PartitionedProjectOneAsync()
        {
            // Arrange
            const string someContent = "ProjectOneAsyncContent";
            var someDate = DateTime.UtcNow;
            var document = CreateTestDocument();
            document.SomeContent = someContent;
            document.Nested.SomeDate = someDate;
            SUT.AddOne<ProjectTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.ProjectOneAsync<ProjectTestsPartitionedTKeyDocument, MyProjection, Guid>(
                x => x.Id == document.Id,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(someContent, result.SomeContent);
            Assert.AreEqual(someDate.Minute, result.SomeDate.Minute);
            Assert.AreEqual(someDate.Second, result.SomeDate.Second);
        }

        [Test]
        public void PartitionedProjectOne()
        {
            // Arrange
            const string someContent = "ProjectOneContent";
            var someDate = DateTime.UtcNow;
            var document = CreateTestDocument();
            document.SomeContent = someContent;
            document.Nested.SomeDate = someDate;
            SUT.AddOne<ProjectTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = SUT.ProjectOne<ProjectTestsPartitionedTKeyDocument, MyProjection, Guid>(
                x => x.Id == document.Id,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(someContent, result.SomeContent);
            Assert.AreEqual(someDate.Minute, result.SomeDate.Minute);
            Assert.AreEqual(someDate.Second, result.SomeDate.Second);
        }

        [Test]
        public async Task PartitionedProjectManyAsync()
        {
            // Arrange
            const string someContent = "ProjectManyAsyncContent";
            var someDate = DateTime.UtcNow;
            var document = CreateTestDocuments(5);
            document.ForEach(e =>
            {
                e.SomeContent = someContent;
                e.Nested.SomeDate = someDate;
            });

            SUT.AddMany<ProjectTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.ProjectManyAsync<ProjectTestsPartitionedTKeyDocument, MyProjection, Guid>(
                x => x.SomeContent == someContent,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(someContent, result.First().SomeContent);
            Assert.AreEqual(someDate.Minute, result.First().SomeDate.Minute);
            Assert.AreEqual(someDate.Second, result.First().SomeDate.Second);
        }

        [Test]
        public void PartitionedProjectMany()
        {
            // Arrange
            const string someContent = "ProjectManyContent";
            var someDate = DateTime.UtcNow;
            var document = CreateTestDocuments(5);
            document.ForEach(e =>
            {
                e.SomeContent = someContent;
                e.Nested.SomeDate = someDate;
            });

            SUT.AddMany<ProjectTestsPartitionedTKeyDocument, Guid>(document);
            // Act
            var result = SUT.ProjectMany<ProjectTestsPartitionedTKeyDocument, MyProjection, Guid>(
                x => x.SomeContent == someContent,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(someContent, result.First().SomeContent);
            Assert.AreEqual(someDate.Minute, result.First().SomeDate.Minute);
            Assert.AreEqual(someDate.Second, result.First().SomeDate.Second);
        }
    }
}
