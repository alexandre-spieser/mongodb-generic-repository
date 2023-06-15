using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.DeleteTests;

public class DeleteOneAsyncTests
{
    [Fact]
    public async Task DeleteOneAsync_WithDocument_ShouldDeleteOne()
    {
        // Arrange
        var repository = GetNewRepository();
        var document = new TestDocument<TKey> {Name = "DeleteOneAsync_WithDocument_ShouldDeleteOne"};
        await repository.InsertOneAsync(document);

        // Act
        var result = await repository.DeleteOneAsync(document);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task DeleteOneAsync_WithDocumentAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var repository = GetNewRepository();
        var document = new TestDocument<TKey> {Name = "DeleteOneAsync_WithDocumentAndCancellationToken_ShouldDeleteOne"};
        await repository.InsertOneAsync(document);

        // Act
        var result = await repository.DeleteOneAsync(document, CancellationToken.None);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task DeleteOneAsync_WithDocumentAndCancellationToken_ShouldDeleteOneWithCancellationToken()
    {
        // Arrange
        var repository = GetNewRepository();
        var document = new TestDocument<TKey> {Name = "DeleteOneAsync_WithDocumentAndCancellationToken_ShouldDeleteOneWithCancellationToken"};
        await repository.InsertOneAsync(document);

        // Act
        var result = await repository.DeleteOneAsync(document, new CancellationToken(true));

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public async Task DeleteOneAsync_WithFilter_ShouldDeleteOne()
    {
        // Arrange
        var repository = GetNewRepository();
        var document = new TestDocument<TKey> {Name = "DeleteOneAsync_WithFilter_ShouldDeleteOne"};
        await repository.InsertOneAsync(document);

        // Act
        var result = await repository.DeleteOneAsync(x => x.Name == document.Name);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task DeleteOneAsync_WithFilterAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var repository = GetNewRepository();
        var document = new TestDocument<TKey> {Name = "DeleteOneAsync_WithFilterAndCancellationToken_ShouldDeleteOne"};
        await repository.InsertOneAsync(document);

        // Act
        var result = await repository.DeleteOneAsync(x => x
    }
}