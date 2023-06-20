using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Index;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.IndexTests;

public class GetIndexNamesTests : BaseIndexTests
{
    [Fact]
    public async Task Ensure_Returns_IndexNames()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        const string indexName = "theIndexName";

        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocument, Guid>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocument>();

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
        IndexHandler.Verify(x => x.GetIndexesNamesAsync<TestDocument, Guid>(null, CancellationToken.None), Times.Once());
    }

    /*
    [Fact]
    public async Task Ensure_Passes_Provided_CancellationToken()
    {
        // Arrange
        const string indexName = "theIndexName";
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocument, Guid>(null, token))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocument>(token);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
    }
    */

    [Fact]
    public async Task Ensure_Handles_PartitionKey()
    {
        // Arrange
        const string partitionKey = "thePartitionKey";
        const string indexName = "theIndexName";

        IndexHandler = new Mock<IMongoDbIndexHandler>();
        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocument, Guid>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocument>(partitionKey);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
    }

    /*[Fact]
    public async Task Ensure_Passes_Provided_CancellationToken_And_Handles_Partition_Key()
    {
        // Arrange
        const string partitionKey = "thePartitionKey";
        const string indexName = "theIndexName";
        var token = new CancellationToken(true);

        IndexHandler = new Mock<IMongoDbIndexHandler>();
        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocument, Guid>(partitionKey, token))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocument>(token, partitionKey);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
    }*/

    /*[Fact]
    public async Task Ensure_Returns_IndexNames_Custom_Primary_Key()
    {
        // Arrange
        const string indexName = "theIndexName";

        IndexHandler = new Mock<IMongoDbIndexHandler>();
        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocumentWithKey, int>(null))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocumentWithKey, int>();

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
    }*/

    /*[Fact]
    public async Task Ensure_Passes_Provided_CancellationToken_Custom_Primary_Key()
    {
        // Arrange
        const string indexName = "theIndexName";
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocumentWithKey, int>(null, token))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocumentWithKey, int>(token);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
    }*/

    /*[Fact]
    public async Task Ensure_Handles_PartitionKey_Custom_Primary_Key()
    {
        // Arrange
        const string indexName = "theIndexName";
        const string partitionKey = "thePartitionKey";

        IndexHandler = new Mock<IMongoDbIndexHandler>();
        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocumentWithKey, int>(partitionKey))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocumentWithKey, int>(partitionKey);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
    }*/

    /*[Fact]
    public async Task Ensure_Passes_Provided_CancellationToken_And_Handles_Partition_Key_Custom_Primary_Key()
    {
        // Arrange
        const string indexName = "theIndexName";
        const string partitionKey = "thePartitionKey";
        var token = new CancellationToken(true);

        IndexHandler = new Mock<IMongoDbIndexHandler>();
        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocumentWithKey, int>(partitionKey, token))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocumentWithKey, int>(token, partitionKey);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
    }*/
}
