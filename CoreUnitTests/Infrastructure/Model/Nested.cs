using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoreUnitTests.Infrastructure.Model;

public class Nested
{
    public DateTime SomeDate { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal SomeAmount { get; set; }
}