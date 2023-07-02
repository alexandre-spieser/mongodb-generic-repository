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

namespace CoreUnitTests.KeyedReadOnlyMongoRepositoryTests;

public class ProjectOneAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
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
}
