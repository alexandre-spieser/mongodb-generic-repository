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

namespace CoreUnitTests.ReadOnlyMongoRepositoryTests;

public class GetSortedPaginatedAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.GroupingKey == 1;
    private readonly Expression<Func<TestDocument, object>> selector = document => document.GroupingKey;
    private readonly SortDefinition<TestDocument> sortDefinition = Builders<TestDocument>.Sort.Ascending(document => document.GroupingKey);

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

    private List<TestDocument> SetupReaderWithSortSelector()
    {
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GetSortedPaginatedAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, object>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);
        return documents;
    }

    private void VerifySelector(List<TestDocument> result, List<TestDocument> documents, bool ascending, int skipNumber, int takeNumber, string partitionKey, CancellationToken cancellationToken)
    {
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(documents);
        Reader.Verify(
            x => x.GetSortedPaginatedAsync<TestDocument, Guid>(
                filter,
                selector,
                ascending,
                skipNumber,
                takeNumber,
                partitionKey,
                cancellationToken),
            Times.Once);
    }

    private List<TestDocument> SetupReaderWithSortDefinition()
    {
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        Reader = new Mock<IMongoDbReader>();

        Reader.Setup(
                x => x.GetSortedPaginatedAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<SortDefinition<TestDocument>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);
        return documents;
    }

    private void VerifyDefinition(List<TestDocument> result, List<TestDocument> documents, int skipNumber, int takeNumber, string partitionKey, CancellationToken cancellationToken)
    {
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(documents);
        Reader.Verify(
            x => x.GetSortedPaginatedAsync<TestDocument, Guid>(
                filter,
                sortDefinition,
                skipNumber,
                takeNumber,
                partitionKey,
                cancellationToken),
            Times.Once);
    }
}
