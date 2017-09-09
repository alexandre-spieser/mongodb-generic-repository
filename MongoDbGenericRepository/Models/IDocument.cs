using System;

namespace MongoDbGenericRepository.Models
{
    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Your document must implement this class in order for the MongoDbRepository to handle them.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// The date and UTC time at which the document was added to the collection.
        /// </summary>
        DateTime AddedAtUtc { get; set; }
        /// <summary>
        /// The Guid, which must be decorated with the [BsonId] attribute 
        /// if you want the MongoDb C# driver to consider it to be the document ID.
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// A version number, to indicate the version of the schema.
        /// </summary>
        int Version { get; set; }
    }
}