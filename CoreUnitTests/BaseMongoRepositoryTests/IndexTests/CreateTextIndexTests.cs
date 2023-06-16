using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.Models;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.IndexTests;

public class CreateTextIndexTests : BaseIndexTests
{
    private readonly Expression<Func<TestDocument, object>> _fieldExpression = t => t.SomeContent2;

    [Fact]
    public async Task Ensure_Creates_Index()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateTextIndexAsync<TestDocument>(_fieldExpression);

        // Assert
        IndexHandler.Verify(
            x => x.CreateTextIndexAsync<TestDocument, Guid>(_fieldExpression, null, null));
    }

    [Fact]
    public async Task Ensure_Passes_Options()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var options = new IndexCreationOptions { Name = "theIndexName" };

        // Act
        await Sut.CreateTextIndexAsync<TestDocument>(_fieldExpression, options);

        // Assert
        IndexHandler.Verify(
            x => x.CreateTextIndexAsync<TestDocument, Guid>(
                _fieldExpression, options, null));
    }

    [Fact]
    public async Task Ensure_Passes_PartitionKey()
    {
        // Arrange
        const string partitionKey = "thePartitionKey";

        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateTextIndexAsync<TestDocument>(_fieldExpression, partitionKey: partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateTextIndexAsync<TestDocument, Guid>(
                _fieldExpression, null, partitionKey));
    }

    /*
    [Fact]
    public async Task Ensure_Creates_Index_With_CancellationToken()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var token = new CancellationToken(true);

        // Act
        await Sut.CreateTextIndexAsync<TestDocument>(_fieldExpression, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateTextIndexAsync<TestDocument, Guid>(_fieldExpression, null, null, token));
    }

    [Fact]
    public async Task Ensure_Passes_Options_With_CancellationToken()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var token = new CancellationToken(true);
        var options = new IndexCreationOptions { Name = "theIndexName" };

        // Act
        await Sut.CreateTextIndexAsync<TestDocument>(_fieldExpression, token, options);

        // Assert
        IndexHandler.Verify(
            x => x.CreateTextIndexAsync<TestDocument, Guid>(
                _fieldExpression, options, null, token));
    }

    [Fact]
    public async Task Ensure_Passes_PartitionKey_With_CancellationToken()
    {
        // Arrange
        const string partitionKey = "thePartitionKey";
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateTextIndexAsync<TestDocument>(_fieldExpression, token, partitionKey: partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateTextIndexAsync<TestDocument, Guid>(
                _fieldExpression, null, partitionKey, token));
    }

    [Fact]
    public async Task Ensure_Creates_Index_Custom_Key()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        Expression<Func<TestDocumentWithKey, object>> fieldExpression = t => t.SomeContent2;

        // Act
        await Sut.CreateTextIndexAsync<TestDocumentWithKey, int>(fieldExpression);

        // Assert
        IndexHandler.Verify(x => x.CreateTextIndexAsync<TestDocumentWithKey, int>(fieldExpression, null, null, default));
    }

    [Fact]
    public async Task Ensure_Passes_CancellationToken_Custom_Key()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var token = new CancellationToken(true);

        // Act
        await Sut.CreateTextIndexAsync<TestDocumentWithKey, int>(t => t.SomeContent2, token);

        // Assert
        IndexHandler.Verify(
            x => x.CreateTextIndexAsync<TestDocumentWithKey, int>(
                t => t.SomeContent2, null, null, token));
    }

    [Fact]
    public async Task Ensure_Passes_Options_Custom_Key()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var options = new IndexCreationOptions { Name = "theIndexName" };

        // Act
        await Sut.CreateTextIndexAsync<TestDocumentWithKey, int>(t => t.SomeContent2, options);

        // Assert
        IndexHandler.Verify(x => x.CreateTextIndexAsync<TestDocumentWithKey, int>(t => t.SomeContent2, options, null, default));
    }

    [Fact]
    public async Task Ensure_Passes_PartitionKey_Custom_Key()
    {
        // Arrange
        const string partitionKey = "thePartitionKey";

        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var options = new IndexCreationOptions { Name = "theIndexName" };

        // Act
        await Sut.CreateTextIndexAsync<TestDocumentWithKey, int>(t => t.SomeContent2, options, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.CreateTextIndexAsync<TestDocumentWithKey, int>(
                t => t.SomeContent2, options, partitionKey, default));
    }

    [Fact]
    public async Task Ensure_Passes_PartitionKey_And_CancellationToken_Custom_Key()
    {
        // Arrange
        const string partitionKey = "thePartitionKey";
        const string indexName = "theIndexName";
        var token = new CancellationToken(true);
        var options = new IndexCreationOptions { Name = indexName };
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.CreateTextIndexAsync<TestDocumentWithKey, int>(t => t.SomeContent2, token, options, partitionKey);

        // Assert
        IndexHandler
            .Verify(x => x.CreateTextIndexAsync<TestDocumentWithKey, int>(
                t => t.SomeContent2, options, partitionKey, token));
    }
    */
}
