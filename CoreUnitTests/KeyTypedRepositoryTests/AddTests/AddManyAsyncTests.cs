using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.AddTests;

public class AddManyAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    [Fact]
    public async Task WithDocument_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddManyAsync(documents);

        // Assert
        Creator.Verify(x => x.AddManyAsync<TestDocumentWithKey<int>, int>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddManyAsync(documents, token);

        // Assert
        Creator.Verify(x => x.AddManyAsync<TestDocumentWithKey<int>, int>(documents, token), Times.Once);
    }
}
