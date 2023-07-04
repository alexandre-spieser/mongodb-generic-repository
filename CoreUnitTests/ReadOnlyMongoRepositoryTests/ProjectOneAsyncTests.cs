using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.ReadOnlyMongoRepositoryTests;

public class ProjectOneAsyncTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocument, TestProjection>> projection = document => new TestProjection {NestedData = document.Nested.SomeDate};

    [Fact]
    public async Task WithFilterAndProjection_Projects()
    {
        // Arrange
        var expected = Fixture.Create<TestProjection>();

        SetupReader(expected);

        // Act
        var result = await Sut.ProjectOneAsync(filter, projection);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.ProjectOneAsync<TestDocument, TestProjection, Guid>(
                filter,
                projection,
                null,
                CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndProjectionAndCancellationToken_Projects()
    {
        // Arrange
        var expected = Fixture.Create<TestProjection>();
        var token = new CancellationToken(true);

        SetupReader(expected);

        // Act
        var result = await Sut.ProjectOneAsync(filter, projection, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.ProjectOneAsync<TestDocument, TestProjection, Guid>(filter, projection, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndProjectionAndPartitionKey_Projects()
    {
        // Arrange
        var expected = Fixture.Create<TestProjection>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(expected);

        // Act
        var result = await Sut.ProjectOneAsync(filter, projection, partitionKey);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.ProjectOneAsync<TestDocument, TestProjection, Guid>(filter, projection, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndProjectionAndPartitionKeyAndCancellationToken_Projects()
    {
        // Arrange
        var expected = Fixture.Create<TestProjection>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(expected);

        // Act
        var result = await Sut.ProjectOneAsync(filter, projection, partitionKey, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.ProjectOneAsync<TestDocument, TestProjection, Guid>(filter, projection, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(TestProjection result)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.ProjectOneAsync<TestDocument, TestProjection, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
    }

    #region keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedFilter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocumentWithKey<int>, TestProjection>> keyedProjection = document => new TestProjection {NestedData = document.Nested.SomeDate};

    [Fact]
    public async Task Keyed_WithFilterAndProjection_Projects()
    {
        // Arrange
        var expected = Fixture.Create<TestProjection>();

        SetupKeyedReader(expected);

        // Act
        var result = await Sut.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(keyedFilter, keyedProjection);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(
                keyedFilter,
                keyedProjection,
                null,
                CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndProjectionAndCancellationToken_Projects()
    {
        // Arrange
        var expected = Fixture.Create<TestProjection>();
        var token = new CancellationToken(true);

        SetupKeyedReader(expected);

        // Act
        var result = await Sut.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(keyedFilter, keyedProjection, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(keyedFilter, keyedProjection, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndProjectionAndPartitionKey_Projects()
    {
        // Arrange
        var expected = Fixture.Create<TestProjection>();
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReader(expected);

        // Act
        var result = await Sut.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(keyedFilter, keyedProjection, partitionKey);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(keyedFilter, keyedProjection, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndProjectionAndPartitionKeyAndCancellationToken_Projects()
    {
        // Arrange
        var expected = Fixture.Create<TestProjection>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReader(expected);

        // Act
        var result = await Sut.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(keyedFilter, keyedProjection, partitionKey, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(keyedFilter, keyedProjection, partitionKey, token),
            Times.Once);
    }

    private void SetupKeyedReader(TestProjection result)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.ProjectOneAsync<TestDocumentWithKey<int>, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
    }

    #endregion
}
