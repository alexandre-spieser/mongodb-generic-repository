using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.ReadOnlyMongoRepositoryTests;

public class GroupByTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, int>> grouping = document => document.GroupingKey;
    private readonly Expression<Func<IGrouping<int, TestDocument>, TestProjection>> projection = documents => new TestProjection {Count = documents.Count()};
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.GroupingKey == 1;

    [Fact]
    public void WithGroupingCriteriaAndProjection_Groups()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocument, int, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocument>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(projections);

        // Act
        var result = Sut.GroupBy(grouping, projection);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(projections);
        Reader.Verify(
            x => x.GroupBy<TestDocument, int, TestProjection, Guid>(grouping, projection, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithGroupingCriteriaAndProjectionAndCancellationToken_Groups()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var token = new CancellationToken(true);
        Reader = new Mock<IMongoDbReader>();

        Reader
            .Setup(
                x => x.GroupBy<TestDocument, int, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocument>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(projections);

        // Act
        var result = Sut.GroupBy(grouping, projection, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(projections);
        Reader.Verify(
            x => x.GroupBy<TestDocument, int, TestProjection, Guid>(grouping, projection, null, token),
            Times.Once);
    }

    [Fact]
    public void WithGroupingCriteriaAndProjectionAndPartitionKey_Groups()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        Reader = new Mock<IMongoDbReader>();

        Reader
            .Setup(
                x => x.GroupBy<TestDocument, int, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocument>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(projections);

        // Act
        var result = Sut.GroupBy(grouping, projection, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(projections);
        Reader.Verify(
            x => x.GroupBy<TestDocument, int, TestProjection, Guid>(grouping, projection, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithGroupingCriteriaAndProjectionAndPartitionKeyAndCancellationToken_Groups()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Reader = new Mock<IMongoDbReader>();

        Reader
            .Setup(
                x => x.GroupBy<TestDocument, int, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocument>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(projections);

        // Act
        var result = Sut.GroupBy(grouping, projection, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(projections);
        Reader.Verify(
            x => x.GroupBy<TestDocument, int, TestProjection, Guid>(grouping, projection, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndGroupingCriteriaAndProjection_Groups()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocument, int, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument,bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocument>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(projections);

        // Act
        var result = Sut.GroupBy(filter, grouping, projection);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(projections);
        Reader.Verify(
            x => x.GroupBy<TestDocument, int, TestProjection, Guid>(filter, grouping, projection, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndGroupingCriteriaAndProjectionAndCancellationToken_Groups()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var token = new CancellationToken(true);
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocument, int, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument,bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocument>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(projections);

        // Act
        var result = Sut.GroupBy(filter, grouping, projection, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(projections);
        Reader.Verify(
            x => x.GroupBy<TestDocument, int, TestProjection, Guid>(filter, grouping, projection, null, token),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndGroupingCriteriaAndProjectionAndPartitionKey_Groups()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocument, int, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument,bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocument>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(projections);

        // Act
        var result = Sut.GroupBy(filter, grouping, projection, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(projections);
        Reader.Verify(
            x => x.GroupBy<TestDocument, int, TestProjection, Guid>(filter, grouping, projection, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndGroupingCriteriaAndProjectionAndPartitionKeyAndCancellationToken_Groups()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocument, int, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument,bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocument>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(projections);

        // Act
        var result = Sut.GroupBy(filter, grouping, projection, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(projections);
        Reader.Verify(
            x => x.GroupBy<TestDocument, int, TestProjection, Guid>(filter, grouping, projection, partitionKey, token),
            Times.Once);
    }

    #region Keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, int>> keyedGrouping = document => document.GroupingKey;
    private readonly Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>> keyedProjection = documents => new TestProjection {Count = documents.Count()};
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedFilter = document => document.GroupingKey == 1;

    [Fact]
    public void Keyed_WithGroupingCriteriaAndProjection_Groups()
    {
        // Arrange
        var keyedProjections = Fixture.CreateMany<TestProjection>().ToList();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(keyedProjections);

        // Act
        var result = Sut.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedGrouping, keyedProjection);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(keyedProjections);
        Reader.Verify(
            x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedGrouping, keyedProjection, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithGroupingCriteriaAndProjectionAndCancellationToken_Groups()
    {
        // Arrange
        var keyedProjections = Fixture.CreateMany<TestProjection>().ToList();
        var token = new CancellationToken(true);
        Reader = new Mock<IMongoDbReader>();

        Reader
            .Setup(
                x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(keyedProjections);

        // Act
        var result = Sut.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedGrouping, keyedProjection, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(keyedProjections);
        Reader.Verify(
            x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedGrouping, keyedProjection, null, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithGroupingCriteriaAndProjectionAndPartitionKey_Groups()
    {
        // Arrange
        var keyedProjections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        Reader = new Mock<IMongoDbReader>();

        Reader
            .Setup(
                x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(keyedProjections);

        // Act
        var result = Sut.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedGrouping, keyedProjection, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(keyedProjections);
        Reader.Verify(
            x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedGrouping, keyedProjection, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithGroupingCriteriaAndProjectionAndPartitionKeyAndCancellationToken_Groups()
    {
        // Arrange
        var keyedProjections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Reader = new Mock<IMongoDbReader>();

        Reader
            .Setup(
                x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(keyedProjections);

        // Act
        var result = Sut.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedGrouping, keyedProjection, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(keyedProjections);
        Reader.Verify(
            x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedGrouping, keyedProjection, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndGroupingCriteriaAndProjection_Groups()
    {
        // Arrange
        var keyedProjections = Fixture.CreateMany<TestProjection>().ToList();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>,bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(keyedProjections);

        // Act
        var result = Sut.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedFilter, keyedGrouping, keyedProjection);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(keyedProjections);
        Reader.Verify(
            x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedFilter, keyedGrouping, keyedProjection, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndGroupingCriteriaAndProjectionAndCancellationToken_Groups()
    {
        // Arrange
        var keyedProjections = Fixture.CreateMany<TestProjection>().ToList();
        var token = new CancellationToken(true);
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>,bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(keyedProjections);

        // Act
        var result = Sut.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedFilter, keyedGrouping, keyedProjection, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(keyedProjections);
        Reader.Verify(
            x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedFilter, keyedGrouping, keyedProjection, null, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndGroupingCriteriaAndProjectionAndPartitionKey_Groups()
    {
        // Arrange
        var keyedProjections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>,bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(keyedProjections);

        // Act
        var result = Sut.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedFilter, keyedGrouping, keyedProjection, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(keyedProjections);
        Reader.Verify(
            x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedFilter, keyedGrouping, keyedProjection, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndGroupingCriteriaAndProjectionAndPartitionKeyAndCancellationToken_Groups()
    {
        // Arrange
        var keyedProjections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>,bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<Expression<Func<IGrouping<int, TestDocumentWithKey<int>>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(keyedProjections);

        // Act
        var result = Sut.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedFilter, keyedGrouping, keyedProjection, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(keyedProjections);
        Reader.Verify(
            x => x.GroupBy<TestDocumentWithKey<int>, int, TestProjection, int>(keyedFilter, keyedGrouping, keyedProjection, partitionKey, token),
            Times.Once);
    }

    #endregion
}
