using System;
using System.Collections.Generic;
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

public class CreateCombinedTextIndexAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    private readonly List<Expression<Func<TestDocumentWithKey<int>, object>>> keyedFieldExpressions = new() {t => t.SomeContent2, t => t.SomeContent3};

    [Fact]
    public async Task WithFieldExpression_CreatesIndex()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(keyedFieldExpressions);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, null, null, CancellationToken.None));
    }

    [Fact]
    public async Task WithFieldExpressionAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(keyedFieldExpressions, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, null, null, token));
    }

    [Fact]
    public async Task WithFieldExpressionAndOptions_CreatesIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(keyedFieldExpressions, options);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, options, null, CancellationToken.None));
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
        await Sut.CreateCombinedTextIndexAsync(keyedFieldExpressions, options, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, options, null, token));
    }

    [Fact]
    public async Task WithFieldExpressionAndPartitionKey_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(keyedFieldExpressions, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, null, partitionKey, CancellationToken.None));
    }

    [Fact]
    public async Task WithFieldExpressionAndPartitionKeyAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(keyedFieldExpressions, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, null, partitionKey, token));
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
        await Sut.CreateCombinedTextIndexAsync(keyedFieldExpressions, options, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, options, partitionKey, CancellationToken.None));
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
        await Sut.CreateCombinedTextIndexAsync(keyedFieldExpressions, options, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, options, partitionKey, token));
    }
}
