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

public class GetAllAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";

    [Fact]
    public async Task WithFilter_GetsOne()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync(filter);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocument, Guid>(filter, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync(filter, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocument, Guid>(filter, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKey_GetsOne()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync(filter, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocument, Guid>(filter, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKeyAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync(filter, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocument, Guid>(filter, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(List<TestDocument> documents)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetAllAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);
    }
}
