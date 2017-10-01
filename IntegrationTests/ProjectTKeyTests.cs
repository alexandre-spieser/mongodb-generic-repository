using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class NestedTKey
    {
        public DateTime SomeDate { get; set; }
    }

    public class MyProjectionTKey
    {
        public DateTime SomeDate { get; set; }
        public string SomeContent { get; set; }
    }

    public class ProjectTestsTKeyDocument : IDocument<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public ProjectTestsTKeyDocument()
        {
            Id = Guid.NewGuid();
            Version = 2;
            Nested = new NestedTKey
            {
                SomeDate = DateTime.UtcNow
            };
        }
        public string SomeContent { get; set; }
        public NestedTKey Nested { get; set; }
    }

    public class ProjectTKeyTests : BaseMongoDbRepositoryTests<ProjectTestsTKeyDocument>
    {
        [Test]
        public async Task ProjectOneAsync()
        {
            // Arrange
            const string someContent = "ProjectOneAsyncContent";
            var someDate = DateTime.UtcNow;
            var document = CreateTestDocument();
            document.SomeContent = someContent;
            document.Nested.SomeDate = someDate;
            SUT.AddOne<ProjectTestsTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.ProjectOneAsync<ProjectTestsTKeyDocument, MyProjection, Guid>(
                x => x.Id == document.Id, 
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                });
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(someContent, result.SomeContent);
            Assert.AreEqual(someDate.Minute, result.SomeDate.Minute);
            Assert.AreEqual(someDate.Second, result.SomeDate.Second);
        }

        [Test]
        public void ProjectOne()
        {
            // Arrange
            const string someContent = "ProjectOneContent";
            var someDate = DateTime.UtcNow;
            var document = CreateTestDocument();
            document.SomeContent = someContent;
            document.Nested.SomeDate = someDate;
            SUT.AddOne<ProjectTestsTKeyDocument, Guid>(document);
            // Act
            var result = SUT.ProjectOne<ProjectTestsTKeyDocument, MyProjection, Guid>(
                x => x.Id == document.Id,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                });
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(someContent, result.SomeContent);
            Assert.AreEqual(someDate.Minute, result.SomeDate.Minute);
            Assert.AreEqual(someDate.Second, result.SomeDate.Second);
        }

        [Test]
        public async Task ProjectManyAsync()
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

            SUT.AddMany<ProjectTestsTKeyDocument, Guid>(document);
            // Act
            var result = await SUT.ProjectManyAsync<ProjectTestsTKeyDocument, MyProjection, Guid>(
                x => x.SomeContent == someContent,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                });
            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(someContent, result.First().SomeContent);
            Assert.AreEqual(someDate.Minute, result.First().SomeDate.Minute);
            Assert.AreEqual(someDate.Second, result.First().SomeDate.Second);
        }

        [Test]
        public void ProjectMany()
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

            SUT.AddMany<ProjectTestsTKeyDocument, Guid>(document);
            // Act
            var result = SUT.ProjectMany<ProjectTestsTKeyDocument, MyProjection, Guid>(
                x => x.SomeContent == someContent,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                });
            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(someContent, result.First().SomeContent);
            Assert.AreEqual(someDate.Minute, result.First().SomeDate.Minute);
            Assert.AreEqual(someDate.Second, result.First().SomeDate.Second);
        }
    }
}
