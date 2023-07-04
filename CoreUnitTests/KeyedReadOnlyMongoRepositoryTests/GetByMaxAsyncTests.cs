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

public class GetByMaxAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<int>
{
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> filter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocumentWithKey<int>, object>> selector = document => document.SomeValue;

    [Fact]
    public async Task WithFilterAndSelector_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();

        SetupReader(document);

        // Act
        var result = await Sut.GetByMaxAsync(filter, selector);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMaxAsync<TestDocumentWithKey<int>, int>(filter, selector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndSelectorAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetByMaxAsync(filter, selector, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMaxAsync<TestDocumentWithKey<int>, int>(filter, selector, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndSelectorAndPartitionKey_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = await Sut.GetByMaxAsync(filter, selector, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMaxAsync<TestDocumentWithKey<int>, int>(filter, selector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndSelectorAndPartitionKeyAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetByMaxAsync(filter, selector, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMaxAsync<TestDocumentWithKey<int>, int>(filter, selector, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(TestDocumentWithKey<int> document)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetByMaxAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(document);
    }
}
