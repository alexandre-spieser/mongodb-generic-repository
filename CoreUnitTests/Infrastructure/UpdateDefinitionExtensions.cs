using System;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CoreUnitTests.Infrastructure;

public static class UpdateDefinitionExtensions
{
    public static bool EquivalentTo<TDocument>(this UpdateDefinition<TDocument> update, UpdateDefinition<TDocument> expected)
    {
        var renderedUpdate = update.Render(new RenderArgs<TDocument>(
            BsonSerializer.SerializerRegistry.GetSerializer<TDocument>(),
            BsonSerializer.SerializerRegistry));

        var renderedExpected = expected.Render(new RenderArgs<TDocument>(
            BsonSerializer.SerializerRegistry.GetSerializer<TDocument>(),
            BsonSerializer.SerializerRegistry));

        return renderedUpdate.Equals(renderedExpected);
    }
}
