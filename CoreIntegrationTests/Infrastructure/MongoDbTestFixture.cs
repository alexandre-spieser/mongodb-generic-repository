using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace CoreIntegrationTests.Infrastructure
{

    public class MongoDbTestFixture<T, TKey> : IDisposable
    where T : IDocument<TKey>, new()
    where TKey : IEquatable<TKey>
    {

        public IMongoDbContext Context;

        public MongoDbTestFixture()
        {
            DocsToDelete = new ConcurrentBag<T>();
        }

        public string PartitionKey { get; set; }

        public ConcurrentBag<T> DocsToDelete { get; set; }

        public virtual void Dispose()
        {
            var docIds = DocsToDelete.ToList().Select(e => e.Id);
            if (docIds.Any())
            {
                TestRepository.Instance.DeleteMany<T, TKey>(e => docIds.Contains(e.Id));
            }
        }

        public T CreateTestDocument()
        {
            var doc = new T();
            DocsToDelete.Add(doc);
            return doc;
        }

        public List<T> CreateTestDocuments(int numberOfDocumentsToCreate)
        {
            var docs = new List<T>();
            for (var i = 0; i < numberOfDocumentsToCreate; i++)
            {
                docs.Add(new T());
                DocsToDelete.Add(docs.Last());
            }
            return docs;
        }
    }
}
