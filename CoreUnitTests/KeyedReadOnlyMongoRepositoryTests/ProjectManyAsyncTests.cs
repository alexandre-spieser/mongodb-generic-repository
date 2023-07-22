using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CoreUnitTests.KeyedReadOnlyMongoRepositoryTests;

public class ProjectManyAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<int>
{
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> filter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocumentWithKey<int>, TestProjection>> projection = document => new TestProjection {NestedData = document.Nested.SomeDate};

    [Fact]
    public async Task WithFilterAndProjection_Projects()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();

        SetupReader(projections);

        // Act
        var result = await Sut.ProjectManyAsync(filter, projection);

        // Assert
        result.Should().OnlyContain(x => projections.Contains(x));
        Reader.Verify(
            x => x.ProjectManyAsync<TestDocumentWithKey<int>, TestProjection, int>(
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
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var token = new CancellationToken(true);

        SetupReader(projections);

        // Act
        var result = await Sut.ProjectManyAsync(filter, projection, token);

        // Assert
        result.Should().OnlyContain(x => projections.Contains(x));
        Reader.Verify(
            x => x.ProjectManyAsync<TestDocumentWithKey<int>, TestProjection, int>(filter, projection, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndProjectionAndPartitionKey_Projects()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();

        SetupReader(projections);

        // Act
        var result = await Sut.ProjectManyAsync(filter, projection, partitionKey);

        // Assert
        result.Should().OnlyContain(x => projections.Contains(x));
        Reader.Verify(
            x => x.ProjectManyAsync<TestDocumentWithKey<int>, TestProjection, int>(filter, projection, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndProjectionAndPartitionKeyAndCancellationToken_Projects()
    {
        // Arrange
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(projections);

        // Act
        var result = await Sut.ProjectManyAsync(filter, projection, partitionKey, token);

        // Assert
        result.Should().OnlyContain(x => projections.Contains(x));
        Reader.Verify(
            x => x.ProjectManyAsync<TestDocumentWithKey<int>, TestProjection, int>(filter, projection, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(List<TestProjection> projections)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.ProjectManyAsync<TestDocumentWithKey<int>, TestProjection, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, TestProjection>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(projections);
    }
}
