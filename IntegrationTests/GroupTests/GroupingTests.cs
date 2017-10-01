using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests.GroupTests
{
    public class GroupingTestsDocument : Document
    {
        public GroupingTestsDocument()
        {
            Version = 2;
            Children = new List<Child>();
        }
        public string SomeContent { get; set; }
        public int GroupingKey { get; set; }
        public List<Child> Children { get; set; }
    }

    public class ProjectedGroup
    {
        public int Key { get; set; }
        public List<string> Content { get; set; } 
    }

    public class GroupingTests : BaseMongoDbRepositoryTests<GroupingTestsDocument>
    {
        [Test]
        public void GroupByTProjection()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            for(var i = 0; i < documents.Count - 2; i++)
            {
                documents[i].GroupingKey = 1;
                documents[i].SomeContent = $"content-{i}";
            }
            for (var i = 3; i < documents.Count; i++)
            {
                documents[i].GroupingKey = 2;
                documents[i].SomeContent = $"content-{i}";
            }
            SUT.AddMany(documents);

            // Act

            var result = SUT.GroupBy<GroupingTestsDocument, int, ProjectedGroup>(
                            e => e.GroupingKey, g => new ProjectedGroup {
                                Key = g.Key,
                                Content = g.Select(doc => doc.SomeContent).ToList()
                            });

            // Assert
            var key1Group = result.First(e => e.Key == 1);
            Assert.NotNull(key1Group);
            Assert.AreEqual(3, key1Group.Content.Count);
            var key2Group = result.First(e => e.Key == 2);
            Assert.NotNull(key2Group);
            Assert.AreEqual(2, key2Group.Content.Count);
        }

        [Test]
        public void FilteredGroupByTProjection()
        {
            // Arrange
            var documents = CreateTestDocuments(5);
            for (var i = 0; i < documents.Count - 2; i++)
            {
                documents[i].GroupingKey = 4;
                documents[i].SomeContent = $"content-{i}";
            }
            for (var i = 3; i < documents.Count; i++)
            {
                documents[i].GroupingKey = 5;
                documents[i].SomeContent = $"content-{i}";
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
            var result = SUT.GroupBy<GroupingTestsDocument, int, ProjectedGroup>(
                            e => e.Children.Any(c => c.Type == guid1),
                            e => e.GroupingKey, g => new ProjectedGroup
                            {
                                Key = g.Key,
                                Content = g.Select(doc => doc.SomeContent).ToList()
                            });

            // Assert
            var key1Group = result.First(e => e.Key == 4);
            Assert.NotNull(key1Group);
            Assert.AreEqual(3, key1Group.Content.Count);
            var key2Group = result.First(e => e.Key == 5);
            Assert.NotNull(key2Group);
            Assert.AreEqual(1, key2Group.Content.Count);
        }
    }
}
