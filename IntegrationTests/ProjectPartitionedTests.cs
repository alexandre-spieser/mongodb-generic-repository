using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class ProjectTestsPartitionedDocument : PartitionedDocument
    {
        public ProjectTestsPartitionedDocument() : base("TestPartitionKey")
        {
            Version = 2;
            Nested = new Nested
            {
                SomeDate = DateTime.UtcNow
            };
        }

        public string SomeContent { get; set; }

        public Nested Nested { get; set; }
    }

    public class ProjectPartitionedTests : BaseMongoDbRepositoryTests<ProjectTestsPartitionedDocument>
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
            SUT.AddOne(document);
            // Act
            var result = await SUT.ProjectOneAsync<ProjectTestsPartitionedDocument, MyTestProjection>(
                x => x.Id == document.Id,
                x => new MyTestProjection
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
            SUT.AddOne(document);
            // Act
            var result = SUT.ProjectOne<ProjectTestsPartitionedDocument, MyTestProjection>(
                x => x.Id == document.Id,
                x => new MyTestProjection
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

            SUT.AddMany(document);
            // Act
            var result = await SUT.ProjectManyAsync<ProjectTestsPartitionedDocument, MyTestProjection>(
                x => x.SomeContent == someContent,
                x => new MyTestProjection
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

            SUT.AddMany(document);
            // Act
            var result = SUT.ProjectMany<ProjectTestsPartitionedDocument, MyTestProjection>(
                x => x.SomeContent == someContent,
                x => new MyTestProjection
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
