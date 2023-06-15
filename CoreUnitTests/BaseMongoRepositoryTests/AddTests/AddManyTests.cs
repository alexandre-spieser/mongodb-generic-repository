using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.AddTests;

public class AddManyTests : TestMongoRepositoryContext
{
    
    [Fact]
    public async Task AddManyAsync_EnsureTokenPassed()
    {
        // Arrange
        Creator = new Mock<IMongoDbCreator>();
        var token = new CancellationToken();
        var documents = new List<TestDocument>
        {
            new(), new(), new()
        };

        // Act
        await Sut.AddManyAsync(documents, token);

        // Assert
        Creator.Verify(x => x.AddManyAsync<TestDocument, Guid>(documents, token));
    }
}