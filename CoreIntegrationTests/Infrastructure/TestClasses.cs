using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;

namespace CoreIntegrationTests.Infrastructure
{
    public class ProjectedGroup
    {
        public int Key { get; set; }
        public List<string> Content { get; set; }
    }

    public class MyTestProjection
    {
        public string SomeContent { get; set; }
        public DateTime SomeDate { get; set; }
    }

    public class Nested
    {
        public DateTime SomeDate { get; set; }
    }

    public class Child
    {
        public Child(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class TestDoc : Document
    {
        public TestDoc()
        {
            Version = 2;
            Nested = new Nested
            {
                SomeDate = DateTime.UtcNow
            };
            Children = new List<Child>();
        }

        public string SomeContent { get; set; }

        public int GroupingKey { get; set; }

        public Nested Nested { get; set; }

        public List<Child> Children { get; set; }

    }

    public class TestDoc<TKey> : IDocument<TKey>
            where TKey : IEquatable<TKey>
    {
        [BsonId]
        public TKey Id { get; set; }
        public int Version { get; set; }

        public TestDoc()
        {
            InitializeFields();
            Version = 2;
            Nested = new Nested
            {
                SomeDate = DateTime.UtcNow
            };
            Children = new List<Child>();
        }

        public string SomeContent { get; set; }

        public Nested Nested { get; set; }

        public List<Child> Children { get; set; }

        public TId Init<TId>()
        {
            var idTypeName = typeof(TKey).Name;
            switch (idTypeName)
            {
                case "Guid":
                    return (TId)(object)Guid.NewGuid();
                case "Int16":
                    return (TId)(object)GlobalVariables.Random.Next(1, short.MaxValue);
                case "Int32":
                    return (TId)(object)GlobalVariables.Random.Next(1, int.MaxValue);
                case "Int64":
                    return (TId)(object)(GlobalVariables.Random.NextLong(1, long.MaxValue));
                case "String":
                    return (TId)(object)Guid.NewGuid().ToString();
                default:
                    throw new NotSupportedException($"{idTypeName} is not supported.");
            }
        }

        private void InitializeFields()
        {
            Id = Init<TKey>();
        }
    }
}
