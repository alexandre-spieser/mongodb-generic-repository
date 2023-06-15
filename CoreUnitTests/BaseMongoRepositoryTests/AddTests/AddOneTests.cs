using System;
using System.Threading;
using System.Threading.Tasks;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.AddTests;

public class AddOneTests : TestMongoRepositoryContext
{
    [Fact]
    public async Task AddOneAsync_EnsureTokenPassed()
    {
        // Arrange
        Creator = new Mock<IMongoDbCreator>();
        var token = new CancellationToken();
        var document = new TestDocument();

        // Act
        await Sut.AddOneAsync<TestDocument>(document, token);

        // Assert
        Creator.Verify(x => x.AddOneAsync<TestDocument, Guid>(document, token));
    }
}