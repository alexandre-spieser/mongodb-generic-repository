using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.Models;
using Moq;
using Xunit;
using CancellationToken = System.Threading.CancellationToken;

namespace CoreUnitTests.BaseMongoRepositoryTests.IndexTests;

public class CreateCombinedTextIndexAsyncTests : BaseIndexTests
{
    private readonly List<Expression<Func<TestDocument, object>>> fieldExpressions = new() {t => t.SomeContent2, t => t.SomeContent3};
    private readonly List<Expression<Func<TestDocumentWithKey<int>, object>>> keyedFieldExpressions = new() {t => t.SomeContent2, t => t.SomeContent3};

    [Fact]
    public async Task WithFieldExpression_CreatesIndex()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(fieldExpressions);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocument, Guid>(fieldExpressions, null, null, CancellationToken.None));
    }

    [Fact]
    public async Task WithFieldExpressionAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(fieldExpressions, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocument, Guid>(fieldExpressions, null, null, token));
    }

    [Fact]
    public async Task WithFieldExpressionAndOptions_CreatesIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(fieldExpressions, options);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocument, Guid>(
                fieldExpressions, options, null, CancellationToken.None));
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
        await Sut.CreateCombinedTextIndexAsync(fieldExpressions, options, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocument, Guid>(
                fieldExpressions, options, null, token));
    }

    [Fact]
    public async Task WithFieldExpressionAndPartitionKey_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(fieldExpressions, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocument, Guid>(
                fieldExpressions, null, partitionKey, CancellationToken.None));
    }

    [Fact]
    public async Task WithFieldExpressionAndPartitionKeyAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync(fieldExpressions, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocument, Guid>(
                fieldExpressions, null, partitionKey, token));
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
        await Sut.CreateCombinedTextIndexAsync(fieldExpressions, options, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocument, Guid>(
                fieldExpressions, options, partitionKey, CancellationToken.None));
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
        await Sut.CreateCombinedTextIndexAsync(fieldExpressions, options, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocument, Guid>(
                fieldExpressions, options, partitionKey, token));
    }

    #region Keyed

    [Fact]
    public async Task Keyed_WithKeyedFieldExpression_CreatesIndex()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, null, null, CancellationToken.None));
    }

    [Fact]
    public async Task Keyed_WithKeyedFieldExpressionAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, null, null, token));
    }

    [Fact]
    public async Task Keyed_WithKeyedFieldExpressionAndOptions_CreatesIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, options);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, options, null, CancellationToken.None));
    }

    [Fact]
    public async Task Keyed_WithKeyedFieldExpressionAndOptionsAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, options, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, options, null, token));
    }

    [Fact]
    public async Task Keyed_WithKeyedFieldExpressionAndPartitionKey_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, null, partitionKey, CancellationToken.None));
    }

    [Fact]
    public async Task Keyed_WithKeyedFieldExpressionAndPartitionKeyAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, null, partitionKey, token));
    }

    [Fact]
    public async Task Keyed_WithKeyedFieldExpressionAndOptionsAndPartitionKey_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };

        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, options, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, options, partitionKey, CancellationToken.None));
    }

    [Fact]
    public async Task Keyed_WithKeyedFieldExpressionAndOptionsAndPartitionKeyAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var indexName = Fixture.Create<string>();
        var options = new IndexCreationOptions { Name = indexName };

        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(keyedFieldExpressions, options, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateCombinedTextIndexAsync<TestDocumentWithKey<int>, int>(
                keyedFieldExpressions, options, partitionKey, token));
    }

    #endregion
}
