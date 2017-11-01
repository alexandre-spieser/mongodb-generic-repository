using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIntegrationTests
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
        [Fact]
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
            var result = await SUT.ProjectOneAsync<ProjectTestsPartitionedDocument, MyProjection>(
                x => x.Id == document.Id,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(someContent, result.SomeContent);
            Assert.Equal(someDate.Minute, result.SomeDate.Minute);
            Assert.Equal(someDate.Second, result.SomeDate.Second);
        }

        [Fact]
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
            var result = SUT.ProjectOne<ProjectTestsPartitionedDocument, MyProjection>(
                x => x.Id == document.Id,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(someContent, result.SomeContent);
            Assert.Equal(someDate.Minute, result.SomeDate.Minute);
            Assert.Equal(someDate.Second, result.SomeDate.Second);
        }

        [Fact]
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
            var result = await SUT.ProjectManyAsync<ProjectTestsPartitionedDocument, MyProjection>(
                x => x.SomeContent == someContent,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.Equal(5, result.Count);
            Assert.Equal(someContent, result.First().SomeContent);
            Assert.Equal(someDate.Minute, result.First().SomeDate.Minute);
            Assert.Equal(someDate.Second, result.First().SomeDate.Second);
        }

        [Fact]
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
            var result = SUT.ProjectMany<ProjectTestsPartitionedDocument, MyProjection>(
                x => x.SomeContent == someContent,
                x => new MyProjection
                {
                    SomeContent = x.SomeContent,
                    SomeDate = x.Nested.SomeDate
                },
                PartitionKey);
            // Assert
            Assert.Equal(5, result.Count);
            Assert.Equal(someContent, result.First().SomeContent);
            Assert.Equal(someDate.Minute, result.First().SomeDate.Minute);
            Assert.Equal(someDate.Second, result.First().SomeDate.Second);
        }
    }
}
