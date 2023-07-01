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

public class GroupByTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
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
}
