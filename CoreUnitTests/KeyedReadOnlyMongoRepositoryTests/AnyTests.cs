using System;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyedReadOnlyMongoRepositoryTests;

public class AnyTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";

    [Fact]
    public void WithFilter_GetsResult()
    {
        // Arrange
        SetupReader();

        // Act
        var result = Sut.Any(filter);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocument, Guid>(filter, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);

        SetupReader();

        // Act
        var result = Sut.Any(filter, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocument, Guid>(filter, null, token),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();

        SetupReader();

        // Act
        var result = Sut.Any(filter, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocument, Guid>(filter, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader();

        // Act
        var result = Sut.Any(filter, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocument, Guid>(filter, partitionKey, token),
            Times.Once);
    }

    private void SetupReader()
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.Any<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);
    }
}
