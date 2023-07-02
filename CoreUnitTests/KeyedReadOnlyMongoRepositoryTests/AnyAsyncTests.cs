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

public class AnyAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";

    [Fact]
    public async Task WithFilter_GetsResult()
    {
        // Arrange
        SetupReader();

        // Act
        var result = await Sut.AnyAsync(filter);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocument, Guid>(filter, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);

        SetupReader();

        // Act
        var result = await Sut.AnyAsync(filter, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocument, Guid>(filter, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();

        SetupReader();

        // Act
        var result = await Sut.AnyAsync(filter, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocument, Guid>(filter, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader();

        // Act
        var result = await Sut.AnyAsync(filter, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocument, Guid>(filter, partitionKey, token),
            Times.Once);
    }

    private void SetupReader()
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.AnyAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }
}
