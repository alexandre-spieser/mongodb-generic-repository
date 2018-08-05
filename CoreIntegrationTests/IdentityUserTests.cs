using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CoreIntegrationTests
{
    public class MongoIdentityUser<TKey> : IdentityUser<TKey>, IDocument<TKey>
        where TKey : IEquatable<TKey>
    {
        public int Version { get; set; }
    }

    public class IdentityUserTest : MongoIdentityUser<Guid>, IDocument<Guid>
    {
        public IdentityUserTest()
        {
            Id = Guid.NewGuid();
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    public class IdentityUserTests : BaseMongoDbRepositoryTests<IdentityUserTest>
    {
        [Fact]
        public void AddOne()
        {
            // Arrange
            var document = new IdentityUserTest();
            // Act
            SUT.AddOne<IdentityUserTest, Guid>(document);
            // Assert
            long count = SUT.Count<IdentityUserTest, Guid>(e => e.Id == document.Id);
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new IdentityUserTest();
            // Act
            await SUT.AddOneAsync<IdentityUserTest, Guid>(document);
            // Assert
            long count = SUT.Count<IdentityUserTest, Guid>(e => e.Id == document.Id);
            Assert.Equal(1, count);
        }

        [Fact]
        public void AddMany()
        {
            // Arrange
            var documents = new List<IdentityUserTest> { new IdentityUserTest(), new IdentityUserTest() };
            // Act
            SUT.AddMany<IdentityUserTest, Guid>(documents);
            // Assert
            long count = SUT.Count<IdentityUserTest, Guid>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<IdentityUserTest> { new IdentityUserTest(), new IdentityUserTest() };
            // Act
            await SUT.AddManyAsync<IdentityUserTest, Guid>(documents);
            // Assert
            long count = SUT.Count<IdentityUserTest, Guid>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.Equal(2, count);
        }
    }
}
