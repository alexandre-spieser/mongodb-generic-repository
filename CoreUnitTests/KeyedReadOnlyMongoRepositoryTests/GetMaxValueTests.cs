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

public class GetMaxValueTests : TestKeyedReadOnlyMongoRepositoryContext<int>
{
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> filter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocumentWithKey<int>, int>> selector = document => document.SomeValue;

    [Fact]
    public void WithFilterAndSelector_GetsMaxValue()
    {
        // Arrange
        var value = Fixture.Create<int>();

        SetupReader(value);

        // Act
        var result = Sut.GetMaxValue(filter, selector);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMaxValue<TestDocumentWithKey<int>, int, int>(filter, selector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndCancellationToken_GetsMaxValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var token = new CancellationToken(true);

        SetupReader(value);

        // Act
        var result = Sut.GetMaxValue(filter, selector, token);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMaxValue<TestDocumentWithKey<int>, int, int>(filter, selector, null, token),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndPartitionKey_GetsMaxValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(value);

        // Act
        var result = Sut.GetMaxValue(filter, selector, partitionKey);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMaxValue<TestDocumentWithKey<int>, int, int>(filter, selector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndPartitionKeyAndCancellationToken_GetsMaxValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(value);

        // Act
        var result = Sut.GetMaxValue(filter, selector, partitionKey, token);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMaxValue<TestDocumentWithKey<int>, int, int>(filter, selector, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(int value)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetMaxValue<TestDocumentWithKey<int>, int, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(value);
    }
}
