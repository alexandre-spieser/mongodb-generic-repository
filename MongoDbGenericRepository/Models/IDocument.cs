using System;

namespace MongoDbGenericRepository.Models
{
    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Your document must implement this class in order for the MongoDbRepository to handle them.
    /// </summary>
    public interface IDocument
    {
        DateTime AddedAtUtc { get; set; }
        Guid Id { get; set; }
        int Version { get; set; }
    }

    /// <summary>
    /// This class represents a document that can be inserted in a collection that can be partitioned.
    /// The partition key allows for the creation of different collections having the same document schema.
    /// This can be useful if you are planning to build a Software as a Service (SaaS) Platform, or if you want to reduce indexing.
    /// You could for example insert Logs in different collections based on the week and year they where created, or their Log category/source.
    /// </summary>
    public interface IPartitionedDocument : IDocument
    {
        string PartitionKey { get; set; }
    }
}