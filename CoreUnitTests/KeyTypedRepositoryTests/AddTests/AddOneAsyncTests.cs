using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.AddTests;

public class AddOneAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    [Fact]
    public async Task WithDocument_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddOneAsync(document);

        // Assert
        Creator.Verify(x => x.AddOneAsync<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddOneAsync(document, token);

        // Assert
        Creator.Verify(x => x.AddOneAsync<TestDocumentWithKey<int>, int>(document, token), Times.Once);
    }
}
