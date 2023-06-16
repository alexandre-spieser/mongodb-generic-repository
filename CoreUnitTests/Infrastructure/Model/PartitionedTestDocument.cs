using MongoDbGenericRepository.Models;

namespace CoreUnitTests.Infrastructure.Model;

public class PartitionedTestDocument : TestDocument, IPartitionedDocument
{
    /// <inheritdoc />
    public string PartitionKey { get; set; } = "PartitionedTestDocument";
}