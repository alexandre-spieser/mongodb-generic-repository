namespace MongoDbGenericRepository.Models
{
    /// <summary>
    /// This class represents a document that can be inserted in a collection that can be partitioned.
    /// The partition key allows for the creation of different collections having the same document schema.
    /// This can be useful if you are planning to build a Software as a Service (SaaS) Platform, or if you want to reduce indexing.
    /// You could for example insert Logs in different collections based on the week and year they where created, or their Log category/source.
    /// </summary>
    public class PartitionedDocument : Document, IPartitionedDocument
    {
        /// <summary>
        /// The constructor, it needs a partition key.
        /// </summary>
        /// <param name="partitionKey"></param>
        public PartitionedDocument(string partitionKey)
        {
            PartitionKey = partitionKey;
        }
        /// <summary>
        /// The name of the property used for partitioning the collection
        /// This will not be inserted into the collection.
        /// This partition key will be prepended to the collection name to create a new collection.
        /// </summary>
        public string PartitionKey { get; set; }
    }
}
