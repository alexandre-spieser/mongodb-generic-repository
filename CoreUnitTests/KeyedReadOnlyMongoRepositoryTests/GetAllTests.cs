using System;
using System.Collections.Generic;
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

namespace CoreUnitTests.KeyedReadOnlyMongoRepositoryTests;

public class GetAllTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";

    [Fact]
    public void WithFilter_GetsOne()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();

        SetupReader(document);

        // Act
        var result = Sut.GetAll(filter);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAll<TestDocument, Guid>(filter, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetAll(filter, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAll<TestDocument, Guid>(filter, null, token),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKey_GetsOne()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = Sut.GetAll(filter, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAll<TestDocument, Guid>(filter, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKeyAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetAll(filter, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAll<TestDocument, Guid>(filter, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(List<TestDocument> documents)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetAll<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(documents);
    }
}
