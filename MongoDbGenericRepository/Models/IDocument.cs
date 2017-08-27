using System;

namespace MongoDbGenericRepository.Models
{
    public interface IDocument
    {
        DateTime AddedAtUtc { get; set; }
        Guid Id { get; set; }
        string PartitionKey { get; set; }
        int Version { get; set; }
    }
}