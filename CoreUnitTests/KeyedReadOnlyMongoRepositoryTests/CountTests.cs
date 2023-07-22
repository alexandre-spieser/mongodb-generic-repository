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

public class CountTests : TestKeyedReadOnlyMongoRepositoryContext<int>
{
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> filter = document => document.SomeContent == "SomeContent";

    [Fact]
    public void WithFilter_GetsOne()
    {
        // Arrange
        var count = Fixture.Create<long>();

        SetupReader(count);

        // Act
        var result = Sut.Count(filter);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.Count<TestDocumentWithKey<int>, int>(filter, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndCancellationToken_GetsOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        SetupReader(count);

        // Act
        var result = Sut.Count(filter, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.Count<TestDocumentWithKey<int>, int>(filter, null, token),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKey_GetsOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(count);

        // Act
        var result = Sut.Count(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.Count<TestDocumentWithKey<int>, int>(filter, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKeyAndCancellationToken_GetsOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(count);

        // Act
        var result = Sut.Count(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.Count<TestDocumentWithKey<int>, int>(filter, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(long count)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.Count<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);
    }
}
