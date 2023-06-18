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

public class AddManyTests : TestKeyedMongoRepositoryContext<int>
{
    [Fact]
    public void WithDocument_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddMany(documents);

        // Assert
        Creator.Verify(x => x.AddMany<TestDocumentWithKey<int>, int>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddMany(documents, token);

        // Assert
        Creator.Verify(x => x.AddMany<TestDocumentWithKey<int>, int>(documents, token), Times.Once);
    }
}
