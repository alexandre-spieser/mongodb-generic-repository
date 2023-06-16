using System;
using System.Collections.Generic;
using MongoDbGenericRepository.Models;

namespace CoreUnitTests.Infrastructure.Model;

public class TestDocumentWithKey<TKey> : IDocument<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
    public int Version { get; set; }

    public TestDocumentWithKey()
    {
        Version = 2;
        Nested = new Nested
        {
            SomeDate = DateTime.UtcNow
        };
        Children = new List<Child>();
    }

    public int SomeValue { get; set; }

    public string SomeContent { get; set; }
    public string SomeContent2 { get; set; }
    public string SomeContent3 { get; set; }

    public int GroupingKey { get; set; }

    public Nested Nested { get; set; }

    public List<Child> Children { get; set; }
}