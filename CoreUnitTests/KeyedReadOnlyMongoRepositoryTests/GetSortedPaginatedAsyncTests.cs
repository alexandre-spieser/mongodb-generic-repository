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
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyedReadOnlyMongoRepositoryTests;

public class GetSortedPaginatedAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<int>
{
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> filter = document => document.GroupingKey == 1;
    private readonly Expression<Func<TestDocumentWithKey<int>, object>> selector = document => document.GroupingKey;
    private readonly SortDefinition<TestDocumentWithKey<int>> sortDefinition = Builders<TestDocumentWithKey<int>>.Sort.Ascending(document => document.GroupingKey);

    private const bool DefaultAscending = true;
    private const int DefaultSkipNumber = 0;
    private const int DefaultTakeNumber = 50;

    [Fact]
    public async Task WithFilterAndSortSelector_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortSelector();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, selector);

        // Assert
        VerifySelector(result, documents, DefaultAscending, DefaultSkipNumber, DefaultTakeNumber, null, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortSelectorAndAscending_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortSelector();
        var ascending = Fixture.Create<bool>();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, selector, ascending);

        // Assert
        VerifySelector(result, documents, ascending, DefaultSkipNumber, DefaultTakeNumber, null, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortSelectorAndSkipNumber_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortSelector();
        var skipNumber = Fixture.Create<int>();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, selector, skipNumber: skipNumber);

        // Assert
        VerifySelector(result, documents, DefaultAscending, skipNumber, DefaultTakeNumber, null, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortSelectorAndTakeNumber_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortSelector();
        var ascending = Fixture.Create<bool>();
        var takeNumber = Fixture.Create<int>();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, selector, ascending, takeNumber: takeNumber);

        // Assert
        VerifySelector(result, documents, ascending, DefaultSkipNumber, takeNumber, null, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortSelectorAndPartitionKey_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortSelector();
        var partitionKey = Fixture.Create<string>();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, selector, partitionKey: partitionKey);

        // Assert
        VerifySelector(result, documents, DefaultAscending, DefaultSkipNumber, DefaultTakeNumber, partitionKey, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortSelectorAndCancellationToken_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortSelector();
        var cancellationToken = new CancellationToken(true);

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, selector, cancellationToken: cancellationToken);

        // Assert
        VerifySelector(result, documents, DefaultAscending, DefaultSkipNumber, DefaultTakeNumber, null, cancellationToken);
    }

    [Fact]
    public async Task WithFilterAndSortDefinition_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortDefinition();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, sortDefinition);

        // Assert
        VerifyDefinition(result, documents, DefaultSkipNumber, DefaultTakeNumber, null, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortDefinitionAndSkipNumber_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortDefinition();
        var skipNumber = Fixture.Create<int>();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, sortDefinition, skipNumber);

        // Assert
        VerifyDefinition(result, documents, skipNumber, DefaultTakeNumber, null, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortDefinitionAndTakeNumber_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortDefinition();
        var takeNumber = Fixture.Create<int>();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, sortDefinition, takeNumber: takeNumber);

        // Assert
        VerifyDefinition(result, documents, DefaultSkipNumber, takeNumber, null, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortDefinitionAndPartitionKey_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortDefinition();
        var partitionKey = Fixture.Create<string>();

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, sortDefinition, partitionKey: partitionKey);

        // Assert
        VerifyDefinition(result, documents, DefaultSkipNumber, DefaultTakeNumber, partitionKey, CancellationToken.None);
    }

    [Fact]
    public async Task WithFilterAndSortDefinitionAndCancellationToken_GetsResults()
    {
        // Arrange
        var documents = SetupReaderWithSortDefinition();
        var token = new CancellationToken(true);

        // Act
        var result = await Sut.GetSortedPaginatedAsync(filter, sortDefinition, cancellationToken: token);

        // Assert
        VerifyDefinition(result, documents, DefaultSkipNumber, DefaultTakeNumber, null, token);
    }

    private List<TestDocumentWithKey<int>> SetupReaderWithSortSelector()
    {
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GetSortedPaginatedAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, object>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);
        return documents;
    }

    private void VerifySelector(List<TestDocumentWithKey<int>> result, List<TestDocumentWithKey<int>> documents, bool ascending, int skipNumber, int takeNumber, string partitionKey, CancellationToken cancellationToken)
    {
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(documents);
        Reader.Verify(
            x => x.GetSortedPaginatedAsync<TestDocumentWithKey<int>, int>(
                filter,
                selector,
                ascending,
                skipNumber,
                takeNumber,
                partitionKey,
                cancellationToken),
            Times.Once);
    }

    private List<TestDocumentWithKey<int>> SetupReaderWithSortDefinition()
    {
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GetSortedPaginatedAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<SortDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);
        return documents;
    }

    private void VerifyDefinition(List<TestDocumentWithKey<int>> result, List<TestDocumentWithKey<int>> documents, int skipNumber, int takeNumber, string partitionKey, CancellationToken cancellationToken)
    {
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(documents);
        Reader.Verify(
            x => x.GetSortedPaginatedAsync<TestDocumentWithKey<int>, int>(
                filter,
                sortDefinition,
                skipNumber,
                takeNumber,
                partitionKey,
                cancellationToken),
            Times.Once);
    }
}
