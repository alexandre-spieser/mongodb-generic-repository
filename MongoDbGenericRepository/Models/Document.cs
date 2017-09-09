using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDbGenericRepository.Models
{
    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Your document must implement this class in order for the MongoDbRepository to handle them.
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// The document constructor
        /// </summary>
        public Document()
        {
            Id = Guid.NewGuid();
            AddedAtUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// The Id of the document
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// The datetime in UTC at which the document was added.
        /// </summary>
        public DateTime AddedAtUtc { get; set; }

        /// <summary>
        /// The version of the schema of the document
        /// </summary>
        public int Version { get; set; }
    }
}