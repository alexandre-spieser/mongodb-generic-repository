using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.Models;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.IndexTests;

public class CreateHashedIndexAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    private readonly Expression<Func<TestDocumentWithKey<int>, object>> keyedFieldExpression = t => t.SomeContent2;

    [Fact]
    public async Task WithFieldExpression_CreatesIndex()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateHashedIndexAsync(keyedFieldExpression);

        // Assert
        IndexHandler.Verify(
            x => x.CreateHashedIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpression, null, null, CancellationToken.None));
    }

    [Fact]
    public async Task WithFieldExpressionAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateHashedIndexAsync(keyedFieldExpression, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateHashedIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpression, null, null, token));
    }

    [Fact]
    public async Task WithFieldExpressionAndOptions_CreatesIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateHashedIndexAsync(keyedFieldExpression, options);

        // Assert
        IndexHandler.Verify(
            x => x.CreateHashedIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpression, options, null, CancellationToken.None));
    }

    [Fact]
    public async Task WithFieldExpressionAndOptionsAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateHashedIndexAsync(keyedFieldExpression, options, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateHashedIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpression, options, null, token));
    }

    [Fact]
    public async Task WithFieldExpressionAndPartitionKey_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateHashedIndexAsync(keyedFieldExpression, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateHashedIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpression, null, partitionKey, CancellationToken.None));
    }

    [Fact]
    public async Task WithFieldExpressionAndPartitionKeyAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateHashedIndexAsync(keyedFieldExpression, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateHashedIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpression, null, partitionKey, token));
    }

    [Fact]
    public async Task WithFieldExpressionAndOptionsAndPartitionKey_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };

        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateHashedIndexAsync(keyedFieldExpression, options, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateHashedIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpression, options, partitionKey, CancellationToken.None));
    }

    [Fact]
    public async Task WithFieldExpressionAndOptionsAndPartitionKeyAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };

        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateHashedIndexAsync(keyedFieldExpression, options, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateHashedIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpression, options, partitionKey, token));
    }
}
